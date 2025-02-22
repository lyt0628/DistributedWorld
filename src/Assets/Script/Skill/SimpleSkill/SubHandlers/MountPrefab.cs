


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
    /// 記錄單獨一個實例化預製體的信息, 從配置文件中讀取
    /// 到了子處理器，以及不是主要的設計層次了，
    /// 直接使用具體類來封裝數據更合適
    /// </summary>
    public class MountPrefab : IResourceInitializer
    {
        /// <summary>
        /// 索引預製體的 key， 使用addressables 的話，就是地址
        /// 直接要 address會更好，不知道爲什麼，就是這麼感覺
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 是否生成在世界空間，否則生成在模型空間
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