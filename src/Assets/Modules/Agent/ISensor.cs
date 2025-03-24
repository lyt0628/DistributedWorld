


using UnityEngine;

namespace QS.Agent
{
    /// <summary>
    /// �����֪��ģ�飬�����ǻ���ͼ�ĸ�֪���ǻ�����ײ��ĸ�֪
    /// </summary>
    public interface ISensor
    {
        /// <summary>
        /// Sensor ��⵽�ĵ���
        /// </summary>
        /// <returns></returns>
        Transform[] Enemies { get; }
        Transform Enemy { get; }
        bool EnemyFound { get; }


    }
}