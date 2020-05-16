using UnityEngine;
using UnityEngine.UI;

namespace Kanamori
{
    public class SelectButton : MonoBehaviour
    {
        public KeyPoint keyPoint;
        void Start()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameObject.Find("SceneMaster").SendMessage("SelectButtonClicked", transform);
            });
        }
    }
}

