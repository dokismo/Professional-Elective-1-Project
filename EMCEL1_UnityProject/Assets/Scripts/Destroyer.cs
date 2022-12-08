using Core;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ITarget target = other.GetComponent<ITarget>();

        if (target != null)
            target.Hit(999999);
        else
            Destroy(other.gameObject);
    }
}
