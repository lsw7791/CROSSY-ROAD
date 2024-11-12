using UnityEngine;

public class WaterParticle : MonoBehaviour
{
    public ParticleSystem waterParticlePrefab; 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            // 충돌 지점을 가져옴
            ContactPoint contact = collision.contacts[0];

            if (waterParticlePrefab != null)
            {
                ParticleSystem particle = Instantiate(waterParticlePrefab, contact.point, Quaternion.identity);

                Destroy(particle.gameObject, particle.main.duration);
            }
        }
    }
}
