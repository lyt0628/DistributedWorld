



using GameLib.DI;
using QS.Api.Common;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Common.Util;
using QS.Skill.Domain.Handler;
using System.Collections.Generic;
using System.Linq;
using Tomlet;
using Tomlet.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

namespace QS.Skill.SimpleSkill
{
    /// <summary>
    /// 擁有實例化預製體能力的子處理器,
    /// 具體的技能是從配置文件中讀取的，那麼，
    /// 誰負責讀取配置文件，實例化技能處理器，並且添加子處理器呢？
    /// 顯然不需要一次性儲存所有技能處理器， 按需讀取，然後釋放
    /// 這應當認爲是工廠，還是 數據存儲呢？應當是工廠
    /// 我前面的設計是錯誤的，增加了不必要的複雜度
    /// 先去修改它 
    /// </summary>
    [Scope(Value =ScopeFlag.Sington, Lazy =false)]
    class MountSubHandler : SimpleSkillSubHandlerAdapter
    {
        public const string resourceKey = nameof(MountSubHandler);

        [Injected]
        public MountSubHandler(ISubHandlerRegistry registry) {
            registry.Register("Mount", this);
        }

        public override void OnParseConfiguration(ISimpleSkill skill, TomlTable skillTable)
        {
            if (!skillTable.TryGetValue("Mount", out var mountValue))
            {
                return;
            }
            var mountTable = mountValue as TomlTable;

            var prefabMap = new Dictionary<SimpleSkillStage, List<MountPrefab>>();
            ParseStage(SimpleSkillStage.Precast);
            ParseStage(SimpleSkillStage.Casting);
            ParseStage(SimpleSkillStage.Postcast);
            skill.ResourceMap[resourceKey] = prefabMap;

            void ParseStage(SimpleSkillStage stage)
            {
                if (mountTable.TryGetValue(stage.ToString(),
                            out var precastEnterValue))
                {
                    List<MountPrefab> prefabs = new();
                    var precastTableArray = ((TomlTable)precastEnterValue).GetArray("Prefabs");
                    foreach (TomlTable precastTable in precastTableArray.Cast<TomlTable>())
                    {
                        var prefab = TomletMain.To<MountPrefab>(precastTable);
                        prefab.Stage = stage;
                        prefabs.Add(prefab);
                       
                    }
                    prefabMap.Add(stage, prefabs);
                }
            }
        }

        public override void PreLoad(Character chara, ISimpleSkillAbility handler)
        {
            if (!handler.Skill.ResourceMap.TryGetValue(resourceKey, out var resource))
            {
                return;
            }
            var prefabMap = resource as Dictionary<SimpleSkillStage, List<MountPrefab>>;
            foreach (var prefab in prefabMap.SelectMany(e => e.Value))
            {
                prefab.Initialize();
            }

        }


        public override void OnPrecast(Character chara, ISimpleSkillAbility handler)
        {
            MountPrefab(chara, handler, SimpleSkillStage.Precast);
        }
        public override void OnCasting(Character chara, ISimpleSkillAbility handler)
        {
            MountPrefab(chara, handler, SimpleSkillStage.Casting);
        }
        public override void OnPostcast(Character chara, ISimpleSkillAbility handler)
        {
            MountPrefab(chara, handler, SimpleSkillStage.Postcast);
        }
        private static void MountPrefab(Character chara, ISimpleSkillAbility handler, SimpleSkillStage stage)
        {
            if (!handler.Skill.ResourceMap.TryGetValue(resourceKey, out var resource))
            {
                return;
            }
            var prefabMap = resource as Dictionary<SimpleSkillStage, List<MountPrefab>>;
            if (prefabMap.TryGetValue(stage, out var prefabs))
            {
                prefabs.ForEach(e => MountPrefab0(chara, e));
            }
        }

        static void MountPrefab0(Character chara, MountPrefab prefab)
        {
            if (prefab.ResourceStatus == ResourceInitStatus.Started)
            {
                DoMountPrefab(chara, prefab);
            }
            else
            {
                Debug.LogWarning($"Resource {prefab.Address} Load too slow!!!");
            }
        }

        static void DoMountPrefab(Character chara, MountPrefab prefab)
        {
            GameObject gameObject;
            Transform mountPoint = prefab.MountPoint switch
            {
                CharaMountPoint.ORIGIN => chara.transform,
                _ => GameObjectUtil.FindChild(chara.transform, prefab.MountPoint)
            };
            Assert.IsNotNull(mountPoint, $"Mount point {prefab.MountPoint} does not exists!!!");

            if (prefab.InWorldSpace)
            {
                gameObject = GameObject.Instantiate(prefab.Prefab,
                     mountPoint.position + prefab.Offset,
                     prefab.Rotation);
            }
            else
            {
                gameObject = GameObject.Instantiate(prefab.Prefab,
                    mountPoint.position, prefab.Rotation, chara.transform);
                gameObject.transform.parent = mountPoint;
                gameObject.transform.localPosition += prefab.Offset;
            }
            gameObject.transform.localScale = prefab.Scale;
            if (prefab.AutoDestroy)
            {
                GameObject.Destroy(gameObject, prefab.DestroyDelay);
            }
        }
    }
}