using UnityEngine;

namespace Player.Gun
{
    public class GunAnimation : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Transform gunAnchor;
        
        [SerializeField] 
        private float lowestZ = -90, highestZ = 90;

        private float z;
        
        private void Start()
        {
            gunAnchor = GetComponentInParent<Transform>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            z = gunAnchor.rotation.eulerAngles.z;
            spriteRenderer.flipY = z > lowestZ && z < highestZ;
        }
    }
}