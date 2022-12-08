using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Display
{
    public class BuySound : MonoBehaviour
    {
        public delegate void GunEvent();
        public static GunEvent buyEvent;
        public static GunEvent lookEvent;    

        public AudioClip purchase, hover;
        public AudioSource audiosource;

        private InspectShopWall buyItem;

        private void OnEnable()
        {
            buyEvent += buy;
            lookEvent += look;
        }
        private void OnDisable()
        {
            buyEvent -= buy;
            lookEvent -= look;
        }
        private void Start()
        {
            buyItem = GetComponent<InspectShopWall>();
        }
        private void buy()
        {
            audiosource.PlayOneShot(purchase);
        }
        private void look()
        {
            audiosource.PlayOneShot(hover);
            //Debug.Log("Looking");
        }

    }
}

