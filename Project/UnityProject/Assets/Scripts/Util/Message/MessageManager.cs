
/// <summary>
/// 全局消息派发器
/// @author 邱洪波
/// </summary>
using UnityEngine;
public class MessageManager
{
    private static EventDispatch<Message> evnetDispatchInstance = new EventDispatch<Message>();

    /// <summary>
    /// 添加侦听,不能注册静态方法,如果this实例被销毁,即使Target存在,该指令也会被移除
    /// </summary>
    /// <param name="type"></param>
    /// <param name="listener"></param>
    /// <param name="obj"></param>
    /// <param name="groupName"></param>
    public static void AddListener(string type, System.Action listener, UnityEngine.Object obj = null, string groupName = null)
    {
        if (!evnetDispatchInstance.hasListener(type, listener))
            evnetDispatchInstance.addListener(type, listener, obj, groupName);
        else
            Debug.Log("Message:" + type + " 已经注册过");
    }

    public static void AddListener(string type, System.Action<Message> listener, UnityEngine.Object obj = null, string groupName = null)
    {
        if (!evnetDispatchInstance.hasListener(type, listener))
            evnetDispatchInstance.addListener(type, listener, obj, groupName);
        else
            Debug.Log("Message:" + type + " 已经注册过");
    }

    ///// <summary>
    ///// 检测侦听是否已经存在
    ///// </summary>
    ///// <param name="type"></param>
    ///// <param name="listener"></param>
    ///// <returns></returns>
    //public static bool HasListener(string type, System.Action listener)
    //{
    //    return instance.hasListener(type, listener);
    //}

    //public static bool HasListener(string type, System.Action<Message> listener)
    //{
    //    return instance.hasListener(type, listener);
    //}

    /// <summary>
    /// 移除侦听,如果obj为null,则直接移除整个指令
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="listener"></param>
    public static void RemoveListener(string type, System.Action listener)
    {
        evnetDispatchInstance.removeListener(type, listener);
    }

    public static void RemoveListener(string type, System.Action<Message> listener)
    {
        evnetDispatchInstance.removeListener(type, listener);
    }

    public static void RemoveAllListener(string type)
    {
        evnetDispatchInstance.removeAllListener(type);
    }

    //public static void checkAction()
    //{
    //    instance.checkAction();
    //}

    ///// <summary>
    ///// 同步广播指令
    ///// </summary>
    ///// <param name="type"></param>
    ///// <param name="source">消息产生源</param>
    ///// <param name="args"></param>
    //public static void DispatchWithSource(string type, object source, params object[] args)
    //{
    //    instance.applyCommand(instance.getMessage(type, source, args));
    //}

    /// <summary>
    /// 派发消息
    /// </summary>
    /// <param name="type">消息类型</param>
    /// <param name="args">消息参数,如果没有可以不写</param>
    public static void Dispatch(string type, params object[] args)
    {
        evnetDispatchInstance.applyCommand(evnetDispatchInstance.getMessage(type, null, args));
    }

    //public static void DispathMsgByCodeEnum(ErrorCodeEnum codeEnum)
    //{
    //    string msg = Utility.GetTextByErrorCode(codeEnum);
    //    int purpose = Utility.GetPurposeByErrorCode(codeEnum);
    //    MessageManager.Dispatch(MsgType.View.show, ViewType.HintTextView, msg, purpose);
    //}

    //public static void DispathBattleMsgByCodeEnum(ErrorCodeEnum codeEnum)
    //{
    //    string msg = Utility.GetTextByErrorCode(codeEnum);
    //    MessageManager.Dispatch(MsgType.View.show, ViewType.BattleHintTextView, msg);
    //}
}