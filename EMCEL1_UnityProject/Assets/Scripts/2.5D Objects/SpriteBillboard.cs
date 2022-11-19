using Unity.Mathematics;
using UnityEngine;

namespace _2._5D_Objects
{
    public class SpriteBillboard : MonoBehaviour
    {
        public Transform parentTransform;
        
        public bool freezeXRotation;
        public bool freezeYRotation;
        public bool freezeZRotation;

        public Vector3 freezeValues = new Vector3(0, 0, 0);
        public Vector3 addRotation = new Vector3(0, 0, 0);

        private Transform mainCamera;


        private void Start()
        {
            mainCamera = Camera.main.transform;
        }

        private void Update()
        {
            if (Camera.main != null) mainCamera ??= Camera.main.transform;

            if (!mainCamera) return;
            
            Vector3 lookDir = (mainCamera.position - transform.position).normalized;
            Quaternion parentRot = parentTransform ? parentTransform.rotation : new();
            
            transform.rotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - parentRot.eulerAngles);
            
            FreezeRotationAxis();
        }

        private void FreezeRotationAxis()
        {
            Vector3 rotation = new()
            {
                x = freezeXRotation ? freezeValues.x : transform.rotation.eulerAngles.x,
                y = freezeYRotation ? freezeValues.y : transform.rotation.eulerAngles.y,
                z = freezeZRotation ? freezeValues.z : transform.rotation.eulerAngles.z
            };

            transform.localRotation = Quaternion.Euler(rotation.x + addRotation.x, rotation.y + addRotation.y, rotation.z + addRotation.z);
        }
    }
}