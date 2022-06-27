using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class LoadHelper
{
    public static async Task<string> LoadStringAsync(string url)
    {
        var getRequest = UnityWebRequest.Get(url);
        await getRequest.SendWebRequest();
        byte[] strData = getRequest.downloadHandler.data;
        var gb2312 = System.Text.Encoding.GetEncoding("gb2312");
        string str = gb2312.GetString(strData);
        return str;
    }
}

public static class ExtensionMethods
{
    public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
    {
        var tcs = new TaskCompletionSource<object>();
        asyncOp.completed += obj => { tcs.SetResult(null); };
        return ((Task)tcs.Task).GetAwaiter();
    }
}