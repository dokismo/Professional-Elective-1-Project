using UnityEngine;

namespace Gun
{
    public class GunSound : MonoBehaviour
    {
        public delegate void GunEvent();
        public static GunEvent shootEvent;
        public static GunEvent reloadEvent;

        public AudioClip shoot, reload;
        public AudioSource audiosource;

        private Shooting gunShooting;

        private void OnEnable()
        {
            shootEvent += Shoot;
            reloadEvent += Reload;
        }
        private void OnDisable()
        {
            shootEvent -= Shoot;
            reloadEvent -= Reload;
        }
        private void Start()
        {
            gunShooting = GetComponent<Shooting>();
        }
        private void Reload()
        {
            audiosource.PlayOneShot(reload);
        }
        private void Shoot()
        {
            audiosource.PlayOneShot(shoot);
        }
    }
    
   
}
