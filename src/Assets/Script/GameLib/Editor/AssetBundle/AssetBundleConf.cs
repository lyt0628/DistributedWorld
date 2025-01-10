


using System.Linq;
using Tomlet;
using UnityEngine;

namespace QS.GameLib.Editor.AssetBundle
{
    /// <summary>
    /// �����o��·���������ļ������ؘ��� AssetBundle��������
    /// </summary>
    class AssetBundleConf
    {

        public const string bundleNameKey = "name";
        public const string filesKey = "files";

        /// <summary>
        /// �������� AssetBundle �����Q
        /// </summary>
        public string BundleName { get; }

        /// <summary>
        /// ������ �@��AssetBundle ���ļ�·��
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