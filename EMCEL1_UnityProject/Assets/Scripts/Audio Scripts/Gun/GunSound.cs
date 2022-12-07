using Item.Gun;
using UnityEngine;

namespace Gun
{
    public class GunSound : MonoBehaviour
    {
        public delegate void GunEvent();
        public static GunEvent reloadEvent;

        public AudioClip reload;
        public AudioSource audiosource;

        private Shooting gunShooting;

        private void OnEnable()
        {
            reloadEvent += Reload;
        }
        private void OnDisable()
        {
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
    }
    
   
}
