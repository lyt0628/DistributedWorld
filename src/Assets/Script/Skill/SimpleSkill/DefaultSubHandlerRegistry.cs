


using QS.Api.Skill.Domain;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace QS.Skill.SimpleSkill
{
    class DefaultSubHandlerRegistry : ISubHandlerRegistry
    {
        readonly Dictionary<string, ISimpleSkillSubHandler> handlerMap;
        public DefaultSubHandlerRegistry()
        {
            handlerMap = new();
        }

        public ISimpleSkillSubHandler GetSubHandler(string name)
        {
            if(handlerMap.TryGetValue(name, out var subHandler))
            {
                return subHandler;
            }
            else
            {
                throw new System.ArgumentException($"The SimpleSkillSubHandler with name: {name} does not exists!!!");
            }
            
        }

        public void Register(string name, ISimpleSkillSubHandler handler)
        {
            handlerMap[name] = handler;
        }
    }

}