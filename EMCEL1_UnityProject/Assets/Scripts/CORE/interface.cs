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

    public interface IPickup
    {
        void Pickup();
    }
}