

using GameLib.DI;
using QS.Common;
using QS.Trunk;
using System.Collections.Generic;
using System.Linq;
using Tomlet;
using Tomlet.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace QS.Dialog
{
    /// <summary>
    /// 解析一个 台词文件获取交互。现在所有NPC的交互都放在一个文件里
    /// </summary>
    class DialogsLoadOp : AsyncOpBase<IDialog[]>
    {
        [Injected]
        readonly TomlParser parser;

        public DialogsLoadOp()
        {
            parser = TrunkGlobal.Instance.GetInstance<TomlParser>();
        }

        protected override async void Execute()
        {
            Dictionary<int, ILine> lineDict = new();
            Dictionary<int, LineOptionList> optionsDict = new();
            Dictionary<int, IDialog> dialogDict = new();

            var h_Conf = Addressables.LoadAssetAsync<TextAsset>("Lines_NPC1");
            await h_Conf.Task;
            var conf = h_Conf.Result;
            var table = parser.Parse(conf.text);
            var lines = table.GetSubTable("lines");
            //var sections = table.GetSubTable("sections");
            var lineOptions = table.GetSubTable("line_options");
            var dialogs = table.GetArray("dialogs");

            // 单句台词
            foreach (var line in lines)
            {
                var id = int.Parse(line.Key);
                var l = new Line(line.Value.StringValue);

                lineDict.Add(id, l);
            }
            // 玩家选项
            foreach (var options in lineOptions)
            {
                var id = int.Parse(options.Key);
                var o = new LineOptionList();
                var ops = options.Value as TomlArray;
                foreach (TomlTable op in ops.Cast<TomlTable>())
                {
                    var lineId = op.GetInteger("id");
                    if (lineDict.TryGetValue(lineId, out var l))
                    {
                        o.AddOption(new LineOption(l));
                    }
                }
                optionsDict.Add(id, o);
            }

            // 遍历两遍，第一次，先建立符号表收集数据
            foreach (TomlTable dialogConf in dialogs.Cast<TomlTable>())
            {
                var id = dialogConf.GetInteger("id");
                var dialog = new Dialog();
                var flowConf = dialogConf.GetArray("flow").Cast<TomlTable>();
                foreach (TomlTable dialogLineConf in flowConf)
                {
                    SpeakerType speaker = SpeakerType.Self;
                    if (dialogLineConf.TryGetValue("speaker", out TomlValue type))
                    {

                        if (int.Parse(type.StringValue) == 1)
                        {
                            speaker = SpeakerType.Player;
                        }
                    }
                    var lineId = dialogLineConf.GetInteger("id");
                    if (speaker is SpeakerType.Self)
                    {
                        dialog.AddLine(new DialogLine(speaker, lineDict[lineId]));
                    }
                    else
                    {
                        dialog.AddLine(new DialogLine(speaker, optionsDict[lineId]));
                    }
                }
                dialogDict.Add(id, dialog);
            }

            // 第二次再构建分支


            Complete(dialogDict.Values.ToArray());
        }
    }
}