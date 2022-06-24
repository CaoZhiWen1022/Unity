package com.unity3d.player;

import android.os.Handler;
import android.os.Looper;
import android.util.Log;

import ad.UnityPlayerActivity1;
import ad.UnityPlayerActivity2;
import ad.UnityPlayerActivity3;
import ad.UnityPlayerActivity4;
import ad.UnityPlayerActivity5;
import ad.UnityPlayerActivity6;

public class Bridge {
    public static String TAG = "Bridge";
    public static Handler m_Handler = new Handler(Looper.getMainLooper());

    public static UnityPlayerActivity1 mainActivity1;

    public static UnityPlayerActivity2 mainActivity2;

    public static UnityPlayerActivity3 mainActivity3;

    public static UnityPlayerActivity4 mainActivity4;

    public static UnityPlayerActivity5 mainActivity5;

    public static UnityPlayerActivity6 mainActivity6;

    public static void Message(String api) {

        m_Handler.post(
                new Runnable() {
                    public void run() {
                        Log.d(TAG, "Message: " + api);
                        switch (api) {
                            case "TEST_LOG": {
                                Log.d(TAG, "Message: ");
                                break;
                            }
                            case "INITADSDK": {
                                mainActivity1.InitADSDK();
                                break;
                            }
                            case "MILOGIN": {
                                mainActivity1.Login();
                                break;
                            }
                            case "USERAGREED": {
                                mainActivity1.OnUserAgreed();
                                break;
                            }
                            case "LOAD_VIDEO": {
                                mainActivity2.LoadVideo();
                                break;
                            }
                            case "PLAY_VIDEO": {
                                mainActivity2.PlayVideo();
                                break;
                            }
                            case "SHOW_INTERSTITIAL_VIDEO_AD": {
                                mainActivity3.ShowInterstitialVideoAd();
                                break;
                            }
                            case "SHOW_NATIVE_INTERSTITIAL": {
                                mainActivity4.ShowNativeInterstitialAd();
                                break;
                            }
                            case "SHOW_NATIVE": {
                                mainActivity5.ShowNativeAd();
                                break;
                            }
                            case "HIDE_NATIVE": {
                                mainActivity5.hideNativeAd();
                                break;
                            }
                            case "HIDE_BANNER": {
                                mainActivity6.HideBanner();
                                break;
                            }
                            case "LOAD_BANNER":{
                                mainActivity6.Load();
                                break;
                            }
                    }
                }
                });
        }

    /**
     * 给unity发送消息
     * @param api VIDEO_PLAY_SUCCESS || VIDEO_PLAY_FAILED
     */
    public static void SendMessage(String api){
            UnityPlayer.UnitySendMessage("ToUnnityBridge", "Message", api);
        }

}
