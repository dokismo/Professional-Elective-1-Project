using UnityEngine;
using Random = UnityEngine.Random;

namespace Gun
{
    public class RecoilEffect : MonoBehaviour
    {
        public delegate float RecoilEvent(float x, float y);
        public delegate void RecoilEvent2(float x);
        public static RecoilEvent apply;
        public static RecoilEvent2 setControl;

        public AnimationCurve curve;

        public Vector2 localMaxRecoil = new(10, -15);

        private float processedYAxis;
        private float recoilControl;
        private float appliedRecoil;
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
            timer = Mathf.Clamp(timer + Time.deltaTime, 0, 1);
            appliedRecoil = Mathf.Clamp(appliedRecoil - recoilControl * curve.Evaluate(timer), 0, -localMaxRecoil.y);
            
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

            float rotX = Mathf.Clamp(recoil * 0.6f, -localMaxRecoil.x, localMaxRecoil.x) * 0.75f;
            float inverseLerp = Mathf.InverseLerp(0, localMaxRecoil.y, appliedRecoil);
            
            processedYAxis =
                inverseLerp >= 0.8f
                    ? Random.Range(-rotX, rotX)
                    : 0;
            
            Debug.Log($"{processedYAxis}, {inverseLerp} , {rotX}");
            
            return Mathf.InverseLerp(localMaxRecoil.y, 0, appliedRecoil) * maxRecoil;
        }
        
    }
}