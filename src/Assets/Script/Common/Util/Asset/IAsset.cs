


using UnityEngine;

namespace QS.Api.Common.Util.Asset
{
    /// <summary>
    /// IAsset �Ƕ���Դ�ĳ���, 
    /// ������������Դ, ������ResourcesҲ������AssetBundle
    /// ����ִ�����ݵĳ־û�, ����ʹ��һ��ȷ����Key�Ϳ����õ�һ��Asset
    /// Asset ͳһ�������Դ, ��������Ŀ����Դ�Ĺ���
    /// ��Ϸ����,��ͨ��������ActiveRecord��, ����Ҫ�־û�
    /// ��Ϸ�ֿⲻ��Executor, ��Ӧ�����־û�, ������ͨ���̳�������������е�����, ���Լ�ʹ��Toml�ĳ־û�Api�Ϳ���
    /// ��ɴ�����
    /// ���״̬�Ƚϸ���, �����˵, һ�����״̬������
    /// Combat������: CombatData, ʵʱ״̬, ���⻹Ӧ���־û�����״̬, �ȷ�˵
    /// CombatData��ʼֵ �� Profession �� Lv ���� CombatData�ĳ�ʼֵ, Ӧ��ͨ����Щ���������в���
    /// //TODO: �����һЩ������߿��Բ���Ԥ�������ǿ��Щ��ʼ����, Ӧ��ʹ������ģʽ���д���.����ʲ�����
    /// (Combat��������Щ��ʼֵ, ���������Ҫ֪����Щ��ʼֵ, Ӧ����ActiveRecord���в���)
    /// �����Ǳ�˭ʹ��??? ���ʹ��Repository, Ӧ����Repository�����־û�
    /// ÿ��ģ����������Եĳ־û�, �־û�ͬ������ʹ��Instruction
    /// �ȷ�˵, Combat 
    /// ��Ϸ״̬�ĳ־û���ʽ��, 
    /// AssetҪ�ṩʲôApi��?
    /// 
    /// �� �뱾�ص����л� ��ʱ��ʹ��Toml, �����������ͨ�ŵ�ʱ�� ʹ��Protobuf
    /// </summary>
    public interface IAsset
    {
        /// <summary>
        /// �ַ�����Դ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetString(string key);
        /// <summary>
        /// ������Դ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int GetInt(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        float GetFloat(string key);
        GameObject GetPrefab(string key);
        Sprite GetSprite(string key);
        string GetFileContent(string key);
        string GetLuaScript(string key);
        AudioClip GetAudioClip(string key);
        AnimationClip GetAnimationClip(string key);

    }
}