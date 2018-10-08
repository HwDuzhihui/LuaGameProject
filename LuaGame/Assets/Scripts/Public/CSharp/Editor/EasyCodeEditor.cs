using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class EasyCodeEditor : MonoBehaviour {

    [MenuItem("EasyCodeEditor/删档")]
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("EasyCodeEditor/制作声音预制体")]
    public static void RePlaceImage()
    {
        string prefabPath = "Assets/Resources/AudioPrefab/";
        if (!Directory.Exists(prefabPath))
        {
            Directory.CreateDirectory(prefabPath);
        }

        List<AudioClip> audios = EasyCode.LoadAllResource<AudioClip>("AudioClip");

        List<GameObject> o = new List<GameObject>();
        for (int i = 0; i < audios.Count; i++)
        {
            Object tempPrefab = PrefabUtility.CreateEmptyPrefab(prefabPath + audios[i].name + ".prefab");

            GameObject last = new GameObject(audios[i].name);
            last.transform.localPosition = Vector3.zero;
            last.AddComponent<SoundController>();
            AudioSource audio = last.AddComponent<AudioSource>();
            audio.clip = audios[i];
            audio.playOnAwake = false;
            tempPrefab = PrefabUtility.ReplacePrefab(last, tempPrefab);

            o.Add(last);
        }
        for (int i = 0; i < o.Count; i++)
            DestroyImmediate(o[i]);
    }


    //把文件拷到工程里
    static string filePath = "E:\\LuaGameProject\\excelToProtobuf\\tool\\lua\\";
    static string projectPath = "E:\\LuaGameProject\\LuaGame\\Assets\\Scripts\\Lua\\Resources\\";
    [MenuItem("EasyCodeEditor/把lua数据拷到工程")]
    public static void MoveLuaFile()
    {
        if (!Directory.Exists(filePath))
        {
            Debug.Log("没有 " + filePath);
            return;
        }
        if (!Directory.Exists(projectPath))
        {
            //Debug.Log("没有 " + projectPath);
            Directory.CreateDirectory(projectPath);
            return;
        }

        DirectoryInfo di = new DirectoryInfo(filePath);
        foreach (var fi in di.GetFiles("*.lua", SearchOption.AllDirectories))
        {
            string fileName = Path.GetFileName(fi.FullName);

            if(!fileName.StartsWith("_"))
            {
                File.Copy(fi.FullName, projectPath + Path.GetFileName(fi.FullName) + ".txt", true);
            }
        }

        AssetDatabase.Refresh();
    }
}
