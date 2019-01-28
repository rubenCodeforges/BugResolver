using UnityEngine;

namespace DefaultNamespace
{
    public class SimpleGun: AbstractWeapon
    {
        private void Start()
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position, shootVolume);
        }
    }
}