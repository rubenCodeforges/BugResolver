using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BloodSplat : MonoBehaviour
{
    public Gradient gradientColor;

    private ParticleDecalPool CollisionDecalPool;
    private ParticleSystem launcher;
    private List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        launcher = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        CollisionDecalPool = FindObjectOfType<ParticleDecalPool>();
        Debug.Log("SplatStart");
        if (CollisionDecalPool == null)
        {
            throw new SyntaxErrorException("No decal pool found");
        }
    }
    
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(launcher, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            CollisionDecalPool.ParticleHit(collisionEvents[i], gradientColor);
        }
    }
}
