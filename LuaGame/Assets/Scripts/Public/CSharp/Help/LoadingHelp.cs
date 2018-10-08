using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LoadingHelp : MonoBehaviour {

    //封装一下
    public bool resourcesStreaming = true;


    public Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// 加载一个Bundle,如果分成了两个bundleName,但是一个bundle引用了另一个bundle的东西，那么就要两个bundle都要加载，不然会出现丢失的情况
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="littleName"></param>
    public GameObject LoadBundleInPersistent(string bundleName, string littleName)
    {
        AssetBundle bundle;
        if (bundles.ContainsKey(bundleName))
        {
            bundle = bundles[bundleName];
        }
        else
        {
            string bundlePath = Application.persistentDataPath + bundleName;

            bundle = AssetBundle.LoadFromFile(bundlePath);

            bundles.Add(bundleName, bundle);
        }

        GameObject o = bundle.LoadAsset<GameObject>(littleName);

        return o;
    }

    /// <summary>
    /// 卸载bundle
    /// </summary>
    /// <param name="bundleName"></param>
    public void BundleUnload(string bundleName)
    {
        if(bundles.ContainsKey(bundleName))
        {
            if(bundles[bundleName] != null)
            {
                bundles[bundleName].Unload(false);  //true代表正在使用的也会清除
            }
        }
    }



}
