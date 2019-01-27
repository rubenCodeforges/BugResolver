using System.Collections;
using DefaultNamespace;
using UnityEngine;

public abstract class AbstractBug : MonoBehaviour
{
    public float health;

    public abstract bool isDead { get; }

    public IEnumerator TakeDamage(int damage)
    {
        health -= damage;
        if (isDead)
        {
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<Collider2D>().isTrigger = true;
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
}