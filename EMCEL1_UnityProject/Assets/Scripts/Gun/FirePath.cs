using System.Collections;
using UnityEngine;

namespace Gun
{
    public class FirePath : MonoBehaviour
    {
        public Transform tracer;
        public Transform lineStart;
        
        
        public float distanceToRender = 5;
        
        [Range(0, 1)]
        public float lifeTime = 0.05f;

        private float timer;
        private Vector3 middlePoint;

        private void Update()
        {
            timer = Mathf.Clamp01(timer - Time.deltaTime);
            tracer.gameObject.SetActive(timer > 0);
            
            
        }

        public void RenderLine(Vector3 endPosition)
        {
            if (!enabled || Vector3.Distance(lineStart.position, endPosition) < distanceToRender) return;
            
            timer = lifeTime;
            tracer.position = middlePoint;

            RenderTracer(endPosition);
        }

        public void RenderTracer(Vector3 endPosition)
        {
            // https://youtu.be/s6qhqm3ec28
            
            float distance = Vector3.Distance(lineStart.position, endPosition);
            tracer.localScale = new Vector3(tracer.localScale.x, distance * 1.1f, tracer.localScale.z);

            middlePoint = (lineStart.position + endPosition) / 2;
            tracer.position = middlePoint;
            
            Vector3 rotationDirection = (endPosition - lineStart.position);
            tracer.up = rotationDirection;
        }
    }
}