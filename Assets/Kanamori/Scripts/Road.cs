using UnityEngine;
using System;

namespace Kanamori
{
    /// <summary>
    /// 路径
    /// </summary>
    [Serializable]
    public class Road
    {
        /// <summary>
        /// 起点名称
        /// </summary>
        public string startName;
        /// <summary>
        /// 起点坐标
        /// </summary>
        public Vector3 startPosition;
        /// <summary>
        /// 终点名称
        /// </summary>
        public string endName;
        /// <summary>
        /// 终点坐标
        /// </summary>
        public Vector3 endPosition;
    }
}

