using UnityEngine;

namespace QS.Api.Common.Util.Detector
{

    /// <summary>
    /// û��ֱ�ӷŵ� GameLib, ���ʹGameLib�ڲ���ͬģ�����, �ŵ�Common, �ȽϺ���
    /// ����������һ��Utilģ��???������һ����Common���Ƿ�һЩ������صĶ�����
    /// ����, �ŵ���һ��Assembly�͵�������Common�����ModuleGlobal, ���ڰ�ModuleGlobal�ŵ�Util��???
    /// ��Ҫ, Util���Ǹ������, �����������ģ���������ģ��
    /// </summary>
    public interface IDetector
    {
        GameObject[] Detect();
    }
}
