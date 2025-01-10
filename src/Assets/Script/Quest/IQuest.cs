using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QS.Quest
{
    /// <summary>
    /// 我想做成 P或者是 異度之刃 那種風格的RPG。所以任務是常規概念的建模
    /// 這個接口描述任務這個領域對象，至於任務的發放，那應該由領域服務實現。
    /// 這是一個複雜的事情，如果它是一個編程構件，我就以抽象類實現它，
    /// 但它是一個領域對象，所以我把咋個複雜的事情放到領域服務中。
    /// 
    /// 一個任務，被描述爲：吩咐玩家做一系列事情，然後給予玩家獎勵。
    /// 每個任務可能有很多奇特的地方，跟技能一樣，使用OOP覆蓋大部分抽象，
    /// 特別的任務特別實現。
    /// 
    /// 任務 大致上經歷這麼一個生命週期：
    /// 觸發任務 => 按線性步驟完成任務 => 領取獎勵
    /// 
    /// 這寫子步驟是有序的，
    /// 子步驟的調用，是任務的責任，
    /// 觸發任務和領取獎勵 是任務服務的責任，但是任務得提供上下文，
    /// 
    /// 那麼，下一步，我得去解析配置文件，把配置文件定義的東西
    /// 構建到內存中來。
    /// 
    /// 先去看 Action 的部分吧
    /// </summary>
    public interface IQuest
    {

        /// <summary>
        /// 指示任務是否已經完成
        /// </summary>
        /// <returns></returns>
        bool IsAchieved { get; }
        
        /// <summary>
        /// 當前任務執行到的子步驟
        /// </summary>
        IQuestStep CurrentStep { get; }
    }

}