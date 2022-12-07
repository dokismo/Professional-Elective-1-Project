using Item.Gun;
using Unity.Mathematics;
using UnityEngine;

namespace Item.Gun
{
    public class SurfaceHitSFX : MonoBehaviour
    {
        public delegate void SurfaceSFXEvent(Vector3 position);
        public static SurfaceSFXEvent wallEvent, fleshEvent;

        public GameObject WallShot, FleshShot;

        private Shooting surfaceShooting;

        private void OnEnable()
        {
            wallEvent += Wall;
            fleshEvent += Flesh;
        }
        private void OnDisable()
        {
            wallEvent -= Wall;
            fleshEvent -= Flesh;
        }
        private void Start()
        {
            surfaceShooting = GetComponent<Shooting>();
        }
        private void Flesh(Vector3 position)
        {
            //GameObject shotFlesh = Instantiate(FleshShot, position, quaternion.identity);
            Debug.Log("Flesh Hit");
        }
        private void Wall(Vector3 position)
        {
            GameObject shotWall = Instantiate(WallShot, position, quaternion.identity);
            Debug.Log("Wall Hit");
        }
    }


}
