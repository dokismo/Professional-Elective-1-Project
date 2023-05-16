using Item.Gun;
using UnityEngine;

namespace Gun
{
    public class GunSound : MonoBehaviour
    {
        public delegate void GunEvent();
        public static GunEvent reloadEvent, shootEvent;

        public AudioClip reload;
        public AudioSource audiosource;

        public GameObject GunShot;

        private Shooting gunShooting;

        private void OnEnable()
        {
            reloadEvent += Reload;
            shootEvent += Shoot;
        }
        private void OnDisable()
        {
            reloadEvent -= Reload;
            shootEvent -= Shoot;
        }
        private void Start()
        {
            gunShooting = GetComponent<Shooting>();
        }
        private void Shoot()
        {
            GameObject gunShot = Instantiate(GunShot, transform.position, transform.rotation);
        }
        private void Reload()
        {
            audiosource.PlayOneShot(reload);
        }
    }
    
   
}
