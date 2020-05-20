using UnityEngine;
namespace Kanamori
{
    public class ShowSelfObject : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            SetVisible(true);
        }
        private void OnTriggerExit(Collider other)
        {
            SetVisible(false);
        }
        public void SetVisible(bool status)
        {
            GetComponent<MeshRenderer>().enabled = status;
        }
    }
}

