


using QS.Skill.Domain;

namespace QS.Api.Skill.Domain
{
    /// <summary>
    /// The Unique address for a Skill
    /// 現在技能還沒有跟它的資源結合，按照領域驅動，這個資源應該由技能本身獲取
    /// 技能本身提供。現在，我們需要確認技能到底需要什麼資源。
    /// 我們的技能系統是由動畫驅動的，所以動畫不被視爲技能資源。
    /// 特效，Prefab，聲音。ARPG中無非這幾種，（先不考慮同期動畫，同期動畫作爲最後一個額外階段）
    /// 前兩者，找找到位置實例化就好了，後者找到碰撞體，在它身上播放聲音即可。這些邏輯
    /// 它們自己處理，但是我的把Chara給它們。
    /// 然後，武器呢，武器的使用絕對是屬於技能的範疇，因此技能應該依賴物體模塊。
    /// 
    /// Skill 是在Chara 上的增強，
    /// </summary>
    public interface ISkillKey
    {
        /// <summary>
        /// The predefined No of Skill.
        /// </summary>
        string No { get; }

        /// <summary>
        /// The name of Skill
        /// </summary>
        string Name { get; }

        string Patch { get; }

        public static ISkillKey New(string no, string name, string patch)
        {
            return new SkillKey(no, name, patch);
        }

        public static ISkillKey New(string no, string name)
        {
            return new SkillKey(no, name);
        }

    }
}