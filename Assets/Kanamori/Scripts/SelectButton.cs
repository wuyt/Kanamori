using UnityEngine;
using UnityEngine.UI;

namespace Kanamori
{
    public class SelectButton : MonoBehaviour
    {
        /// <summary>
        /// 关键点
        /// </summary>
        public KeyPoint keyPoint;
        /// <summary>
        /// 路径
        /// </summary>
        public Road road;
        /// <summary>
        /// 目的地
        /// </summary>
        public Transform target;
        void Start()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameObject.Find("SceneMaster").SendMessage("SelectButtonClicked", transform);
            });
        }
    }
}

