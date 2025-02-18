

namespace QS.Api.Skill.Domain
{
    /// <summary>
    /// Skill 的行为和资源定义被定义为完全不相关的两个实体, 与WorldItem完全不同,
    /// SkillAsset, 是配置的,预定义的, 和WorldItem 一样, 放到程序的第三方进行管理, 
    /// 因此不必放到 Skill 的内部定义, 行为程序通过 <see cref="ISkillKey"/> 到数据层
    /// 进行查找, 具体每个资源是什么, 脚本自己知道, 这个是预先约定好的
    /// 
    /// 最基本的，角色發動輕擊技能
    /// 爲了實現效果，需要有如下上下文
    /// HitDetector，這個有指令提供，但是SimpleSkill 定義爲 有前後搖的技能，並不設計到碰撞體
    ///     得做子類了，No， 是做子處理器，SimpleSkill是容器
    ///     DefaultSimpleSkill 也不需要持有上下文，這些上下文由 子處理器提供
    /// 根據攻擊方式的不同來劃分子類是比較合理的。
    ///     一般是碰撞體攻擊，
    ///     或許有射線攻擊
    ///     這些就是Detector相關了，Detector不會提供這些東西，看來Detector得在
    ///     Handler中構建了，然後指令中提供碰撞體或是Ray。
    /// 技能的Combat屬性，這個由技能本身提供，TODO，建立技能DB，從配置文件中讀取
    /// 
    /// 現在的活動記錄模式太難用了，得修改更新才行
    /// 總之TODO： 活動記錄模式修改=>資源DB=>資源定義=>子處理器
    /// 
    /// 實話說，技能這個東西，不同技能差別很大，沒法用繼承
    /// 來描述所有的技能，對於每種技能，都需要一個獨立的類
    /// 這些類相同的東西很少，大概有這些
    /// key，這個是項目的約定
    /// 消耗，這個是基於 CombatData 的，是領域的，實話說，
    /// 我這裏技能的定義很廣泛，把消耗也定義在裏面其實不太好
    /// 先單獨放着，等到之後發現代碼重複的時候再考慮
    /// 其他的領域規定，像是技能樹，等級限定技能之類的，角色限定技能
    /// 這些領域規則，我們把它放到更上層，Skill作爲基礎設施
    /// 
    /// 設計到展示的東西，比方說技能的描述，圖標，等級 這些東西
    /// 額外做定義，額外做配置，把這些東西，放到上層來做會更好
    /// 這個Skill模塊，只是用來做這些技能的實施的，不必涉及這些
    /// 
    /// 指令只是一个外观，在模块内部可以提供更加直接的接口来调用。
    /// </summary>
    public interface ISkill
    {
        ISkillKey Key { get; }
    }
}