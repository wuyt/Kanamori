using UnityEngine;

namespace Kanamori.Difficulty
{
    public class PathsController : MonoBehaviour
    {
        public Transform pa;
        public Transform pb;
        public Transform path;

        void Update()
        {
            path.position = (pa.position + pb.position) / 2;
            path.LookAt(pa);
            path.localScale = new Vector3(0.03f, 1f, (pa.position - pb.position).magnitude * 0.1f);
        }
    }
}

