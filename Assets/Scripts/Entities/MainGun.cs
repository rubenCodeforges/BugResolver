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
    
    private int damageAnimationCounter = 1;
    
    
    public void takeDamage(int amount)
    {
        health -= amount;

        if (damageAnimationCounter <= maxDamageAnimationClips)
        {
            var animator = transform.parent.GetComponent<Animator>();
            animator.SetInteger("DamageCounter", damageAnimationCounter);
            animator.Play("TurretHit");
            Destroy(healthBar.transform.GetChild(0).gameObject);
            damageAnimationCounter++;
        }
        
        if (health <= 0)
        {
            Debug.Log("GameOver");
        }
    }
    
    // Update is called once per frame
    void Update()
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

    private void TargetMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    
}
