using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Kanamori.Dbg
{
    public class DbgModelController : MonoBehaviour
    {
        /// <summary>
        /// 游戏控制
        /// </summary>
        private GameController game;
        /// <summary>
        /// 摄像头前方
        /// </summary>
        public Transform frontCamera;
        /// <summary>
        /// 稀疏空间地图
        /// </summary>
        public Transform ssMap;
        /// <summary>
        /// 添加的物体
        /// </summary>
        public GameObject blueBox;
        /// <summary>
        /// 添加界面
        /// </summary>
        public GameObject addUI;
        /// <summary>
        /// 保存界面
        /// </summary>
        public GameObject saveUI;
        /// <summary>
        /// UI控制游戏对象
        /// </summary>
        private UIControlObject uiControl;
        /// <summary>
        /// 显示信息文本
        /// </summary>
        public Text textShow;

        void Start()
        {
            game = FindObjectOfType<GameController>();
            uiControl = FindObjectOfType<UIControlObject>();
            Close();
            Load();
        }
        /// <summary>
        /// 返回菜单
        /// </summary>
        public void Back()
        {
            if (game)
            {
                game.BackDbgMenu();
            }
        }
        /// <summary>
        /// 添加物体
        /// </summary>
        public void Add()
        {
            Instantiate(blueBox, frontCamera.position, Quaternion.identity, ssMap);
        }
        /// <summary>
        /// 关闭保存界面
        /// </summary>
        public void Close()
        {
            addUI.SetActive(true);
            saveUI.SetActive(false);
        }
        void Update()
        {
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
        /// <summary>
        /// 点击物体处理
        /// </summary>
        /// <param name="ray"></param>
        private void TouchedObject(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                addUI.SetActive(false);
                saveUI.SetActive(true);
                uiControl.SetSelected(hit.transform);
                textShow.text = "选中物体";
            }
        }

        // void Update()
        // {
        //     if (Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject())
        //     {
        //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //         if (Physics.Raycast(ray, out RaycastHit hit))
        //         {
        //             addUI.SetActive(false);
        //             saveUI.SetActive(true);
        //         }
        //     }
        // }

        /// <summary>
        /// 保存物体
        /// </summary>
        public void Save()
        {
            string[] jsons = new string[ssMap.childCount - 1];
            for (int i = 0; i < ssMap.childCount; i++)
            {
                if (ssMap.GetChild(i).name != "PointCloudParticleSystem")
                {
                    DynamicObject dynamicObject = new DynamicObject();
                    dynamicObject.position = ssMap.GetChild(i).localPosition;
                    dynamicObject.rotation = ssMap.GetChild(i).localEulerAngles;
                    dynamicObject.scale = ssMap.GetChild(i).localScale;
                    jsons[i - 1] = JsonUtility.ToJson(dynamicObject);
                }
            }
            if (game)
            {
                game.SaveDynamicObject(jsons);
                textShow.text = "保存" + (ssMap.childCount - 1) + "个游戏对象";
            }
        }
        /// <summary>
        /// 删除物体
        /// </summary>
        public void Delete()
        {
            var go = uiControl.selected.gameObject;
            uiControl.ClearSelected();
            Destroy(go);
            textShow.text = "删除选中物体，请保存结果。";
        }
        /// <summary>
        /// 加载物体
        /// </summary>
        private void Load()
        {
            if (game)
            {
                var list = game.LoadDynamicObject();
                foreach (var item in list)
                {
                    var dynamicObject = JsonUtility.FromJson<DynamicObject>(item);
                    var tf = Instantiate(blueBox, ssMap).transform;
                    tf.localPosition = dynamicObject.position;
                    tf.localEulerAngles = dynamicObject.rotation;
                    tf.localScale = dynamicObject.scale;
                }
            }
        }
    }
}

