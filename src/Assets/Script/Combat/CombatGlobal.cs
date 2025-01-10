

using GameLib.DI;
using QS.Api.Combat.Domain;
using QS.Api.Combat.Service;
using QS.Api.Common;
using QS.Combat.Domain;
using QS.Combat.Service;
using QS.Common;
using Tomlet;
using Tomlet.Exceptions;
using Tomlet.Models;
using UnityEngine;

namespace QS.Combat
{

    public class CombatGlobal : ModuleGlobal<CombatGlobal>
    {
        internal IDIContext DI = IDIContext.New();
        protected override IDIContext DIContext => DI;
        [Injected]
        readonly IBuffFactory buffFactory;
        [Injected]
        readonly IAttackFactory attackFactory;
        public CombatGlobal()
        {
            CommonGlobal.Instance.ProvideBinding(DI);

            DI
                .Bind<BuffFactory>()
                .Bind<AttackFactory>();
            DI.Inject(this);

            // 有必要順序依賴的東西，放到這裏構造器實現，DI的機制會保證
            // 它們的加載順序
            RegisterTomlType();
        }

   
        public override void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(DI.GetInstance<IBuffFactory>())
                .BindExternalInstance(DI.GetInstance<IAttackFactory>());
        }

        void RegisterTomlType()
        {
            TomletMain.RegisterMapper<IBuff>(
                            buff =>
                            {
                                TomlTable buffTable = new();
                                TomlString stage = new(buff.Stage.ToString());

                                buffTable.PutValue("Stage", stage);

                                if (buff is AttackBuff attackBuff)
                                {
                                    buffTable.PutValue("Type", new TomlString("Attack"));

                                    TomlDouble atkRatio = new(attackBuff.AtkRatio);

                                    TomlDouble matkRatio = new(attackBuff.MatkRatio);

                                    buffTable.PutValue("AtkRatio", atkRatio);
                                    buffTable.PutValue("MatkRatio", matkRatio);
                                }

                                else
                                {
                                    throw new System.Exception();
                                }

                                return buffTable;
                            },
                            tomlValue =>
                            {
                                if (tomlValue is not TomlTable table)
                                {
                                    throw new TomlTypeMismatchException(
                                        typeof(TomlTable), tomlValue.GetType(), typeof(IBuff));
                                }
                                IBuff result = default;
                                var type = table.GetString("Type");
                                result = type switch
                                {
                                    "Attack" => buffFactory.AttackBuff(
                                                                    table.GetFloat("AtkRatio"),
                                                                    table.GetFloat("MatkRatio")),
                                    _ => throw new System.Exception(),
                                };
                                return result;
                            }
                            );
            TomletMain.RegisterMapper<IAttack>(
                atk =>
                {
                    var table = new TomlTable();
                    table.PutValue("Atk", new TomlDouble(atk.Atn));
                    table.PutValue("Matk", new TomlDouble(atk.Matk));
                    return table;
                },
                tomlValue =>
                {
                    if (tomlValue is not TomlTable table)
                    {
                        throw new TomlTypeMismatchException(typeof(TomlTable), tomlValue.GetType(), typeof(IAttack));
                    }

                    return attackFactory.NewAttack(table.GetFloat("Atk"), table.GetFloat("Matk"));
                }
                );
        }

    }
}