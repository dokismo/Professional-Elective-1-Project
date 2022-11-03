using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 1f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
