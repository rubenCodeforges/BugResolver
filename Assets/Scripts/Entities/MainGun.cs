﻿using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MainGun : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public ParticleSystem muzzleFlash;
    public GameObject muzzleLight;
    public ParticleDecalPool DecalPool;
    public GameObject gunShootPoint;

    public int health = 10;
    public int maxDamageAnimationClips = 3;
    public GameObject healthBar;
    public ParticleSystem deathParticles;
    public float laserPointerDistance = 20f;
    public Material laserPointerMaterial;
    public float SemiAutoFireRate = 1f;

    [HideInInspector]
    public PowerUp currentPowerUp = PowerUp.SimpleGun;
    
    private int damageAnimationCounter = 1;
    private Animator turretAnimator;
    private Animator gunAnimator;
    private bool _isDead;
    private LineRenderer _lineRenderer;
    private bool isFiring;
    
    private void Start()
    {
        turretAnimator = transform.parent.GetComponent<Animator>();
        gunAnimator = GetComponent<Animator>();
        GameObject newLine = new GameObject("TargetLine");
        _lineRenderer = newLine.AddComponent<LineRenderer>();
        _lineRenderer.material = laserPointerMaterial;
    }

    public void takeDamage(int amount)
    {
        health -= amount;

        if (damageAnimationCounter <= maxDamageAnimationClips)
        {
            turretAnimator.SetInteger("DamageCounter", damageAnimationCounter);
            turretAnimator.Play("TurretHit");
            Destroy(healthBar.transform.GetChild(0).gameObject);
            damageAnimationCounter++;
        }

        if (health <= 0)
        {
            StartCoroutine(Destroy());
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    public bool isDead
    {
        get { return health <= 0; }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            TargetMouse();
                
            if (Input.GetButtonDown("Fire1") && currentPowerUp == PowerUp.SimpleGun)
            {
               FireSimpleGun();
            }

            if (Input.GetButton("Fire1") && !isFiring && currentPowerUp == PowerUp.SemiAuto)
            {
                StartCoroutine(FireSemiAuto());
            }

            muzzleFlash.transform.localRotation = transform.rotation;
        }
    }

    private void TargetMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        DrawGunTargetingLine(direction);
    }

    private void DrawGunTargetingLine(Vector2 direction)
    {
        _lineRenderer.SetPosition(0, gunShootPoint.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, gunShootPoint.transform.position * laserPointerDistance, out hit,
            Mathf.Infinity))
        {
            _lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, gunShootPoint.transform.position * laserPointerDistance);
        }

        _lineRenderer.startWidth = .01f;
        _lineRenderer.endWidth = .01f;
    }

    private IEnumerator Destroy()
    {
        Instantiate(deathParticles, transform);
        turretAnimator.SetTrigger("Destroyed");
        gunAnimator.SetTrigger("Destroyed");
        yield return new WaitForSeconds(turretAnimator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void FireSimpleGun()
    {
        var gunPosition = gunShootPoint.transform.position;

        var muzzle = Instantiate(muzzleFlash);
        muzzle.transform.position = gunPosition;

        var light = Instantiate(muzzleLight);
        light.transform.position = new Vector3(gunPosition.x, gunPosition.y, -1.3f);
        gunAnimator.SetTrigger("Fire");
    }

    private IEnumerator FireSemiAuto()
    {
        isFiring = true;
        FireSimpleGun();
        yield return new WaitForSeconds(SemiAutoFireRate);
        isFiring = false;
    }
}