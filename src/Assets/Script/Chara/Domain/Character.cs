using GameLib.DI;
using QS.Api.Character;
using QS.Api.Common;
using QS.Api.Executor.Domain;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Rx.Relay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace QS.Chara.Domain
{
    public class Character : MonoBehaviour, ICharacter    
    {

        [Injected]
        readonly IRelayExecutor executor;

        [Injected(Name =Api.Common.DINames.GameLib_Message_Messager)]
        readonly IMessager _messager;
        public IMessager Messager => _messager;

        void Start()
        {
            CharaGlobal.Instance.DI.Inject(this);
        }
      
        public void AnimAware(string param)
        {
            Messager.Boardcast(param, Msg0.Instance);
            Debug.Log(param);
        }

        public void Execute(Relay<IInstruction> instructions)
        {
            executor.Execute(instructions);
        }

        public void Execute(IInstruction instruction)
        {
            executor.Execute(instruction);
        }

        public void AddFirst(string name, IInstructionHandler handler)
        {
            executor.AddFirst(name, handler);
        }

        public void AddLast(string name, IInstructionHandler handler)
        {
            executor.AddLast(name, handler);
        }

        public void AddBefore(string baseName, string name, IInstructionHandler handler)
        {
            executor.AddBefore(baseName, name, handler);
        }

        public void AddAfter(string baseName, string name, IInstructionHandler handler)
        {
            executor.AddAfter(baseName, name, handler);
        }

        public void Remove(string name)
        {
            executor.Remove(name);
        }

        public void Remove(IInstructionHandler handler)
        {
            executor.Remove(handler);
        }
    }

}