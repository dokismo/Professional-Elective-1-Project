using UnityEngine;

namespace Item.Gun
{
    public class GunLight : MonoBehaviour
    {
        public delegate void TriggerLight();
        public static TriggerLight triggerLight;
        
        private Light fireLight;

        public float lifeTime = 0.05f;

        private float timer;

        private void OnEnable()
        {
            triggerLight += Light;
        }

        private void OnDisable()
        {
            triggerLight -= Light;
        }

        private void Start()
        {
            fireLight = GetComponent<Light>();
            fireLight.enabled = false;
        }

        private void Update()
        {
            timer = Mathf.Clamp01(timer - Time.deltaTime);
            fireLight.enabled = timer > 0;
        }

        public void Light() => timer = lifeTime;
    }
}