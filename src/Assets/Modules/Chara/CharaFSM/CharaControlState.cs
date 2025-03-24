


namespace QS.Chara.Abilities
{
    /// <summary>
    /// 这个控制状态也是共用的，大部分角色状态都要填充到里面.
    /// 这是指角色的控制状态，并非是Agent的状态
    /// Agent 通过向 这个状态发送指令来进行工作

    /// 控制状态机做杂活，向解析指令，更新子状态机的状态，
    /// 计算大家都需要的一些数据等等
    /// 基本上，数值的计算是可复用的，但是动画的设置不一定了
    /// 这个有几个不同的方案，
    /// 1. process中 执行速度和位移计算，然后设置动画。位移和旋转由代码计算
    /// 2. process中 执行速度计算，然后设置动画。最后在动画回调中，获取根骨骼动画的位移来设置
    /// 3. 完全由根骨骼控制

    /// </summary>
    public enum CharaState
    {
        Idle,
        Walking,
        Runing,
        Jumping,
        Blocking,
        Hit,
        RootMotion,
        Dodge
    }
}
