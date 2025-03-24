


using System;

namespace QS.GameLib.DI
{
    /// <summary>
    /// 配置源信息的地方，现在只用与标识工厂方法
    /// </summary>
    public class Source : Attribute
    {
        public string Name;
    }
}