using UnityEngine;
using UnityEngine.AI;

namespace Kanamori.Difficulty
{
    public class NavController : MonoBehaviour
    {
        public Transform target;
        public NavMeshAgent agent;
        public LineRenderer lineRenderer;
        private NavMeshPath path;
        public Transform player;

        void Start()
        {
            path = new NavMeshPath();
        }

        void Update()
        {
            //agent.SetDestination(target.position);
            agent.enabled=false;
            agent.transform.position=player.position;
            agent.enabled=true;
            agent.CalculatePath(target.position,path);
            lineRenderer.positionCount = path.corners.Length;
            lineRenderer.SetPositions(path.corners);
        }
    }
}

