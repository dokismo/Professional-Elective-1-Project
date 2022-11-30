using UnityEngine;

namespace Gun
{
    public class FirePath : MonoBehaviour
    {
        public LineRenderer lineRenderer;

        public Transform lineStart;
        public float distanceToRender = 5;
        
        [Range(0, 1)]
        public float lifeTime = 0.05f;

        private float timer;

        private void Update()
        {
            timer = Mathf.Clamp01(timer - Time.deltaTime);
            lineRenderer.enabled = timer > 0;
        }

        public void RenderLine(Vector3 endPosition)
        {
            if (!enabled || Vector3.Distance(transform.position, endPosition) < distanceToRender) return;
            
            timer = lifeTime;
            
            lineRenderer.SetPosition(0, lineStart.position);
            lineRenderer.SetPosition(1, endPosition);
        }
    }
}