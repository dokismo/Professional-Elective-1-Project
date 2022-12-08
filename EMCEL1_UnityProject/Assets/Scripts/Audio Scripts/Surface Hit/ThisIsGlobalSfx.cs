using System;
using Unity.Mathematics;
using UnityEngine;

namespace Audio_Scripts.Surface_Hit
{
    public class ThisIsGlobalSfx : MonoBehaviour
    {
        public static Action<Vector3> wallEvent, fleshEvent;
        public static Action<Vector3, GameObject> grunt, death;

        public GameObject wallShot, fleshShot;

        private void OnEnable()
        {
            wallEvent += Wall;
            fleshEvent += Flesh;
            grunt += PlaceObjAt;
            death += PlaceObjAt;
        }

        private void OnDisable()
        {
            wallEvent -= Wall;
            fleshEvent -= Flesh;
            grunt -= PlaceObjAt;
            death -= PlaceObjAt;
        }
        
        private void PlaceObjAt(Vector3 arg1, GameObject arg2) => Instantiate(arg2, arg1, quaternion.identity);
        
        private void Flesh(Vector3 position) => Instantiate(fleshShot, position, quaternion.identity);

        private void Wall(Vector3 position) => Instantiate(wallShot, position, quaternion.identity);
    }


}
