using UnityEngine;
namespace Kanamori
{
    public class ShowSelfObject : MonoBehaviour
    {
        /// <summary>
        /// 进入事件
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            SetVisible(true);
        }
        /// <summary>
        /// 退出事件
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            SetVisible(false);
        }
        /// <summary>
        /// 设置自身是否可见
        /// </summary>
        /// <param name="status">状态</param>
        public void SetVisible(bool status)
        {
            GetComponent<MeshRenderer>().enabled = status;
        }
    }
}

