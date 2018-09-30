using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class Clean2DScripts : MonoBehaviour {


    //拿到动画后只需把场景和资源拖到Resources下的Operation2D目录，打开场景
    //点第一步
    //点第二步
    //检查有没有个别缺失(自己拖一下)
    //点第三步
    

    [MenuItem("EasyCodeEditor/批量制作预制体网格(第一步)")]
    public static void ShowAll()
    {
        Animation[] anima = FindObjectsOfType<Animation>();
        for (int i = 0; i < anima.Length; i++)
        {
            Transform trans = anima[i].transform;

            for (int j = 0; j < trans.childCount; j++)
            {
                if (trans.GetChild(j).gameObject.activeSelf == false)
                    trans.GetChild(j).gameObject.SetActive(true);
            }
        }
        //先创建一个空的预制物体
        for (int i = 0; i < anima.Length; i++)
        {
            Object tempPrefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Operation2D/" + anima[i].name + ".prefab");
            //然后拿我们场景中的物体替换空的预制物体
            tempPrefab = PrefabUtility.ReplacePrefab(anima[i].gameObject, tempPrefab);
        }
    }


    //填充网格
    [MenuItem("EasyCodeEditor/填充丢失的网格(第二步)")]
    public static void FillMesh()
    {
        Debug.Log("Time1 + " + Time.time);
        ////删除脚本
        //Uni2DSprite[] spr = FindObjectsOfType<Uni2DSprite>();
        //for (int i = 0; i < spr.Length; i++)
        //    DestroyImmediate(spr[i]);

        //Uni2DSmoothBindingBone[] sprBone = FindObjectsOfType<Uni2DSmoothBindingBone>();
        //for (int i = 0; i < sprBone.Length; i++)
        //    DestroyImmediate(sprBone[i]);

        Debug.Log("Time2 + " + Time.time);
        //生成网格后这个预制体已经没用了
        Animation[] anima = FindObjectsOfType<Animation>();
        for (int i = 0; i < anima.Length; i++)
        {
            string prefabPath = "Assets/Resources/Operation2D/" + anima[i].name + ".prefab";
            System.IO.File.Delete(prefabPath);
        }

        
        List<string> nameList = new List<string>();
        for(int i=0;i<anima.Length;i++)
        {
            //资源目录名称
            string resPath = "Operation2D/" + anima[i].name + "_Resources/";
            nameList.Add(resPath);
        }

        for(int i=0;i<anima.Length;i++)
        {
            Transform trans = anima[i].transform;
            for(int j=0;j<trans.childCount;j++)
            {
                Transform child = trans.GetChild(j);


                string meshPath = nameList[i]+ child.name + "/" +"mesh_" + child.name;
                string materialPath = nameList[i] + child.name + "/" + "mat_Generated_" + child.name;

                Mesh NewMesh = Resources.Load(meshPath) as Mesh;
                Material NewMat = Resources.Load(materialPath) as Material;

                //老的用这一版
                ////美术命名可能不规范，这里有可能会找不到，查找一下可能的命名
                //if (NewMesh == null)
                //{
                //    for (int m = 0; i < 4; m++)    //只找4次，找不到就算了
                //    {
                //        string meshPathSub = meshPath.Substring(0, meshPath.Length - 1);
                //        NewMesh = Resources.Load(meshPathSub) as Mesh;
                //        if (NewMesh != null)
                //            break;
                //        if (m == 3)
                //            Debug.Log("没找到" + meshPath);
                //    }
                //}
                //if (NewMat == null)
                //{
                //    for (int m = 0; m < 4; m++)
                //    {
                //        string matPathSub = materialPath.Substring(0, materialPath.Length - 1);
                //        NewMat = Resources.Load(matPathSub) as Material;
                //        if (NewMat != null)
                //            break;
                //        if (m == 3)
                //            Debug.Log("没找到" + materialPath);
                //    }
                //}

                //命名规范了用这一版,只找一次
                if(NewMesh == null)
                {
                    string meshPathSub = meshPath.Substring(0, meshPath.Length - 1);
                    NewMesh = Resources.Load(meshPathSub) as Mesh;
                    if (NewMesh == null)
                        Debug.Log("没找到");
                }
                if(NewMat == null)
                {
                    string matPathSub = materialPath.Substring(0, materialPath.Length - 1);
                    NewMat = Resources.Load(matPathSub) as Material;
                    if (NewMat != null)
                        Debug.Log("没找到");
                }

                MeshFilter filter = child.GetComponent<MeshFilter>();
                filter.mesh = NewMesh;

                MeshRenderer meshRender = child.GetComponent<MeshRenderer>();
                SkinnedMeshRenderer skin = child.GetComponent<SkinnedMeshRenderer>();

                if(meshRender != null)
                {
                    //一定要用数组进行赋值，不然拖成预制体材质会丢失
                    List<Material> mat = new List<Material>() { NewMat };
                    meshRender.materials = mat.ToArray();
                }
                if(skin != null)
                {
                    List<Material> mat = new List<Material>() { NewMat };
                    skin.materials = mat.ToArray();
                    skin.sharedMesh = NewMesh;
                }
            }
        }
        MoveResources();
    }

    //[MenuItem("EasyCodeEditor/挪动网格")]
    public static void MoveResources()
    {
        List<string> nameList = new List<string>();

        Animation[] anima = FindObjectsOfType<Animation>();
        for (int i = 0; i < anima.Length; i++)
        {
            string resPath = Application.dataPath + "/Resources/" + "Operation2D/" + anima[i].name + "_Resources";
            nameList.Add(resPath);
        }

        for (int i=0;i<anima.Length;i++)
        {
            if (!Directory.Exists(nameList[i]))
            {
                Debug.LogError("不要随意挪动文件夹，本次操作无效" + nameList[i]);
                continue;
            }
            Transform trans = anima[i].transform;
            for (int j = 0; j < trans.childCount; j++)
            {
                Transform child = trans.GetChild(j);


                string meshPath = nameList[i] + "/" + child.name + "/" + "mesh_" + child.name;
                string materialPath = nameList[i] + "/" + child.name + "/" + "mat_Generated_" + child.name;

                if (!File.Exists(meshPath +".asset"))
                {
                    for(int m=0;m<4;m++) //暂时只查找后4位
                    {
                        meshPath = meshPath.Substring(0, meshPath.Length - 1);
                        if (File.Exists(meshPath + ".asset"))
                            break;
                        if(m == 3)
                            Debug.Log("文件没找到" + meshPath);
                    }
                }
                if (!File.Exists(materialPath + ".mat"))
                {
                    for(int m=0;m<4;m++)
                    {
                        materialPath = materialPath.Substring(0, materialPath.Length - 1);
                        if (File.Exists(materialPath + ".mat"))
                            break;
                        if(m == 3)
                        {
                            Debug.Log("文件没找到" + materialPath);
                            continue;
                        }
                    }
                }
                string NewMeshPath = nameList[i] + "/" + child.name + i + "_0_1" + ".asset";
                string NewMaterialPath = nameList[i] + "/" + child.name + i + "_0_2" + ".mat";

                string NewMeshPathMeta = nameList[i] + "/" + child.name + i + "_0_1" + ".asset" +".meta";
                string NewMaterialPathMeta = nameList[i] + "/" + child.name + i + "_0_2" + ".mat" + ".meta";


                System.IO.File.Move(meshPath + ".asset", NewMeshPath);
                System.IO.File.Move(materialPath + ".mat", NewMaterialPath);

                //文件信息存在meta里面，也要挪过去
                System.IO.File.Move(meshPath + ".asset" + ".meta", NewMeshPathMeta);
                System.IO.File.Move(materialPath + ".mat" + ".meta", NewMaterialPathMeta);
            }

        }
    }

    [MenuItem("EasyCodeEditor/删除空文件夹(第三步)")]
    public static void DeleteFlood()
    {
        List<string> nameList = new List<string>();

        Animation[] anima = FindObjectsOfType<Animation>();
        for (int i = 0; i < anima.Length; i++)
        {
            string resPath = Application.dataPath + "/Resources/" + "Operation2D/" + anima[i].name + "_Resources";
            nameList.Add(resPath);
        }

        for (int i = 0; i < anima.Length; i++)
        {
            if (!Directory.Exists(nameList[i]))
                continue;

            Transform trans = anima[i].transform;
            for (int j = 0; j < trans.childCount; j++)
            {
                Transform child = trans.GetChild(j);

                string path = nameList[i] + "/" + child.name;
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                    if (files.Length == 0)
                        Directory.Delete(path);
                }
            }
        }

        for (int i = 0; i < anima.Length; i++)
        {
            Object tempPrefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Operation2D/" + anima[i].name + ".prefab");
            tempPrefab = PrefabUtility.ReplacePrefab(anima[i].gameObject, tempPrefab);
        }
    }

}
