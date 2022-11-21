using Unity.Mathematics;
using UnityEngine;

namespace Gun
{
    public enum SurfaceType
    {
        Wall,
        Flesh
    }
    
    
    public class ParticleEffect : MonoBehaviour
    {
        [Header("Particles")] 
        public GameObject bulletImpact;
        public GameObject bloodImpact;

        public void SpawnEffect(Vector3 point, Vector3 normal, SurfaceType surfaceType)
        {
            var instance = Instantiate(surfaceType == SurfaceType.Wall
                ? bulletImpact : bloodImpact, point, quaternion.identity);
            instance.transform.LookAt(normal + instance.transform.position);
        }
    }
}