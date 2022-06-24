using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiBridge
{
    private static MiBridge ins;
    public static MiBridge Ins
    {
        get
        {
            if (ins == null) ins = new MiBridge();
            return ins;
        }
    }

    private MiToUnityBridge miToUnity;

    AndroidJavaClass jc;
    MiBridge()
    {
        //创建一个全局物体用于接收andiord回发的消息
        GameObject obj = new GameObject();
        GameObject unityBridge = UnityEngine.Object.Instantiate(obj);
        unityBridge.name = "ToUnnityBridge";
        miToUnity = unityBridge.AddComponent<MiToUnityBridge>();
        //获取andiord端Bridge脚本
        jc = new AndroidJavaClass("com.unity3d.player.Bridge");
        SendCallStatic(MIADAPI.TEST_LOG);
    }

    public void SendCallStatic(MIADAPI api)
    {
        jc.CallStatic("Message", api.ToString());
    }

    /// <summary>
    /// 视频广告会出现连点
    /// </summary>
    public bool isPlayVideo = false;
    public void PlayVideo(Action success, Action failed)
    {
        if (isPlayVideo) {
            Debug.Log("当前有广告正在加载或播放,退出调用");
            return;
        }
        isPlayVideo = true;
        miToUnity.SetVideoEvent(success, failed);
        SendCallStatic(MIADAPI.PLAY_VIDEO);
    }

    public void SetNativeInterstitialEvent(Action action)
    {
        miToUnity.SetNativeInterstitialEvent(action);
    }
}

public enum MIADAPI
{
    INITADSDK,

    TEST_LOG,

    MILOGIN,

    USERAGREED,

    LOAD_VIDEO,

    PLAY_VIDEO,

    SHOW_INTERSTITIAL_VIDEO_AD,

    SHOW_NATIVE_INTERSTITIAL,

    SHOW_NATIVE,

    HIDE_NATIVE,

    SHOW_BANNER,

    LOAD_BANNER,

    HIDE_BANNER,

    VIBRATE,

    GET_USER_AGREEMENT,
}
