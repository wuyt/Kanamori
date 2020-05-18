using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Kanamori
{
    public class RoadController : MonoBehaviour
    {
        private GameController game;
        public Dropdown dpdStart;
        public Dropdown dpdEnd;
        public Text textInfo;
        public Transform svContent;
        public SelectButton prefab;
        private List<KeyPoint> keyPoints;
        private Transform selected;
        public Button btnDelete;

        void Start()
        {
            game = FindObjectOfType<GameController>();
            keyPoints = new List<KeyPoint>();
            BindDropdown();
            btnDelete.interactable = false;
            Load();
        }
        public void BackDbgMenu()
        {
            game.BackDbgMenu();
        }
        /// <summary>
        /// 绑定下拉列表
        /// </summary>
        private void BindDropdown()
        {
            var list = game.LoadKeyPoint();
            foreach (var item in list)
            {
                KeyPoint keyPoint = JsonUtility.FromJson<KeyPoint>(item);
                dpdStart.options.Add(new Dropdown.OptionData(keyPoint.name));
                dpdEnd.options.Add(new Dropdown.OptionData(keyPoint.name));
                dpdStart.captionText.text = dpdStart.options[0].text;
                dpdEnd.captionText.text = dpdEnd.options[0].text;
                keyPoints.Add(keyPoint);
            }
        }
        public void Add()
        {
            SelectButton btn = Instantiate(prefab, svContent);
            btn.road.startName = dpdStart.captionText.text;
            btn.road.endName = dpdEnd.captionText.text;
            btn.road.startPosition = GetPositionByName(btn.road.startName);
            btn.road.endPosition = GetPositionByName(btn.road.endName);
            btn.GetComponentInChildren<Text>().text = btn.road.startName + "<===>" + btn.road.endName;
            textInfo.text = "添加完成。";
        }
        private Vector3 GetPositionByName(string pName)
        {
            foreach (var kp in keyPoints)
            {
                if (kp.name == pName)
                {
                    return kp.position;
                }
            }
            return Vector3.zero;
        }
        public void Save()
        {
            string[] jsons = new string[svContent.childCount];
            for (int i = 0; i < svContent.childCount; i++)
            {
                jsons[i] = JsonUtility.ToJson(svContent.GetChild(i).GetComponent<SelectButton>().road);
            }
            game.SaveRoad(jsons);
            textInfo.text = "保存完成。";
        }
        public void SelectButtonClicked(Transform btn)
        {
            selected = btn;
            textInfo.text = btn.GetComponentInChildren<Text>().text;
            btnDelete.interactable = true;
        }
        public void Delete()
        {
            Destroy(selected.gameObject);
            textInfo.text = "删除完成。";
            btnDelete.interactable = false;
        }
        private void Load()
        {
            var list = game.LoadRoad();
            foreach (var item in list)
            {
                var btn = Instantiate(prefab, svContent);
                btn.road = JsonUtility.FromJson<Road>(item);
                btn.GetComponentInChildren<Text>().text = btn.road.startName + "<===>" + btn.road.endName;
            }
        }
    }
}

