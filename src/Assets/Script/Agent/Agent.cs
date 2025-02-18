using GameLib.DI;
using QS.Api.Chara.Service;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Service;
using QS.Api.Skill;
using QS.Chara.Domain;
using QS.Common.Util;
using QS.GameLib.Util;
using QS.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace QS.Agent
{
    public class Agent : Character
    {

        [Injected]
        readonly ICharaAblityFactory instructionHandlerFactory;
        [Injected]
        readonly ISkillAblityFactory skillAblityFactory;
        [Injected]
        ISkillInstrFactory skillInstrFactory;
        [Injected]
        readonly Steering steering;
        IInstruction tapInstr;
        private void Start()
        {
            
            AgentGlobal.Instance.DI.Inject(this);

            SkillGlobal.Instance.Messager.AddListener(SkillGlobal.MSG_READY, (_) =>
            {
                ISkillRepo skillRepo = SkillGlobal.Instance.GetInstance<ISkillRepo>();
                AddLast(MathUtil.UUID(),
                    skillAblityFactory.Create(this, skillRepo.GetSkill("00002")));

               
                var sk = skillRepo.GetSkill("00002");
                tapInstr = skillInstrFactory.Create(sk);
            });


            steering.robot = transform;
            var animator = GetComponent<Animator>();
            AddLast(MathUtil.UUID(), 
                    instructionHandlerFactory.CharaControl(this));

            var h = Addressables.LoadAssetAsync<GameObject>("RustSword");
            h.Completed += (h) =>
            {
                var go = GameObject.Instantiate(h.Result);

                // 這也是變化點，必須得封裝起來
                var originalMesh = transform.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
                var mesh = go.transform.Find("RustSword");
                var skin = mesh.GetComponent<SkinnedMeshRenderer>();
                var skeleton = go.transform.Find("Skeleton");

                AnimationUtil.RebindSkeleton(gameObject.transform, originalMesh,
                                            "Skeleton", skeleton.transform, skin.bones);

                mesh.transform.parent = transform;
                GetComponent<Animator>().Rebind();
            };
        }

        private void Update()
        {

            var instr = steering.GetTranslateInstr();
            if (instr.Vertical == 0 && instr.Horizontal == 0 && Input.GetKeyDown(KeyCode.V))
            {
                    Execute(tapInstr);
                
            }
            else
            {
                Execute(instr);
            }
        }
    }

}