using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SynchronousLoading : MonoBehaviour
{
    private AsyncOperation mAsyncOperation; //异步加载信息
    public void Load(string scenename)
    {
        //SceneManager.LoadScene(scenename);
        StartCoroutine(LoadSceneFunction(scenename));
    }
    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadSceneFunction(string scenename)
    {
        mAsyncOperation = SceneManager.LoadSceneAsync(scenename);
        //不跳转场景，停留在当前场景
        mAsyncOperation.allowSceneActivation = true;
        yield return mAsyncOperation;
    }

}
