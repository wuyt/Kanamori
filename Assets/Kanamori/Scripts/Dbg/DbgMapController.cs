using UnityEngine;
using UnityEngine.UI;

namespace Kanamori.Dbg
{
    public class DbgMapController : MonoBehaviour
    {
        /// <summary>
        /// 游戏控制
        /// </summary>
        private GameController game;
        /// <summary>
        /// 显示文本
        /// </summary>
        public Text text;
        void Start()
        {
            game = FindObjectOfType<GameController>();
        }
        /// <summary>
        /// 保存地图
        /// </summary>
        public void SaveMap()
        {
            if (game)
            {
                game.SaveMapID("DbgMapTest");
                game.SaveMapName(game.inputName);
                text.text = "地图保存成功。";
            }
        }
        /// <summary>
        /// 返回
        /// </summary>
        public void Back()
        {
            if (game)
            {
                game.BackDbgMenu();
            }
        }
    }
}

