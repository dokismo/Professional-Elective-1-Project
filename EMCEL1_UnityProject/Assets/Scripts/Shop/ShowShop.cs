using UnityEngine;

namespace Shop
{
    public class ShowShop : MonoBehaviour
    {
        public GameObject shopUI;

        private void Start()
        {
            shopUI.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                 return;
            
            shopUI.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
            
            shopUI.SetActive(false);
        }
    }
}
