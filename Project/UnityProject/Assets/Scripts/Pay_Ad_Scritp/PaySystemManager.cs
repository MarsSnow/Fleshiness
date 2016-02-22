/*
* 　　　　　　　　┏┓　　　┏┓+ +
*　　　　　　　┏┛┻━━━┛┻┓ + +
*　　　　　　　┃　　　　　　　┃ 　
*　　　　　　　┃　　　━　　　┃ ++ + + +
*　　　　　　 ████━████ ┃+
*　　　　　　　┃　　　　　　　┃ +
*　　　　　　　┃　　　┻　　　┃
*　　　　　　　┃　　　　　　　┃ + +
*　　　　　　　┗━┓　　　┏━┛
*　　　　　　　　　┃　　　┃　　　　　　　　　
*　　　　　　　　　┃　　　┃ + + + +
*　　　　　　　　　┃　　　┃　
*　　　　　　　　　┃　　　┃ + 　　　　
*　　　　　　　　　┃　　　┃
*　　　　　　　　　┃　　　┃　　+　　　　　　　　　
*　　　　　　　　　┃　 　　┗━━━┓ + +
*　　　　　　　　　┃ 　　　　　　　┣┓
*　　　　　　　　　┃ 　　　　　　　┏┛
*　　　　　　　　　┗┓┓┏━┳┓┏┛ + + + +
*　　　　　　　　　　┃┫┫　┃┫┫
*　　　　　　　　　　┗┻┛　┗┻┛+ + + +
*/
/*
 *脚本作用:管理支付信息，主要管理积分
 *寄生物体：UnityPayManager物体（空物体）
 */

using UnityEngine;
using System.Collections;

public class PaySystemManager : MonoBehaviour
{
    //标识本脚本的名称，便于Android中调用
    private string MyGameObjectName = "UnityPayManager";
    //public static AndroidJavaObject android_java_class;
    //用户积分
    public static int MyUserPoint = 0;

    void Start()
    {
        //设置脚本标识
        this.name = MyGameObjectName;
        //把本C#代码包装成Android中类似java代码形式（Activity），用于本C#代码访问外部java代码
        //AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //android_java_class = ajc.GetStatic<AndroidJavaObject>("currentActivity");

        //从设备上获取用户积分
        //MyUserPoint = PlayerPrefs.GetInt("my_user_point");
    }

    void Update()
    {

    }

    ////设置积分（Android端访问的方法）
    //void SetMyUserPoint(string point)
    //{
    //    //string转换为int
    //    int temp = int.Parse(point);
    //    //设置int值进入设备
    //    PlayerPrefs.SetInt("my_user_point", temp);
    //    //把最新的从服务器传来的int值记录下来
    //    MyUserPoint = temp;
    //    Debug.Log("积分:" + MyUserPoint);
    //}

}
