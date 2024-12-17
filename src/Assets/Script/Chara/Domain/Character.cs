using GameLib.DI;
using QS.Api.Chara;
using QS.Api.Common;
using QS.Api.Executor.Domain;
using QS.Executor;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Rx.Relay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace QS.Chara.Domain
{
    public class Character : ExecutorBehaviour, ICharacter    
    {


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
        }
    }

}