using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kanamori
{
    /// <summary>
    /// 游戏控制器
    /// </summary>
    public class GameController : MonoBehaviour
    {

        private static GameController instance = null;
        public string inputName;

        void Awake()
        {
            //实现单实例
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (this != instance)
            {
                Destroy(gameObject);
                return;
            }
        }
        /// <summary>
        /// 获取稀疏空间地图名称
        /// </summary>
        /// <returns>地图名称</returns>
        public string GetMapName()
        {
            return PlayerPrefs.GetString("MapName", "");
        }
        /// <summary>
        /// 保存稀疏空间地图名称
        /// </summary>
        /// <param name="mapName">地图名称</param>
        public void SaveMapName(string mapName)
        {
            PlayerPrefs.SetString("MapName", mapName);
        }
        /// <summary>
        /// 获取稀疏空间地图ID
        /// </summary>
        /// <returns>地图ID</returns>
        public string GetMapID()
        {
            return PlayerPrefs.GetString("MapID", "");
        }
        /// <summary>
        /// 保存稀疏空间地图ID
        /// </summary>
        /// <param name="mapID">稀疏空间地图ID</param>
        public void SaveMapID(string mapID)
        {
            PlayerPrefs.SetString("MapID", mapID);
        }
        /// <summary>
        /// 删除地图
        /// </summary>
        public void DelMap(){
            PlayerPrefs.DeleteKey("MapID");
            PlayerPrefs.DeleteKey("MapName");
        }
        /// <summary>
        /// 返回调试用菜单
        /// </summary>
        public void BackDbgMenu(){
            SceneManager.LoadScene("DbgMenu");
        }
    }
}

