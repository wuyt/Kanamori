using UnityEngine;
using System;

namespace Kanamori
{
    /// <summary>
    /// 导航关键点
    /// </summary>
    [Serializable]
    public class KeyPoint
    {
        /// <summary>
        /// 位置
        /// </summary>
        public Vector3 position;
        /// <summary>
        /// 名称
        /// </summary>
        public string name;
        /// <summary>
        /// 类型：0=目的地；1=途经点
        /// </summary>
        public int pointType;
    }
}

