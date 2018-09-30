using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using System.Text;
using System.IO;

public class EasyCodeCreateLua : MonoBehaviour {

    /*
     * 创建Lua

     *           
     */

    static string scriptName = "SoundController";
    static string scriptsFile = Application.dataPath + "/Temp/";
    static string tab = "    ";
    static StringBuilder sb;

    [MenuItem("Assets/Create/LuaScript",false,0)]
    public static void CreatePoolMgr()
    {


        sb = new StringBuilder();


        CreateFunction("function","awake" );
        CreateFunction("function", "start");
        CreateFunction("function", "update");
        CreateFunction("function", "ondestroy");





        if (!Directory.Exists(scriptsFile))
            Directory.CreateDirectory(scriptsFile);
        File.WriteAllText(scriptsFile + scriptName + ".lua.txt", sb.ToString(), Encoding.UTF8);

        AssetDatabase.Refresh();
    }


    /// <summary>
    /// 函数
    /// </summary>
    /// <param name="paragraphTittle">函数名</param>
    /// <param name="paragraph">行</param>
    public static void CreateFunction(string returnTittle, string paragraphTittle, params string[] paragraph)
    {
        sb.AppendLine();
        sb.AppendLine(returnTittle + " " + paragraphTittle + "()");

        for(int i=0;i<paragraph.Length;i++)
        {
            sb.Append(tab);
            sb.Append(tab);
            sb.AppendLine(paragraph[i]);
        }

        sb.AppendLine();
        sb.AppendLine("end");
        sb.AppendLine();
    }


}
