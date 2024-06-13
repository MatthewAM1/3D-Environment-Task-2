using UnityEngine;

public class WindEffect : MonoBehaviour
{
    public float windStrength = 5.0f; // Kekuatan angin
    public float changeInterval = 3.0f; // Interval waktu untuk mengubah arah angin

    private Vector3 windDirection;
    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private float timer;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        }
        SetRandomWindDirection();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeInterval)
        {
            SetRandomWindDirection();
            timer = 0.0f;
        }
    }

    void LateUpdate()
    {
        if (particleSystem != null)
        {
            int numParticlesAlive = particleSystem.GetParticles(particles);

            for (int i = 0; i < numParticlesAlive; i++)
            {
                // Terapkan gaya angin pada partikel
                particles[i].velocity += windDirection.normalized * windStrength * Time.deltaTime;
            }

            particleSystem.SetParticles(particles, numParticlesAlive);
        }
    }

    void SetRandomWindDirection()
    {
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-0.1f, 0.1f); // Sebagian besar angin terjadi secara horizontal
        float randomZ = Random.Range(-1.0f, 1.0f);
        windDirection = new Vector3(randomX, randomY, randomZ);
    }
}
