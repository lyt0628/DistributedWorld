


using GameLib.DI;
using QS.Api.Common.Util.Detector;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Common.Util;
using System.Collections.Generic;
using System.Linq;
using Tomlet.Models;
using UnityEngine;
using UnityEngine.Assertions;

namespace QS.Skill.SimpleSkill
{
    /// <summary>
    /// 這一層級的子攻擊類確定攻擊檢測的方式
    /// </summary>
    [Scope(Value = ScopeFlag.Sington, Lazy = false)]
    class CollideAttackSubHandler : BaseAttackSubHandler
    {
        [Injected]
        readonly IDetectorFactory detectorFactory;

        const string collideAttackResourceKey = nameof(collideAttackResourceKey);
        protected override IDetector HitDetector => _hitDetector;
        IDetector _hitDetector;
        ISpanDetector[] collideDetectors;

        [Injected]
        public CollideAttackSubHandler(ISubHandlerRegistry registry)
        {
            registry.Register("CollideAttack", this);
        }

        public override void OnParseConfiguration(ISimpleSkill skill, TomlTable skillTable)
        {
            base.OnParseConfiguration(skill, skillTable);

            var colliders = skillTable.GetArray("Colliders")
                .Select(v => v.StringValue);
            skill.ResourceMap[collideAttackResourceKey] = colliders;
        }

        public override void OnCasting(Character chara, ISimpleSkillAbility handler)
        {
            var colliders = handler.Skill.ResourceMap[collideAttackResourceKey] as IEnumerable<string>;
            collideDetectors = colliders
                .Select(name =>
                {
                    var c = GameObjectUtil
                    .FindChild(chara.transform, name)
                    .GetComponent<Collider>();
                    var d = detectorFactory.Collide(c, CollideStage.Enter);
                    d.Enable();
                    return d;
                }).ToArray();

            _hitDetector = detectorFactory.Group(collideDetectors);

            base.OnCasting(chara, handler);
        }

        public override void OnPostcast(Character chara, ISimpleSkillAbility handler)
        {
            base.OnPostcast(chara, handler);
            collideDetectors = null;
        }

    }
}