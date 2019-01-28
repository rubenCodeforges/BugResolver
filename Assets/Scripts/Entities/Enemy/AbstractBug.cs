using System.Collections;
using DefaultNamespace;
using UnityEngine;

public abstract class AbstractBug : MonoBehaviour
{
    public float health;
    public float maxDistanceToTarget;
    public float speed;
    public ParticleSystem deathParticles;

    public abstract bool isDead { get; }

    public IEnumerator TakeDamage(int damage)
    {
        health -= damage;
        if (isDead)
        {
            var particleSystem = Instantiate(deathParticles);
            particleSystem.transform.position = transform.position;
            
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<Collider2D>().isTrigger = true;
            GetComponent<SpriteRenderer>().sortingLayerName = "Middle_behind";
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
        if (Vector2.Distance(transform.position, target.transform.position) > maxDistanceToTarget && !isDead)
        {
            
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.transform.position,
                speed * Time.deltaTime
                );

            Vector3 moveDirection = target.transform.position - transform.position; 
            if (moveDirection != Vector3.zero) 
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.Euler(0,0,-90);
            }
        }
    }
}