using QS.Api.Executor.Domain;
using QS.Api.WorldItem.Domain;
using QS.Combat;
using QS.Common;
using QS.GameLib.Pattern.Message;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace QS.Chara.Domain
{

    public class CharaAnimMsg : UnityEvent<string> { }



    /// <summary>
    /// �[����}�s���ҿ����X���Լ������á����ǣ���ȥ�����lҲ��֪������Ч�����N��
    /// ���ܺͽ�ɫ������ȫ�ֿ���
    /// ��ɫ������Ҫ�����ɫ�ܸ�ʲô������ǰ�ECS ���� ��ɫλ���²㣬
    /// �����෴�������úڰ�ģʽ�����������Ҫ���ϲ���߸�������һ������
    /// NPC�ͽ�ɫ��Ϊ������������������
    /// 
    /// 
    /// Character ��ô�����أ���Ҫ���ඨ�廹���ý�����������
    /// 
    /// Chara ��һ�����ģ�飬����߻ᶨ����ϵ�����ģ�͡�
    /// һЩ�ϲ�������Ҫ���������Լ��
    /// </summary>
    public abstract class Character : MonoBehaviour
    {
        public abstract GameUnit Unit { get; }
        /// <summary>
        /// ��ɫʹ�úڰ�ģʽ��������
        /// </summary>
        public IHandlerGroup HandlerGroup => m_HanderGroup;
        IHandlerGroup m_HanderGroup;

        /// <summary>
        /// ˭���������ָ��ת���������ӡ��������û�Agent��Chara��Agent����Ҫת��ָ��
        /// Chara�����ж����������ⲿ��ӣ����ǡ�
        /// ���ǣ�Ψ������̳������Ų�����ʧ������������
        /// </summary>
        public IInstructionConverter InstructionConverter => m_InstructionConverter;
        IInstructionConverter m_InstructionConverter;

        /// <summary>
        /// ��ɫ���ⷢ����Ϣ���ṩ�Լ���״̬�仯
        /// </summary>
        public IMessager Messager { get; } = new Messager();
        public IInstructionFiler InstrFiler => m_InstrFiler;
        FilterHandler m_InstrFiler;

        public virtual bool Frozen { get; set; } = false;
        /// <summary>
        /// �����ݽ������ص����ܵ���״̬
        /// </summary>
        public CharaAnimMsg OnAnimMsg { get; } = new();
        /// <summary>
        /// ��ɫһ����״̬����
        /// </summary>
        public CharaControlTemplate ControlFSM { get; private set; }
        /// <summary>
        /// ��ɫһ����ս��������û��ս��������NPC������ơ�
        /// </summary>
        public CombatorBehaviour Combator { get; private set; }
        /// <summary>
        /// ��ɫһ��ӵ�ж�������
        /// </summary>
        public Animator Animator { get; private set; }
        
        /// <summary>
        /// ��ɫ�����������л�������������ģʽ
        /// ��ɫ֪��������ôװ����ȥ�𣬽�ɫ��֪��.���������ControlFSM����
        /// ��������붯��ǿ��أ������ܲ���������ܵĴ�������Ҫ�׶�Ҳ�������ڶ���
        /// ����+RootMotion����˵CastSkill ������Cast�������⡣��û�취���������������Լ���ⱻ�����ߵ�״̬��
        /// �������ָ��Ĵ���Monster ӵ�к�Playerһ����Weapon
        /// </summary>
        public IWeapon ActiveWeapon { get; }


        /// <summary>
        /// ����������ṩ������״̬���ķ���
        /// ��ʵ�ֵ�һ����
        /// </summary>
        /// <param name="param"></param>
        public void AnimAware(string param)
        {
            OnAnimMsg.Invoke(param);
            Messager.Boardcast(param, UnitMsg.Instance);
        }

        protected virtual void Start()
        {
            ControlFSM = GetComponent<CharaControlTemplate>();
            Combator = GetComponent<CombatorBehaviour>();
            Animator = GetComponent<Animator>();
            m_InstrFiler = CharaGlobal.Instance.GetInstance<FilterHandler>();
            m_HanderGroup = CharaGlobal.Instance.GetInstance<IHandlerGroup>();
            m_InstructionConverter = CharaGlobal.Instance.GetInstance<IInstructionConverter>();

            Combator.OnDead.AddListener(() =>
            {
                Messager.Boardcast(CharaConstants.CHARA_DIE, IMessage.Unit);
                var msgBus = CharaGlobal.Instance.GetInstance<IMessager>(QS.Common.DINames.MsgBus);
                msgBus.Boardcast(CharaConstants.CHARA_DIE, new DeadMsg(Unit));
            });
        }


        public virtual void Execute(IInstruction instruction)
        {
            if (Frozen) return;
            // ��ת���������
            instruction = InstructionConverter.Convert(instruction);
            // ͨ��������������ָ�ʵ���޵�Ч��
            if (m_InstrFiler.TryHande(instruction)) return;
        


            // ר��ϵͳ������ָ��
            if (ControlFSM.TryHande(instruction)) return;
            if (Combator.TryHande(instruction)) return;
            if (m_HanderGroup.TryHande(instruction)) return;

            Debug.Log($"Insturction is miss to handle {instruction.GetType().Name}");
        }

    }
}
