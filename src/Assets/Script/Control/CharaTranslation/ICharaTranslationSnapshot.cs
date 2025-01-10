


using UnityEngine;

namespace QS.Api.Control.Domain
{
    /// <summary>
    ///
    /// ֵ�����@�����ǽ�ɫĳһ�r�g��B�Ŀ��գ�ӛ��ˮ��r�̽�ɫλ�����P�Ġ�B��ָ��
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
