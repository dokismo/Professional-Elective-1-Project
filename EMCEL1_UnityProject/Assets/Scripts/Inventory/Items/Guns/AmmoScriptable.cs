using UnityEngine;

namespace Inventory.Items.Guns
{
    
    [CreateAssetMenu(fileName = "Ammo", menuName = "Item/Ammo")]
    public class AmmoScriptable : ItemScriptable
    {
        public AmmoType ammoType = AmmoType.Rifle;
        public int amount = 60;

        public override AmmoScriptable GetAmmoScriptable() => this;
    }
}