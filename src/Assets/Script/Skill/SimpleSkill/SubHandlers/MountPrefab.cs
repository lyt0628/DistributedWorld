


using QS.Api.Common;
using QS.Api.Skill.Domain;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace QS.Skill.SimpleSkill
{
    /// <summary>
    /// ӛ䛆Ϊ�һ���������A�u�w����Ϣ, �������ļ����xȡ
    /// ������̎�������Լ�������Ҫ���OӋ�Ӵ��ˣ�
    /// ֱ��ʹ�þ��w���b���������m
    /// </summary>
    public class MountPrefab : IResourceInitializer
    {
        /// <summary>
        /// �����A�u�w�� key�� ʹ��addressables ��Ԓ�����ǵ�ַ
        /// ֱ��Ҫ address�����ã���֪����ʲ�N�������@�N���X
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// �Ƿ�������������g����t������ģ�Ϳ��g
        /// </summary>

        public bool InWorldSpace { get; set; } = true;
     
        public GameObject Prefab { get; private set; } 
        public SimpleSkillStage Stage { get; set; }
        public string MountPoint { get; set; } = CharaMountPoint.ORIGIN;
        public bool AutoDestroy => DestroyDelay > 0;
        public float DestroyDelay { get; set; } = 0;
        public Vector3 Offset { get; set; } = Vector3.zero;
        public Quaternion Rotation { get; set; } = Quaternion.identity;
        public Vector3 Scale { get; set; } = Vector3.one;


        public ResourceInitStatus ResourceStatus { get; private set; } = ResourceInitStatus.Initializing;

        public UnityEvent OnReady { get; } = new();

        public void Initialize()
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(Address);
            handle.Completed += (h) => {
                Prefab = h.Result;
                ResourceStatus = ResourceInitStatus.Started;
                OnReady.Invoke();
            };

        }


    }
}