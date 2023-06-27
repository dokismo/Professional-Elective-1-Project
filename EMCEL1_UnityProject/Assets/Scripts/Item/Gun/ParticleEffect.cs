using Unity.Mathematics;
using UnityEngine;

namespace Item.Gun
{
    public enum SurfaceType
    {
        Wall,
        Flesh
    }
    public enum WeaponType
    {
        Gun,
        Melee
    }

    public class ParticleEffect : MonoBehaviour
    {
        public WeaponType weaponType = WeaponType.Gun;
        [Header("Particles")] 
        public GameObject bulletImpact;
        public GameObject bloodImpact;

        [Header("Gun shot Decals")]
        public GameObject WallShotDecal;
        public GameObject FleshShotDecal;

        [Header("Melee shot Decals")]

        public GameObject WallSlashDecal;
        public GameObject FleshSlashDecal;


        public void SpawnEffect(Vector3 point, Vector3 normal, SurfaceType surfaceType, Transform ObjectTransform)
        {
            var instanceParticle = Instantiate(surfaceType == SurfaceType.Wall
                ? bulletImpact : bloodImpact, point, quaternion.identity);
            instanceParticle.transform.LookAt(normal + instanceParticle.transform.position);

            GameObject instanceDecal = null;

            if (weaponType == WeaponType.Gun)
            {
                instanceDecal = Instantiate(surfaceType == SurfaceType.Wall
                ? WallShotDecal : FleshShotDecal, point, Quaternion.identity);
            } else if (weaponType == WeaponType.Melee)
            {
                instanceDecal = Instantiate(surfaceType == SurfaceType.Wall
                ? WallSlashDecal : FleshSlashDecal, point, Quaternion.identity);
            }


            if (surfaceType == SurfaceType.Wall) Destroy(instanceDecal, 5f);

            instanceDecal.transform.parent = ObjectTransform;
            instanceDecal.transform.forward = normal * -1f;

            instanceDecal.transform.position += instanceDecal.transform.forward * -0.1f;
            



        }
    }
}