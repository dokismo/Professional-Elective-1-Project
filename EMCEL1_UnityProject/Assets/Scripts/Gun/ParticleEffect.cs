using Unity.Mathematics;
using UnityEngine;

namespace Gun
{
    public class ParticleEffect : MonoBehaviour
    {
        [Header("Particles")] 
        public GameObject bulletImpact;

        public void SpawnEffect(Vector3 point, Vector3 normal)
        {
            var instance = Instantiate(bulletImpact, point, quaternion.identity);
            instance.transform.LookAt(normal + instance.transform.position);
        }
    }
}