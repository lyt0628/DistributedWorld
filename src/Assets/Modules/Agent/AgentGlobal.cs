


using GameLib.DI;
using QS.Api.Common;
using QS.Common;

namespace QS.Agent
{
    public class AgentGlobal : ModuleGlobal<AgentGlobal>
    {
        public AgentGlobal()
        {
            LoadOp = new AgentModuleLoadOp(this);
        }
        protected override AsyncOpBase<IModuleGlobal> LoadOp { get; }

        class AgentModuleLoadOp : AsyncOpBase<IModuleGlobal>
        {
            readonly AgentGlobal m_Module;

            public AgentModuleLoadOp(AgentGlobal module)
            {
                m_Module = module;
            }

            protected override async void Execute()
            {
                m_Module.Inject(this);
                await DepsGlobal.Instance.LoadHandle.Task;
                //                lua.DoString(@"import('QS.Agent', 'QS.Agent')");
                //                lua.DoString(@"
                //                    P = {}
                //                    P.__index = p
                //                    setmetatable(P, {__index = Printer})

                //                    function P:new()
                //                        local o = setmetatable({}, self)
                //                        return o
                //                    end

                //                    function P:Print()
                //                        print('lyt0628')
                //                    end
                //");
                //                var ctor = lua.GetFunction("P.new");
                //                var i = (LuaTable)ctor.Call(lua["P"])[0];
                //                var p = i["Print"] as LuaFunction;
                //                Debug.Log(p.Call());
                Complete(m_Module);
            }
        }

        protected override void DoSetupBinding(IDIContext globalContext, IDIContext moduleContext)
        {

        }
    }

}