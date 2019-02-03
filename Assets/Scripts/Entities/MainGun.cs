using System.Collections;
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
    
    private int damageAnimationCounter = 1;
    private Animator turretAnimator;
    private Animator gunAnimator;
    private bool _isDead;
    
    private void Start()
    {
        turretAnimator = transform.parent.GetComponent<Animator>();
        gunAnimator = GetComponent<Animator>();
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
            Debug.Log("GameOver");
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
            if (Input.GetButtonDown("Fire1"))
            {
                var gunPosition = gunShootPoint.transform.position;
            
                var muzzle = Instantiate(muzzleFlash);
                muzzle.transform.position = gunPosition;
            
                var light = Instantiate(muzzleLight);
                light.transform.position = new Vector3(gunPosition.x, gunPosition.y, -1.3f);

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
    }

    private IEnumerator Destroy()
    {
        Instantiate(deathParticles, transform);
        turretAnimator.SetTrigger("Destroyed");
        gunAnimator.SetTrigger("Destroyed");
        yield return new WaitForSeconds(turretAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
}
