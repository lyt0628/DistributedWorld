
using GameLib.DI;
using QS.Api.Chara.Service;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Service;
using QS.Api.Skill;
using QS.Skill;
using UnityEngine;
using UnityEngine.InputSystem;
using QS.Executor;
using QS.Player.Instrs;
using QS.PlayerControl;

namespace QS.Player
{
    [Scope(Value = ScopeFlag.Sington ,Lazy = false)]
    public class PlayerControlCallbacks : PlayerControls.IPlayerActions
    {
        readonly IPlayerData playerChara;
        readonly ICharaInsrFactory characterInsructionFactory;

        IInstruction tapInstr;

        [Injected]
        public PlayerControlCallbacks(PlayerControls playerControls, IPlayerData playerChara, ICharaInsrFactory characterInsructionFactory, ISkillInstrFactory skillInstrFactory)
        {
            this.playerChara = playerChara;
            this.characterInsructionFactory = characterInsructionFactory;
            
            playerControls.Player.AddCallbacks(this);

            SkillGlobal.Instance.OnReady.AddListener(() =>
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

        public void OnView(InputAction.CallbackContext context)
        {
            
        }
    }
}