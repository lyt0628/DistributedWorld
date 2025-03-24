
using GameLib.DI;
using QS.Api.Chara.Service;
using QS.Api.Data;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Service;
using QS.Api.Skill;
using QS.Skill;
using UnityEngine;
using UnityEngine.InputSystem;
using QS.Executor;
using QS.PlayerControl.Instrs;

namespace QS.PlayerControl
{
    [Scope(Value = ScopeFlag.Sington ,Lazy = false)]
    public class PlayerControlCallbacks : PlayerControls.IPlayerActions
    {
        readonly IPlayerCharacterData playerChara;
        readonly ICharaInsrFactory characterInsructionFactory;
        readonly IPlayerLocationData playerLocation;
        IInstruction tapInstr;

        [Injected]
        public PlayerControlCallbacks(PlayerControls playerControls, IPlayerCharacterData playerChara, ICharaInsrFactory characterInsructionFactory, ISkillInstrFactory skillInstrFactory, IPlayerLocationData playerLocation)
        {
            this.playerChara = playerChara;
            this.characterInsructionFactory = characterInsructionFactory;
            this.playerLocation = playerLocation;

            playerControls.Player.AddCallbacks(this);
            SkillGlobal.Instance.Messager.AddListener(SkillGlobal.MSG_READY, (_) =>
            {
                ISkillRepo skillRepo = SkillGlobal.Instance.GetInstance<ISkillRepo>();
                var sk = skillRepo.GetSkill("00002");
                tapInstr = skillInstrFactory.Create(sk);
            });
        }

        public void OnDash(InputAction.CallbackContext context)
        {
        }

        /// <summary>
        /// �������߼��ɱ�������ʵ�֣�������Ҫ�����������Ϊ���ݳ�ȥ��
        /// ���� �� PlayerControls ��¶��ȥ
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                var hitColliders = Physics.OverlapSphere(
                playerChara.ActivedCharacter.transform.position, 1.5f);
                foreach (var hitCollider in hitColliders)
                {
                    var direction = hitCollider.transform.position - playerChara.ActivedCharacter.transform.position;
                    if(Vector3.Dot(playerChara.ActivedCharacter.transform.forward, direction) > .5f)
                    {
                        if (hitCollider.TryGetComponent<ExecutorBehaviour>(out var executor))
                        {
                            executor.Execute(new InteractInstr());
                        }
                    }

                }
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
        }

        public void OnMove(InputAction.CallbackContext context)
        {

        }

        public void OnTap(InputAction.CallbackContext context)
        {
            // ��߽��б�ѹ�Ļ��Ͳ����� Actor ��
            // ������ȴ���߼�������
            if (context.started)
            {
                if (playerChara.ActivedCharacter != null && tapInstr != null)
                {
                    playerChara.ActivedCharacter.Execute(tapInstr);
                }
            }

        }
    }
}