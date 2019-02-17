using UnityEngine;

namespace DefaultNamespace
{
    public class SimpleGun: AbstractWeapon
    {
        private PowerUp PowerUp = PowerUp.SemiAuto;
        private float destroyDelay = .1f;
        private void Start()
        {
            MainGun = FindObjectOfType<MainGun>();
            AudioSource.PlayClipAtPoint(shootSound, transform.position, shootVolume);
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("SemiAuto"))
            {
                MainGun.currentPowerUp = PowerUp;
                Destroy(other, destroyDelay);                
            }
        }
    }
   
}