-- System

Task = CS.System.Threading.Tasks.Task
TaskCompleteSource = CS.System.Threading.Tasks.TaskCompletionSource(CS.System.Int32)

--  Unity
DebugLog = CS.UnityEngine.Debug.Log
Vector3 = CS.UnityEngine.Vector3
Vector2 = CS.UnityEngine.Vector2


-- UIToolkit
uidoc = {
    uquery = CS.UnityEngine.UIElements.UQueryExtensions,
    IStyle = CS.UnityEngine.UIElements.IStyle,
    StyleLength = CS.UnityEngine.UIElements.StyleLength,
    Length = CS.UnityEngine.UIElements.Length,
    LengthUnit = CS.UnityEngine.UIElements.LengthUnit,

    getstyle = function (ve)
        local style = ve.style
        cast(style, typeof(uidoc.IStyle))
        return style
    end
}

-- Common
TransformUtil = CS.QS.Common.Util.TransformUtil
ReflectionUtil = CS.QS.Common.Util.ReflectionUtil
RelativeDir = CS.QS.Common.RelativeDir

MiscUtil = CS.QS.Common.Util.MiscUtil
ObjectPool = CS.QS.Common.Util.ObjectPool


-- Module Global
Trunk = CS.QS.Trunk.TrunkGlobal.Instance


-- Player Module
PlayerUtil = CS.QS.Player.PlayerUtil

LuaUtil = CS.QS.Trunk.LuaUtil
LuaUIDocument = CS.QS.UI.LuaUIDocument
LuaUIDocumentLoadOp = CS.QS.UI.LuaUIDocumentLoadOp


-- Agent NameSpace
agent = {

}