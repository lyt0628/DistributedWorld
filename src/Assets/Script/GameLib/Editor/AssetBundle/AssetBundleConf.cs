


using System.Linq;
using Tomlet;
using UnityEngine;

namespace QS.GameLib.Editor.AssetBundle
{
    /// <summary>
    /// 解析給定路徑的配置文件，返回構建 AssetBundle的上下文
    /// </summary>
    class AssetBundleConf
    {

        public const string bundleNameKey = "name";
        public const string filesKey = "files";

        /// <summary>
        /// 構建出的 AssetBundle 的名稱
        /// </summary>
        public string BundleName { get; }

        /// <summary>
        /// 包含在 這個AssetBundle 的文件路徑
        /// </summary>
        public string[] Files {  get; }

        public string ConfPath { get; }

        public AssetBundleConf(string confPath)
        {
            ConfPath = confPath;

            var doc = TomlParser.ParseFile(confPath);
            BundleName = doc.GetString(bundleNameKey);
        
            var files = doc.GetArray(filesKey);
            Files = files.Select(f=>f.StringValue).ToArray();
        }
    }
}