﻿using System.Collections;
using DefaultNamespace;
using UnityEngine;

public abstract class AbstractBug : MonoBehaviour
{
    public float health;
    public float maxDistanceToTarget;
    public float speed;
    public ParticleSystem deathParticles;
    public AudioClip[] HitAudioClips;
    public AudioClip[] DeathAudioClips;
    public float audioVolume = 1f;
    public int damage = 1;

    public abstract bool isDead { get; }
    private bool isDamaging;
    
    public IEnumerator TakeDamage(int damage)
    {
        health -= damage;
        var position = transform.position;
        var randHit = Random.Range(0, HitAudioClips.Length);

        AudioSource.PlayClipAtPoint(HitAudioClips[randHit], position, audioVolume);

        if (isDead)
        {
            var particleSystem = Instantiate(deathParticles);
            var randDeath = Random.Range(0, DeathAudioClips.Length);

            particleSystem.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(DeathAudioClips[randDeath], position, audioVolume);

            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<Collider2D>().isTrigger = true;
            GetComponent<SpriteRenderer>().sortingLayerName = "Middle_behind";
            FindObjectOfType<GameManager>().AddScore(damage);
            yield return null;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
    
        if (other.CompareTag("Weapon"))
        {
            if (!isDead)
            {
                var damage = other.GetComponent<AbstractWeapon>().damage;
                StartCoroutine(TakeDamage(damage));                
            }
        }
    }

    protected void MoveToTarget(GameObject target)
    {
        var distance = Vector2.Distance(transform.position, target.transform.position);

        if (!FindObjectOfType<MainGun>().isDead)
        {
            if (distance > maxDistanceToTarget && !isDead)
            {
            
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    target.transform.position,
                    speed * Time.deltaTime
                );
                transform.position += new Vector3(0,0, -0.1f);

                Vector3 moveDirection = target.transform.position - transform.position;
            
                if (moveDirection != Vector3.zero) 
                {
                    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.Euler(0,0,-90);
                }
            }
            else if (distance <= maxDistanceToTarget && !isDamaging && !isDead)
            {
                isDamaging = true;
                StartCoroutine(applyDamage(FindObjectOfType<MainGun>()));
            }
        }
    }
    
    protected IEnumerator applyDamage(MainGun target)
    {
        var animator = GetComponent<Animator>();
        animator.SetTrigger("Suicide");
        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        target.takeDamage(damage);
        StartCoroutine(TakeDamage((int) health)); 
        isDamaging = false;
    }
}