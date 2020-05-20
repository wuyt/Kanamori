using UnityEngine;

namespace Kanamori
{
    public class ShowChildObject : MonoBehaviour
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
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(status);
            }
        }
    }
}

