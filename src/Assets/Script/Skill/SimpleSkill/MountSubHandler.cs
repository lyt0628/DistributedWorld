



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
    /// ���Ќ������A�u�w��������̎����,
    /// ���w�ļ����Ǐ������ļ����xȡ�ģ����N��
    /// �lؓ؟�xȡ�����ļ�������������̎�������K�������̎�����أ�
    /// �@Ȼ����Ҫһ���ԃ������м���̎������ �����xȡ��Ȼ��ጷ�
    /// �@�����J���ǹ��S��߀�� �����惦�أ������ǹ��S
    /// ��ǰ����OӋ���e�`�ģ������˲���Ҫ���}�s��
    /// ��ȥ�޸��� 
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

            var s = SimpleSkillStage.PrecastEnter.ToString();
            if (mountTable.TryGetValue(s,
                        out var precastEnterValue))
            {
                var precastTableArray = ((TomlTable)precastEnterValue).GetArray("Prefabs");
                foreach (TomlTable precastTable in precastTableArray.Cast<TomlTable>())
                {
                    var prefab = TomletMain.To<MountPrefab>(precastTable);
                    skill.ResourceMap[resourceKey] = prefab;
                }
            }
        }

        public override void PreLoad(Character chara, ISimpleSkillHandler handler)
        {
            if (!handler.Skill.ResourceMap.TryGetValue(resourceKey, out var resource))
            {
                return;
            }
            var prefab = resource as MountPrefab;
            prefab.Initialize();
        }

        public override void OnCastingEnter(Character chara, ISimpleSkillHandler handler)
        {
            if (!handler.Skill.ResourceMap.TryGetValue(resourceKey, out var resource))
            {
                return;
            }
            var prefab = resource as MountPrefab;
            MountPrefab(chara, prefab);
        }

        static void MountPrefab(Character chara, MountPrefab prefab)
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