using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class FollowPlayer : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;

        public float followInterval;
        public Transform target;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            StartCoroutine(IFollow());
        }

        private IEnumerator IFollow()
        {
            while (true)
            {
                var path = new NavMeshPath();

                if (navMeshAgent.CalculatePath(target.position, path))
                {
                    navMeshAgent.SetPath(path);
                }
                
                yield return new WaitForSeconds(followInterval);
            }
            
            // ReSharper disable once IteratorNeverReturns
        }
    }
}
