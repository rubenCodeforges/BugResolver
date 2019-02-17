using UnityEngine;

namespace DefaultNamespace
{
    public class AbstractWeapon : MonoBehaviour
    {
        public int damage;
        public AudioClip shootSound;
        public float shootVolume = 1f;
        protected MainGun MainGun;
    }
}