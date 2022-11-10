using Player;
using Player.Inventory;
using UnityEngine;

namespace CORE
{
    public interface IInteract
    {
        void Interact();
    }

    public interface IOpenClose
    {
        void IsOpen(bool value);
    }

    public interface IPickup<out T>
    {
        GameObject GetItem();

        T GetItemType();
    }
}