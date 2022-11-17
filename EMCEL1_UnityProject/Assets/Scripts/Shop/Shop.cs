using Player;
using Player.Control;
using UnityEngine;

namespace Shop
{
    public class Shop : MonoBehaviour
    {
        public ItemsScriptable itemsScriptable;
        public PlayerStatusScriptable playerStatusScriptable;

        public PlayerStatus playerStatus;

        private void Start()
        {
            playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        }

        public void Buy(int position)
        {
            Item item = itemsScriptable.GetItem(position);
            
            if (item.gameObject == null || playerStatus.inventoryIsFull)
                return;

            if (playerStatusScriptable.GetMoney(item.value) > -1 ? item.gameObject : null) playerStatus.AddGun(item.gameObject);
        }
    }
}