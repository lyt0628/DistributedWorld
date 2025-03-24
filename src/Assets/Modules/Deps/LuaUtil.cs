

using NLua;
using System;
using System.Threading.Tasks;

namespace QS.Deps
{


    public static class LuaUtil
    {




        private const string COROUTINE_RESUME_PATH = "coroutine.resume";
        private const string COROUTINE_STATUS_PATH = "coroutine.status";

        public static void CO_Resume(Lua lua, object co, params object[] args)
        {
            var resumeFunc = lua.GetFunction(COROUTINE_RESUME_PATH);
            resumeFunc.Call(co, args);
        }

        public static string CO_Status(Lua lua, object co)
        {
            var statusFunc = lua.GetFunction(COROUTINE_STATUS_PATH);
            return statusFunc.Call(co)[0] as string;
        }

        public static Action<object, TParams> DefineAsync<TParams, TResult>(Lua lua, Func<TParams, Task<TResult>> func)
        {
            return (co, args) =>
            {
                var t = func(args);

                Task.Run(async () =>
                {

                    //Debug.Log(CO_Status(lua, co));
                    await t;
                    //Debug.Log(CO_Status(lua, co));
                    CO_Resume(lua, co, t.Result);
                    //Debug.Log(CO_Status(lua, co));
                });
            };
        }


        public static Action<object, object[]> DefineAsync(Lua lua, Func<object[], Task<object[]>> func)
        {
            return new Action<object, object[]>((co, args) =>
            {
                Task.Run(async () =>
                {
                    var t = func(args);
                    await t;
                    CO_Resume(lua, t.Result);
                });
            });
        }

        public static void EvalStringInSandbox(Lua lua, LuaTable sanbox, string code)
        {
            var oldEnv = lua["_G"];
            lua["_G"] = sanbox;
            lua.DoString(code);
            lua["_G"] = oldEnv;
        }
    }
}