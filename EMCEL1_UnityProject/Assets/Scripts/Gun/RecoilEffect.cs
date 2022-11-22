using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Gun
{
    public class RecoilEffect : MonoBehaviour
    {
        public delegate float RecoilEvent(float x, float y);
        public delegate void RecoilEvent2(float x);
        public static RecoilEvent apply;
        public static RecoilEvent2 setControl;

        [FormerlySerializedAs("curve")] 
        public AnimationCurve controlCurve;
        public AnimationCurve shootingCurve;

        public Vector2 localMaxRecoil = new(10, -15);

        private float processedYAxis;
        private float recoilControl;
        private float appliedRecoil;
        private float shootingTimer;
        private float timer;

        private void OnEnable()
        {
            apply += SprayLinear;
            setControl += SetControl;
        }

        private void OnDisable()
        {
            apply -= SprayLinear;
            setControl -= SetControl;
        }

        private void Update()
        {
            timer = Mathf.Clamp01(timer + Time.deltaTime);

            if (timer > 0.3f)
            {
                shootingTimer = Mathf.Clamp01(shootingTimer - Time.deltaTime);
            }

            appliedRecoil = Mathf.Clamp(appliedRecoil - recoilControl * controlCurve.Evaluate(timer), 0, -localMaxRecoil.y);
            
            transform.localRotation = Quaternion.Euler(-appliedRecoil, transform.localRotation.eulerAngles.y + processedYAxis, 0);
            
            processedYAxis = 0;
        }

        private void SetControl(float control) => recoilControl = control;

        private void ApplyX(float recoil)
        {
            appliedRecoil = Mathf.Clamp(recoil + appliedRecoil, 0, -localMaxRecoil.y);
            timer = 0;
        }

        private float SprayLinear(float recoil, float maxRecoil)
        {
            ApplyX(recoil);
            
            shootingTimer = Mathf.Clamp01(shootingTimer + Time.deltaTime * 3) ;
            
            float rotX = Mathf.Clamp(recoil, -localMaxRecoil.x, localMaxRecoil.x) * 0.75f;
            float inverseLerp = Mathf.InverseLerp(0, -localMaxRecoil.y, appliedRecoil);
            float test = 
                shootingCurve.Evaluate(shootingTimer) * 
                Mathf.InverseLerp(0, -localMaxRecoil.y, appliedRecoil) * maxRecoil;
            
            processedYAxis =
                inverseLerp >= 0.8f
                    ? Random.Range(-rotX, rotX)
                    : 0;
            
            Debug.Log($"{inverseLerp}, {rotX} , {processedYAxis}");
            
            return test;
        }
        
    }
}