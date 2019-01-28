using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class AbstractWeapon : MonoBehaviour
    {
        public int damage;
        public Gradient gradientColor;
        public ParticleDecalPool CollisionDecalPool;
        private ParticleSystem launcher;
        private List<ParticleCollisionEvent> collisionEvents;

        private void Start()
        {
            launcher = GetComponent<ParticleSystem>();
            collisionEvents = new List<ParticleCollisionEvent>();
        }

        private void OnParticleCollision(GameObject other)
        {
            ParticlePhysicsExtensions.GetCollisionEvents(launcher, other, collisionEvents);
            Debug.Log(collisionEvents.Count);
            for (int i = 0; i < collisionEvents.Count; i++)
            {
                Debug.Log(i);
                CollisionDecalPool.ParticleHit(collisionEvents[i], gradientColor);
            }
        }
    }
}