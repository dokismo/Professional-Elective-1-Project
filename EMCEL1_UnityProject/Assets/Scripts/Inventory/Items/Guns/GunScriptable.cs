using UnityEngine;

namespace Inventory.Items.Guns
{
    [CreateAssetMenu(fileName = "TestGun", menuName = "Item/TestGun")]
    public class GunScriptable : ItemScriptable
    {
        public AmmoType ammoType = AmmoType.Rifle;
        public int totalAmmo = 60;

        public override AmmoType GetAmmoType() => ammoType;
    }
}