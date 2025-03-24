

using GameLib.DI;

using QS.Api.Combat.Service;
using QS.Api.Common;
using QS.Combat.Domain;
using QS.Combat.Service;
using QS.Common;
using QS.Common.Util;
using System;
using Tomlet;
using Tomlet.Exceptions;
using Tomlet.Models;

namespace QS.Combat
{

    public class CombatGlobal : ModuleGlobal<CombatGlobal>
    {
        public CombatGlobal()
        {
            LoadOp = new UnitAsyncOp<IModuleGlobal>(this);
        }
        protected override AsyncOpBase<IModuleGlobal> LoadOp { get; }

        [Injected]
        readonly IBuffFactory buffFactory;

        protected override void DoSetupBinding(IDIContext globalContext, IDIContext moduleContext)
        {

            globalContext
                .Bind<SkillFactory>()
                .Bind<CombatInstrFactory>()
                .Bind<BuffFactory>();

            moduleContext.Inject(this);
            RegisterTomlType();
        }


        void RegisterTomlType()
        {
            TomletMain.RegisterMapper<IBuff>(
                buff =>
                {
                    if (ReflectionUtil.IsChildOf<LinearBuff>(buff))
                    {
                        return TomletMain.ValueFrom(typeof(LinearBuff), buff);
                    }
                    throw new NotImplementedException();
                },
                tomlValue =>
                {
                    if (tomlValue is not TomlTable table)
                    {
                        throw new TomlTypeMismatchException(
                            typeof(TomlTable), tomlValue.GetType(), typeof(IBuff));
                    }
                    if (table.GetString(ConfigConstants.BUFF_TYPE).Equals(nameof(LinearBuff)))
                    {
                        return TomletMain.To<LinearBuff>(table);
                    }
                    throw new NotImplementedException();
                });

            TomletMain.RegisterMapper<LinearBuff>(
                buff =>
                {
                    var defaultSlop = ConfigConstants.LINEARBUFF_DEFAULT_SLOP;
                    var defaultIntercept = ConfigConstants.LINEARBUFF_DEFAULT_INTERCEPT;

                    TomlTable buffTable = new();
                    buffTable.PutValue(ConfigConstants.BUFF_TYPE, new TomlString(nameof(LinearBuff)));
                    if (buff.apSlop != defaultSlop) buffTable.PutValue(ConfigConstants.LINEARBUFF_AP_SLOP, new TomlDouble(buff.apSlop));
                    if (buff.apIntercept != defaultIntercept) buffTable.PutValue(ConfigConstants.LINEARBUFF_AP_INTERCEPT, new TomlDouble(buff.apSlop));
                    if (buff.mpSlop != defaultSlop) buffTable.PutValue(ConfigConstants.LINEARBUFF_MP_SLOP, new TomlDouble(buff.apSlop));
                    if (buff.mpIntercept != defaultIntercept) buffTable.PutValue(ConfigConstants.LINEARBUFF_MP_INTERCEPT, new TomlDouble(buff.apSlop));
                    if (buff.defSlop != defaultSlop) buffTable.PutValue(ConfigConstants.LINEARBUFF_DEF_SLOP, new TomlDouble(buff.apSlop));
                    if (buff.defIntercept != defaultIntercept) buffTable.PutValue(ConfigConstants.LINEARBUFF_DEF_INTERCEPT, new TomlDouble(buff.apSlop));
                    if (buff.mdefSlop != defaultSlop) buffTable.PutValue(ConfigConstants.LINEARBUFF_MDEF_SLOP, new TomlDouble(buff.apSlop));
                    if (buff.mdefIntercept != defaultIntercept) buffTable.PutValue(ConfigConstants.LINEARBUFF_MDEF_INTERCEPT, new TomlDouble(buff.apSlop));

                    return buffTable;
                },
                static tomlValue =>
                {
                    if (tomlValue is not TomlTable table)
                    {
                        throw new TomlTypeMismatchException(
                            typeof(TomlTable), tomlValue.GetType(), typeof(LinearBuff));
                    }
                    var defaultSlop = 1f;
                    var defaultIntercept = 0f;

                    float apSlop = defaultSlop;
                    float apIntercept = defaultIntercept;
                    float mpSlop = defaultSlop;
                    float mpIntercept = defaultIntercept;
                    float defSlop = defaultSlop;
                    float defIntercept = defaultIntercept;
                    float mdefSlop = defaultSlop;
                    float mdefIntercept = defaultIntercept;

                    if (table.TryGetValue(ConfigConstants.LINEARBUFF_AP_SLOP, out var apSlopVal))
                    {
                        apSlop = (float)((TomlDouble)apSlopVal).Value;
                    }
                    if (table.TryGetValue(ConfigConstants.LINEARBUFF_AP_INTERCEPT, out var apInterceptVal))
                    {
                        apIntercept = (float)((TomlDouble)apInterceptVal).Value;
                    }
                    if (table.TryGetValue(ConfigConstants.LINEARBUFF_MP_SLOP, out var mpSlopVal))
                    {
                        mpSlop = (float)((TomlDouble)mpSlopVal).Value;
                    }
                    if (table.TryGetValue(ConfigConstants.LINEARBUFF_MP_INTERCEPT, out var mpInterceptVal))
                    {
                        mpIntercept = (float)((TomlDouble)mpInterceptVal).Value;
                    }
                    if (table.TryGetValue(ConfigConstants.LINEARBUFF_DEF_SLOP, out var defSlopVal))
                    {
                        defSlop = (float)((TomlDouble)defSlopVal).Value;
                    }
                    if (table.TryGetValue(ConfigConstants.LINEARBUFF_DEF_INTERCEPT, out var defInterceptVal))
                    {
                        defIntercept = (float)((TomlDouble)defInterceptVal).Value;
                    }
                    if (table.TryGetValue(ConfigConstants.LINEARBUFF_MDEF_SLOP, out var mdefSlopVal))
                    {
                        mdefSlop = (float)((TomlDouble)mdefSlopVal).Value;
                    }
                    if (table.TryGetValue(ConfigConstants.LINEARBUFF_MDEF_INTERCEPT, out var mdefInterceptVal))
                    {
                        mdefIntercept = (float)((TomlDouble)mdefInterceptVal).Value;
                    }
                    return new LinearBuff(apSlop, apIntercept,
                                          mpSlop, mpIntercept,
                                          defSlop, defIntercept,
                                          mdefSlop, mdefIntercept);

                });
        }


    }
}