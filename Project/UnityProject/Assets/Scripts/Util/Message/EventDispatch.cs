using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System;


/**
 * 事件派发器,使用方式类型flash的EventDispathch,如果需要使用多线程(非主线程不能访问gameobject)需要开启异步功能openAsyn
 * 子线程需要派发事件使用dispatchAsyn
 * @author 邱洪波
 */
public class EventDispatch<T> where T : Message, new()
{
    #region 内部类,枚举,结构体 inner classes, enum, and structs

    /// <summary>
    /// 事件侦听器
    /// </summary>
    protected class MessageListener
    {
        public string type;

        public string groupName;

        public System.Action listener;

        public bool checkObject = false;

        public System.Object obj;//生命周期判断对象,例如静态方法自身没有target,如果指定生命周期判断对象,则该侦听的生命周期由对象的生命周期来判断

        /// <summary>
        /// 事件侦听是否还有效
        /// </summary>
        /// <returns></returns>
        public virtual bool isAction()
        {
            if (checkObject)//如果是检测指定的对象
                return obj != null || obj as UnityEngine.Object;

            if (listener.Method.IsStatic)//静态方法
                return true;

            if (listener.Target is UnityEngine.Object)
                return (UnityEngine.Object)listener.Target;
            else
                return listener.Target != null;
        }

        /// <summary>
        /// 执行该事件侦听
        /// </summary>
        /// <param name="args"></param>
        public virtual void invoke(T msg)
        {
            listener();
        }

        public override bool Equals(object obj)
        {
            return listener.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string str = "";
            if (listener.Method.IsStatic)
                str += "静态方法";
            else
                str += listener.Target;
            str += "." + listener.Method.Name;
            str += "[" + groupName + "]";
            return str;
        }
    }

    /// <summary>
    /// 带参数的事件侦听器
    /// </summary>
    protected class MessageListenerParams : MessageListener
    {
        new public System.Action<T> listener;

        public override bool isAction()
        {
            if (checkObject)//如果是检测指定的对象
                return obj != null || obj as UnityEngine.Object;

            if (listener.Method.IsStatic)//静态方法
                return true;

            if (listener.Target is UnityEngine.Object)
                return (UnityEngine.Object)listener.Target;
            else
                return listener.Target != null;
        }

        public override void invoke(T msg)
        {
            listener(msg);
        }

        public override bool Equals(object obj)
        {
            return listener.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string str = "";
            if (listener.Method.IsStatic)
                str += "静态方法";
            else
                str += listener.Target;
            str += "." + listener.Method.Name;
            str += "[" + groupName + "]";
            return str;
        }
    }


    protected class MessageListenerList
    {
        public string type;

        private List<MessageListener> listeners = new List<MessageListener>();

        public List<MessageListener> getList()
        {
            return listeners;
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="msg"></param>
        public int invoke(T msg)
        {
            check();//清除失效事件侦听
            int count = 0;
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].invoke(msg);
                count++;
            }
            return count;
        }

        /// <summary>
        /// 检测列表中失效的事件侦听并移除,一般用于场景切换之前使用
        /// </summary>
        public void check()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                if (listeners[i].isAction() == false)//如果存活则触发,否则移除
                {
                    //if (MessageManager.isDebug)
                    //    LogManager.debug("事件[" + type + "]的(" + listeners[i].ToString() + ")侦听通过check检测移除");
                    listeners.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 添加事件侦听
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="groupName"></param>
        public void addListener(System.Action listener, System.Object checkObj = null, string groupName = null)
        {
            int index = indexOfListener(listener);
            if (index >= 0)
            {
                //if (MessageManager.isDebug)
                //    LogManager.warn("事件:", type, "方法(" + listener.ToString() + ")已经注册过,不能重复注册");
                return;
            }
            MessageListener el = new MessageListener() { listener = listener, type = type, groupName = groupName };
            if (checkObj != null)
            {
                el.obj = checkObj;
                el.checkObject = true;
            }
            listeners.Add(el);
        }

        public void addListener(System.Action<T> listener, System.Object checkObj = null, string groupName = null)
        {
            int index = indexOfListener(listener);
            if (index >= 0)
            {
                //if (MessageManager.isDebug)
                //    LogManager.warn("事件:", type, "方法(" + listener.ToString() + ")已经注册过,不能重复注册");
                return;
            }
            MessageListenerParams el = new MessageListenerParams() { listener = listener, type = type, groupName = groupName };
            if (checkObj != null)
            {
                el.obj = checkObj;
                el.checkObject = true;
            }
            listeners.Add(el);
        }

        public bool hasListener(object listener)
        {
            return indexOfListener(listener) >= 0;
        }

        public int removeListener(string groupName)
        {
            int count = listeners.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (listeners[i].groupName == groupName)
                    listeners.RemoveAt(i);
            }
            return count - listeners.Count;
        }

        public int removeListener(object listener)
        {
            int index = indexOfListener(listener);
            if (index >= 0)
            {
                listeners.RemoveAt(index);
            }
            return 0;
        }

        public void clear()
        {
            listeners.Clear();
        }

        /// <summary>
        /// 调试使用,打印所有的方法列表
        /// </summary>
        public void print()
        {
            string str = type + "  ==================================================================\n";
            for (int i = 0; i < listeners.Count; i++)
            {
                str += listeners[i].ToString() + "\n";
            }
            //LogManager.debug(str);
        }

        private int indexOfListener(object listener)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                if (listeners[i].Equals(listener))
                {
                    return i;
                }
            }
            return -1;
        }
    }

    #endregion

    #region 属性定义

    public bool isDebug = false;
    protected List<T> pool;//异步派发保存消息列表
    protected List<MessageListenerList> listeners = new List<MessageListenerList>();
    protected List<T> messageCache = new List<T>();//事件对象缓存,防止频繁实例化事件对象

    #endregion

    #region 公有方法public

    public EventDispatch()
    {
    }

    /// <summary>
    /// 添加侦听,不能注册静态方法,如果this实例被销毁,即使Target存在,该指令也会被移除
    /// </summary>
    /// <param name="type"></param>
    /// <param name="listener"></param>
    /// <param name="obj"></param>
    /// <param name="groupName"></param>
    public void addListener(string type, System.Action listener, System.Object obj = null, string groupName = null)
    {
        MessageListenerList vo = getListener(type);

        if (vo == null)
        {
            vo = new MessageListenerList() { type = type };
            listeners.Add(vo);
        }
        vo.addListener(listener, obj, groupName);
    }

    public void addListener(string type, System.Action<T> listener, UnityEngine.Object obj = null, string groupName = null)
    {
        MessageListenerList vo = getListener(type);

        if (vo == null)
        {
            vo = new MessageListenerList() { type = type };
            listeners.Add(vo);
        }
        vo.addListener(listener, obj, groupName);
    }

    /// <summary>
    /// 检测侦听是否已经存在
    /// </summary>
    /// <param name="type"></param>
    /// <param name="listener"></param>
    /// <returns></returns>
    public bool hasListener(string type, System.Action listener)
    {
        MessageListenerList vo = getListener(type);
        return vo != null && getListener(type).hasListener(listener);
    }

    public bool hasListener(string type, System.Action<T> listener)
    {
        MessageListenerList vo = getListener(type);
        return vo != null && getListener(type).hasListener(listener);
    }

    /// <summary>
    /// 移除侦听,如果obj为null,则直接移除整个指令
    /// </summary>
    /// <param name="type"></param>
    /// <param name="listener"></param>
    public void removeListener(string type, System.Action listener = null)
    {
        MessageListenerList vo = getListener(type);
        if (vo != null)
        {
            if (listener == null)
                vo.clear();
            else
                vo.removeListener(listener);
        }
    }

    public void removeListener(string type, System.Action<T> listener = null)
    {
        MessageListenerList vo = getListener(type);
        if (vo != null)
        {
            if (listener == null)
                vo.clear();
            else
                vo.removeListener(listener);
        }
    }

    public void removeAllListener(string type)
    {
        MessageListenerList vo = getListener(type);
        if (vo != null)
        {
            vo.clear();
        }
    }

    public void checkAction()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].check();
        }
    }

    /// <summary>
    /// 同步广播指令
    /// </summary>
    /// <param name="type"></param>
    /// <param name="source">消息产生源</param>
    /// <param name="args"></param>
    public void dispatchWithSource(string type, object source, params object[] args)
    {
        applyCommand(getMessage(type, source, args));
    }

    public void dispatch(string type, params object[] args)
    {
        applyCommand(getMessage(type, null, args));
    }

    public void dispatch(T msg)
    {
        applyCommand(msg);
    }
    #endregion

    #region 私有方法private,包属性方法internal

    //获取一个指令,如果指令列表不存在该指令则返回null
    protected MessageListenerList getListener(string type)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            if (listeners[i].type == type)
            {
                return listeners[i];
            }
        }
        return null;
    }

    //触发一个指令,如果该指令没有侦听,则自动移除
    internal virtual void applyCommand(T msg)
    {
        MessageListenerList listener = getListener(msg.type);
        if (listener != null)
        {
            if (listener.invoke(msg) == 0)
                listeners.Remove(listener);
        }
    }

    internal virtual T getMessage(string type, object source, object[] args)
    {
        int count = messageCache.Count - 1;
        T msg;
        if (count >= 0)
        {
            msg = messageCache[count];
            messageCache.RemoveAt(count);
            msg.args = args;
            msg.source = source;
            msg.type = type;
        }
        else
        {
            msg = new T() { type = type, source = source, args = args };
        }
        return msg;
    }

    internal virtual void putMessage(T msg)
    {
        messageCache.Add(msg);
    }
    #endregion
}


public class Message
{
    public string type;
    public object source;//事件源

    public virtual object[] args
    {
        get;
        set;
    }

    public virtual void reset()
    {
        args = null;
        source = null;
    }
}
