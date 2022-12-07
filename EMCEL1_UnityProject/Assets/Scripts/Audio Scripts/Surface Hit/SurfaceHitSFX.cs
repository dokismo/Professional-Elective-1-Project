using Item.Gun;
using UnityEngine;

namespace Item.Gun
{
    public class SurfaceHitSFX : MonoBehaviour
    {
        public delegate void SurfaceSFXEvent();
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
        private void Flesh()
        {
            //GameObject shotFlesh = Instantiate(FleshShot, transform.position, transform.rotation);
            Debug.Log("Flesh Hit");
        }
        private void Wall()
        {
            GameObject shotWall = Instantiate(WallShot, transform.position, transform.rotation);
            Debug.Log("Wall Hit");
        }
    }


}
