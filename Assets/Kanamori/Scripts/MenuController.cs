using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Kanamori
{
    public class MenuController : MonoBehaviour
    {
        /// <summary>
        /// 添加地图按钮
        /// </summary>
        public Button btnAdd;
        /// <summary>
        /// 删除地图按钮
        /// </summary>
        public Button btnDel;
        /// <summary>
        /// 地图名称文本输入框
        /// </summary>
        public InputField input;
        /// <summary>
        /// 游戏控制
        /// </summary>
        private GameController game;
        void Start()
        {
            game = FindObjectOfType<GameController>();
            UISettings();
        }
        /// <summary>
        /// 退出应用
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        /// <summary>
        /// 删除地图
        /// </summary>
        public void DeleteMap()
        {
            game.DelMap();
            UISettings();
        }
        /// <summary>
        /// 添加地图
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        public void AddMap(string sceneName)
        {
            if (input.text.Length > 0)
            {
                LoadScene(sceneName);
            }
        }
        /// <summary>
        /// UI显示控制
        /// </summary>
        private void UISettings()
        {
            if (game.GetMapID().Length == 0)
            {
                btnAdd.gameObject.SetActive(true);
                btnDel.gameObject.SetActive(false);
                input.text = "";
                input.interactable = true;
            }
            else
            {
                btnAdd.gameObject.SetActive(false);
                btnDel.gameObject.SetActive(true);
                input.text = game.GetMapName();
                input.interactable = false;
            }
        }
    }
}

