using Player.Control;
using UnityEngine;

namespace Shop
{
    public class ShowShop : MonoBehaviour
    {
        public delegate void ShopUI(bool state);

        public static ShopUI showState;

        private void Start()
        {
            showState?.Invoke(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                 return;
            
            Cursor.lockState = CursorLockMode.None;
            
            showState?.Invoke(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
            
            Cursor.lockState = CursorLockMode.Locked;
            
            showState?.Invoke(false);
        }
    }
}
