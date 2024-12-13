using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Api.Common.Conf
{
    /// <summary>
    /// 这个配置是按项目模块划分的. 配置按层级分父子配置
    /// 默认每个配置按文件夹表示继承结构, 如果要特定指定继承请在配置文件的根表加上 parent 键 parent: false 表示没有父节点
    /// 配置按文件夹划分, 默认以文件夹名称(级联父文件夹名称, .划分)作为配置的ID, 也可以在配置文件的根表加上
    /// id 键来指定自己的ID, 每个文件夹都被视为独立的配置节点, 配置机制
    /// 首先会扫描名称为index.toml 的配置文件, 你应该把所有的Meta配置信息都放在index.toml的根表中
    /// 然后使用 根表键 cfgs 来指定一个列表, 列表名包括当前文件夹下, 你想加载的其他配置文件
    /// 没有index.toml的文件夹不被视为配置文件夹, Configuration包含的内容是Toml直接反序列化之后的内容
    /// </summary>
    public interface IConfiguration 
    {

    }

}