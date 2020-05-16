using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kanamori.Dbg
{
    public class DbgKeyPointController : MonoBehaviour
    {
        public Transform cam;
        public Transform image;
        private GameController game;
        public GameObject uiBack;
        public GameObject uiMain;
        private Transform selected;
        public Button btnAdd;
        public Text textInfo;
        public InputField inputField;
        public Dropdown dropdown;
        public Transform svContent;
        public SelectButton prefab;
        public Button btnDelete;

        void Start()
        {
            game = FindObjectOfType<GameController>();
            Load();
            btnAdd.interactable = false;
            btnDelete.interactable = false;
            Close();
        }
        void Update()
        {
            cam.LookAt(image);
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (Input.GetMouseButtonDown(0)
                && !EventSystem.current.IsPointerOverGameObject())
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    TouchedObject(ray);
                }
            }
            else
            {
                if (Input.touchCount == 1
                && Input.touches[0].phase == TouchPhase.Began
                && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                    TouchedObject(ray);
                }
            }
        }

        private void TouchedObject(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                uiBack.SetActive(false);
                uiMain.SetActive(true);
                selected = hit.transform;
                btnAdd.interactable = true;
            }
        }

        public void Back()
        {
            game.BackDbgMenu();
        }
        public void Close()
        {
            uiMain.SetActive(false);
            uiBack.SetActive(true);
        }
        public void Add()
        {
            if (!string.IsNullOrEmpty(inputField.text) && selected != null)
            {
                SelectButton btn = Instantiate(prefab, svContent);

                btn.keyPoint.name = inputField.text;
                btn.keyPoint.position = selected.localPosition;
                btn.keyPoint.pointType = dropdown.value;

                btn.GetComponentInChildren<Text>().text = inputField.text;

                inputField.text = "";
                selected = null;
                textInfo.text = "添加完成。";
                btnAdd.interactable = false;
            }
        }
        public void Save()
        {
            string[] jsons = new string[svContent.childCount];
            for (int i = 0; i < svContent.childCount; i++)
            {
                jsons[i] = JsonUtility.ToJson(svContent.GetChild(i).GetComponent<SelectButton>().keyPoint);
            }
            game.SaveKeyPoint(jsons);
            textInfo.text = "保存完成。";
        }
        private void Load()
        {
            var list = game.LoadKeyPoint();
            foreach (var item in list)
            {
                SelectButton btn = Instantiate(prefab, svContent);
                btn.keyPoint = JsonUtility.FromJson<KeyPoint>(item);
                btn.GetComponentInChildren<Text>().text = btn.keyPoint.name;
            }
        }
        public void SelectButtonClicked(Transform btn)
        {
            selected = btn;
            textInfo.text = btn.GetComponentInChildren<Text>().text;
            btnDelete.interactable = true;
            btnAdd.interactable = false;
        }
        public void Delete()
        {
            Destroy(selected.gameObject);
            textInfo.text = "删除完成。";
            btnDelete.interactable = false;
        }
    }
}

