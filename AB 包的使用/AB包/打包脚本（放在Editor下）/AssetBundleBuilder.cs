using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;


/// <summary>
/// 资源包打包工具
/// <para>打包AssetBundle和场景(Unity 2018.2.20)</para>
/// </summary>
public class AssetBundleBuilder
{
#if UNITY_EDITOR
    //当然，如果是在编辑器里测试，无论发布设置里是怎么设置的，这里的代码都会被编译
    [MenuItem("打包/WebGL/资源包")]
    public static void BuildAbsWebGL()
    {
        string path= Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Package/RescurceABs/";
        path= Application.streamingAssetsPath + "/Package/RescurceABs/";
        BuildAssetBundles(BuildTarget.WebGL,path);
    }


    [MenuItem("打包/WebGL/场景")]
    public static void BuildScenesWebGL()
    {
        string path= Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Package/SceneABS/";
        path = Application.streamingAssetsPath + "/Package/SceneABS/";
        BuildScenes(BuildTarget.WebGL,path);
    }
    [MenuItem("打包/Windows/场景")]
    public static void BuildScenesWindows()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Package_Win/SceneABS/";
        path = Application.streamingAssetsPath + "/Package_Win/SceneABS/";
        BuildScenes(BuildTarget.StandaloneWindows64,path);
    }
    [MenuItem("打包/Windows/资源包")]
    public static void BuildAbsWindows()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Package_Win/RescurceABs/";
        path = Application.streamingAssetsPath + "/Package_Win/RescurceABs/";
        BuildAssetBundles(BuildTarget.StandaloneWindows64,path);
    }
    // 打包AssetBundles
    private static void BuildAssetBundles(BuildTarget platform,string path)
    {
        // 输出路径
        //string outPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Package_Win/RescurceABs/";
        string outPath = path;
        if (!Directory.Exists(outPath)) Directory.CreateDirectory(outPath);

        EditorUtility.DisplayProgressBar("信息", "打包资源包", 0f);
        BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.DeterministicAssetBundle, platform);
        AssetDatabase.Refresh();
        Debug.Log("所有资源包打包完毕");
    }

    // 打包Scenes
    private static void BuildScenes(BuildTarget platform,string path)
    {
        // 指定场景文件夹  //Application.dataPath =》Assets文件下
        string scenePath = Application.dataPath + "/ABScenes";

        //输出路径
        //string outPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Package_Win/SceneABS/";
        string outPath = path;

        //string[] scenes = GetAllFiles(scenePath, "*.unity");
        //for (int i = 0; i < scenes.Length; i++)
        //{
        //    string path = scenes[i].Replace("\\", "/");
        //    // 注意这里【区别】通常我们打包，第2个参数都是指定文件夹目录，在此方法中，此参数表示具体【打包后文件的名字】
        //    // 记得指定目标平台，不同平台的打包文件是不可以通用的。最后的BuildOptions要选择流格式
        //    BuildPipeline.BuildPlayer(levels, Application.dataPath + "/Scene.unity3d", BuildTarget.Android, BuildOptions.BuildAdditionalStreamedScenes);
        //    // 刷新，可以直接在Unity工程中看见打包后的文件
        //    AssetDatabase.Refresh();
        //}
        if (Directory.Exists(scenePath))
        {
            Debug.Log("开始打包");
            // 创建输出文件夹
            if (!Directory.Exists(outPath)) Directory.CreateDirectory(outPath);

            // 查找指定目录下的场景文件
            string[] scenes = GetAllFiles(scenePath, "*.unity");
            for (int i = 0; i < scenes.Length; i++)
            {
                string url = scenes[i].Replace("\\", "/");
                string[] scene_s = { url };
                int index = url.LastIndexOf("/");
                string scene = url.Substring(index + 1, url.Length - index - 1);
                string msg = string.Format("打包场景{0}", scene);
                EditorUtility.DisplayProgressBar("信息", msg, 0f);
                scene = scene.Replace(".unity", ".ab");
                Debug.Log(string.Format("打包场景{0}到{1}", url, outPath + scene));
                //参数：1、
                //2、输出路径
                //3、打包平台
                BuildPipeline.BuildPlayer(scene_s, outPath + scene, platform, BuildOptions.BuildAdditionalStreamedScenes);
                AssetDatabase.Refresh();
            }
            EditorUtility.ClearProgressBar();
            Debug.Log("所有场景打包完毕");
        }
    }

    /// <summary> 获取文件夹和子文件夹下所有指定类型文件 </summary>
    private static string[] GetAllFiles(string directory, params string[] types)
    {
        if (!Directory.Exists(directory))
            return new string[0];
        string searchTypes = (types == null || types.Length == 0) ? "*.*" : string.Join("|", types);
        string[] names = Directory.GetFiles(directory, searchTypes, SearchOption.AllDirectories);
        return names;
    }


    private static void Test(string path)
    {
        if (Directory.Exists(path))
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            FileInfo[] files = direction.GetFiles("*");
            for (int i = 0; i < files.Length; i++)
            {
                //忽略关联文件
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                Debug.Log("文件名:" + files[i].Name);
                Debug.Log("文件绝对路径:" + files[i].FullName);
                Debug.Log("文件所在目录:" + files[i].DirectoryName);
            }
        }
    }
#endif


}

