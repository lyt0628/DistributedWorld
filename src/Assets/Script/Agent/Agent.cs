using GameLib.DI;
using QS.Api.Chara.Service;
using QS.Api.Executor.Service;
using QS.Chara.Domain;
using QS.GameLib.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Agent
{
    public class Agent : Character
    {

        [Injected]
        readonly ICharaAblityFactory instructionHandlerFactory;

        [Injected]
        readonly ISteering steering;

        private void Start()
        {
            AgentGlobal.Instance.DI.Inject(this);
            var animator = GetComponent<Animator>();
            AddLast(MathUtil.UUID(), 
                    instructionHandlerFactory.Translate(this, transform, animator));
        }

        private void Update()
        {
            Execute(steering.GetTranslateInstr());
        }
    }

}