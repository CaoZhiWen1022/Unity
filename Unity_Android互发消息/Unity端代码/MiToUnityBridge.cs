using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiToUnityBridge : MonoBehaviour
{
    public static bool isLoginSuccess = false;

    public Action success, failed;

    public Action interstitialEvent;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetVideoEvent(Action success, Action failed)
    {
        this.success = success;
        this.failed = failed;
    }

    public void SetNativeInterstitialEvent(Action interstitialEvent)
    {
        this.interstitialEvent = interstitialEvent;
    }

    public void Message(string api)
    {
        switch (api)
        {
            case "VIDEO_PLAY_SUCCESS":
                {
                    MiBridge.Ins.isPlayVideo = false;
                    TaskMgr.Ins.SetTaskDataByType(TaskSumType._观看广告_, 1);
                    if (success != null)
                    {
                        success();
                        success = null;
                    }
                    break;
                }
            case "VIDEO_PLAY_FAILED":
                {
                    MiBridge.Ins.isPlayVideo = false;
                    if (failed != null)
                    {
                        failed();
                        failed = null;
                    }
                    break;
                }
            case "LOGIN_SUCCESS":
                {
                    isLoginSuccess = true;
                    break;
                }
            case "NATIVE_INTERSTITIAL_END":
                {
                    if (interstitialEvent != null)
                    {
                        interstitialEvent();
                    }
                    break;
                }
            case "GET_AGREEMENT_TRUE":
                {
                    //告知统一协议
                    MIPlatform.Ins.OnUserAgreed();
                    //调用登录
                    MIPlatform.Ins.MiLogin();
                    //初始化SDK
                    MIPlatform.Ins.InitADSDK();
                    break;
                }
        }
    }
}
