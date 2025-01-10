


using QS.GameLib.Pattern.Message;

namespace QS.Quest
{
    /// <summary>
    /// 那麼麼一個子步驟，都應當獨立地判斷，判斷自己這個子步驟是否完成。
    /// 常見的子步驟包括，交互，打怪，到達指定地點
    /// 
    /// 交互是：玩家面對NPC 按下E鍵， 然後交互。
    /// 顯然，這一些都於任務無關，我只關係是否交互，一下的事情。
    /// 
    /// 這些，零散的事情，又需要通知，我們約定使用全局事件來做，
    /// 請在Toml中提供消息ID
    /// 
    /// 子步驟需要生命週期來，爲自己做準備
    /// 任務進入 取決於上一個子步驟是否完成，因此本個子步驟對此無感知，這個需要生命週期
    /// 至於任務完成，子步驟自己會檢測任務完成這個時機的，
    /// 
    /// 子任務需要提供，自己完成的消息才行。與其每個子步驟都有消息讓Quest監聽，
    /// 不如 NONONONO 
    /// 差點忘記了，子步驟自己知道自己下一個步驟是什麼。
    /// </summary>
    public interface IQuestStep
    {
        /// <summary>
        /// 指示子步驟是否已經完成
        /// </summary>
        bool IsAchieved { get; }

        /// <summary>
        /// 下一個子步驟
        /// </summary>
        IQuestStep Next { get; }
        /// <summary>
        /// 當子任務開始時後被調用
        /// </summary>
        void OnBegin();
    }
}