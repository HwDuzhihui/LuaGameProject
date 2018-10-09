using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.IO;
using UnityEditor;

public class CreateBundle : MonoBehaviour {


    public static string outPut = Application.dataPath + "/StreamingAssets/Bundle/";
    public static string outPutPersistent = Application.persistentDataPath + "/Bundle/";

    [MenuItem("Assets/BuildBundles")]
    [MenuItem("Tools/BuildBundles")]
    public static void BuidldBundles()
    {

        if (!Directory.Exists(outPut))
        {
            Directory.CreateDirectory(outPut);
        }
        BuildPipeline.BuildAssetBundles(outPut, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);


        if(!Directory.Exists(outPutPersistent))
        {
            Directory.CreateDirectory(outPutPersistent);
        }

        CopyDirectory(outPut, outPutPersistent);

        AssetDatabase.Refresh();

        Debug.Log(outPutPersistent);


        

    }

    /// <summary>
    /// 复制文件夹
    /// </summary>
    /// <param name="srcPath"></param>
    /// <param name="destPath"></param>
    public static void CopyDirectory(string srcPath, string destPath)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)     //判断是否文件夹
                {
                    if (!Directory.Exists(destPath + "\\" + i.Name))
                    {
                        Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                    }
                    CopyDirectory(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
                }
                else
                {
                    File.Copy(i.FullName, destPath + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            throw;
        }
    }


    //static string outPutAndroid = Application.dataPath + "/StreamingAssets/Android";
    //static string outPutIOS = Application.dataPath + "/StreamingAssets/IOS";
    //static string suffix = "ab";
    //[MenuItem("Assets/BuildBundles/Android")]
    //[MenuItem("Tools/BuildBundles/Android")]
    //public static void BuidldBundlesAndroid()
    //{
    //    BuildBundles(outPutAndroid, BuildTarget.Android);
    //}
    //[MenuItem("Assets/BuildBundles/IOS")]
    //[MenuItem("Tools/BuildBundles/IOS")]
    //public static void BuidldBundlesIOS()
    //{
    //    BuildBundles(outPutIOS, BuildTarget.iOS);
    //}
    //static void BuildBundlesOne(string folder,BuildTarget platform)
    //{
    //    if(!Directory.Exists(folder))
    //    {
    //        Directory.CreateDirectory(folder);
    //    }

    //    AssetDatabase.RemoveUnusedAssetBundleNames();
    //    AssetBundleBuild[] builds = new AssetBundleBuild[1];
    //    UnityEngine.Object[] selects = Selection.GetFiltered<UnityEngine.Object>(SelectionMode.DeepAssets);
    //    string[] textAssets = new string[selects.Length];
    //    for (int i = 0; i < selects.Length; i++)
    //    {
    //        //文件路径
    //        string path = AssetDatabase.GetAssetPath(selects[i]);
    //        //把一个目录的对象检索为AssetImporter
    //        AssetImporter asset = AssetImporter.GetAtPath(path);
    //        asset.assetBundleName = selects[i].name;
    //        asset.assetBundleVariant = "unity3d";
    //        asset.SaveAndReimport();
    //    }
    //    BuildPipeline.BuildAssetBundles(folder, BuildAssetBundleOptions.None, platform);

    //    AssetDatabase.Refresh();
    //}

    //    O BuildAssetBundleOptions.None  --杓建AssetBundle投有任何持殊的选项
    //    1 BuildissetBundleOptions.UnccmpressedAssetBundle  --不进行数据圧縮。如果使用垓項,因カ投有圧縮解圧縮的辻程,AssetBundle的友布和加戟会很快,但是AssetBundle也会更大,下載変慢
    //    2 BuildAssetBundleOptions.ColleetDependencies  --包合所有依頼美系
    //    4 BuildAssetBundleOptions.CompleteAssets  --強制包括整个資源
    //    8 BuildAssetBundleOptions.DisableriteTypeTree  blog--在AssetBundle中不包合業型信息 岌布web平台吋,不能使用垓項
    //   16 BuildassetBundleoptions.DeterministickssetBundle -- 使毎个obect具有唯-不変的hash ID, 可用干増量式安布AssetBundle
    //   32 BuildssetBundleptions.ForceRebuildAssetBundle-強 制重新Bui 1d所有的AssetBundle
    //   64 BuildissetBundleoptions.IgnoreTypeTreeChanges  -- 忽略TypeTree的変化, 不能与DisablerypeTree同吋使用
    //  128 BuildAssetBundleOptions.AppendlashToAssetBundleName --附hash到AssetBundle名称中
    //  256 BuildAssetBundleOptions.ChunkBasedCompression - Assetbundle的圧 鏥格式カ1z4.默圦的是lzaa格式, 下戟assetbundle后 立即解圧。
    //                                                       而l格式的Assetbundle会在加載資源的吋候オ迸行解圧, 只是解圧資源的吋机不一祥


}
