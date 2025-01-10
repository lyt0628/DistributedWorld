


using UnityEngine;

namespace QS.Api.Control.Domain
{
    /// <summary>
    ///
    /// 值對象，這部分是角色某一時間狀態的快照，記錄了當時刻角色位移相關的狀態和指令
    /// </summary>
    public interface ICharaTranslationSnapshot
    {

        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        float Horizontal { get; set; }
        float Vertical { get; set; }
        bool Dash { get; set; }
        bool Jump { get; set; }
        Vector3 BaseRight { get; set; }
        Vector3 BaseForword { get; set; }
        Vector3 BaseUp { get; set; }

    }
}
