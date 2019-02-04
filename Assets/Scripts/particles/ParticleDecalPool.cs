using UnityEngine;


public class ParticleDecalPool : MonoBehaviour
{
    public int maxDecals = 100;
    public float decalSizeMin = .5f;
    public float decalSizeMax = 1.5f;
    public Color particlesStartColor;
    public ParticleSystem ps;
    
    private ParticleSystem decalParticleSystem;
    private int particleDecalDataIndex;
    private ParticleDecalData[] particleData;
    private ParticleSystem.Particle[] particles;

    void Start()
    {
        decalParticleSystem = GetComponent<ParticleSystem>();
        InitParticles();
    }

    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        SetParticleData(particleCollisionEvent, colorGradient);
        DisplayParticles();
    }

    void SetParticleData(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        if (particleDecalDataIndex >= maxDecals)
        {
            particleDecalDataIndex = 0;
        }

        particleData[particleDecalDataIndex].position = particleCollisionEvent.intersection;
        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        particleData[particleDecalDataIndex].color = colorGradient.Evaluate(Random.Range(0f, 1f));

        particleDecalDataIndex++;
    }

    void DisplayParticles()
    {
        for (int i = 0; i < particleData.Length; i++)
        {
            particles[i].position = particleData[i].position;
        }

        decalParticleSystem.SetParticles(particles, particles.Length);
    }
    
    private void InitParticles()
    {
        particles = new ParticleSystem.Particle[maxDecals];
        particleData = new ParticleDecalData[maxDecals];

        for (int i = 0; i < maxDecals; i++)
        {
            particleData[i] = new ParticleDecalData();
            particles[i].startColor = particlesStartColor;
            particles[i].startSize = decalSizeMin;
        }
    }
}