using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using System.Text;
using System.IO;

public class EasyCodePoolMgrEditor : MonoBehaviour {

    /*
     * 代码编辑器
     * 
     * 使用方法：把想要的属性填在CreateProperty,可变的代码填在CreateFunction
     *           修改scriptName,保存再点击生成
     *           
     */

    static string scriptName = "EffectMgr";
    static string controllerName = "EffectController";
    static string scriptsFile = Application.dataPath + "/Scripts/";
    static string tab = "    ";
    static StringBuilder sb;

    [MenuItem("EasyCodeEditor/CreateVariableCode")]
    public static void CreatePoolMgr()
    {
        sb = new StringBuilder();


        CreateHead();
        CreateProperty
            (
            "public static " + scriptName + " singleton" + ";",
            "public Dictionary<int," + "List<" + controllerName + ">>" + " m_" + scriptName + "Info = new Dictionary<int,List<" + controllerName + ">>();"
            );


        CreateFunction
            (
            "void",
            "Awake",
            "singleton = this;"
            );
        CreateFunction
            (
            "void",
            "Pool"

            );
        CreateTail();






        if (!Directory.Exists(scriptsFile))
            Directory.CreateDirectory(scriptsFile);
        File.WriteAllText(scriptsFile + scriptName + ".cs", sb.ToString(), Encoding.UTF8);

        AssetDatabase.Refresh();
    }



    /// <summary>
    /// 头
    /// </summary>
    /// <param name="_sb"></param>
    public static void CreateHead()
    {
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("using System.Collections;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine();

        sb.AppendLine("public class " + scriptName + " : MonoBehaviour");
        sb.AppendLine("{");
    }

    /// <summary>
    /// 属性
    /// </summary>
    /// <param name="property"></param>
    public static void CreateProperty(params string[] property)
    {
        sb.AppendLine();

        for(int i=0;i<property.Length;i++)
        {
            sb.Append(tab);
            sb.AppendLine(property[i]);
        }

        sb.AppendLine();
    }

    /// <summary>
    /// 函数
    /// </summary>
    /// <param name="paragraphTittle">函数名</param>
    /// <param name="paragraph">行</param>
    public static void CreateFunction(string returnTittle, string paragraphTittle, params string[] paragraph)
    {
        sb.AppendLine();
        sb.Append(tab);
        sb.AppendLine(returnTittle + " " + paragraphTittle + "()");
        sb.Append(tab);
        sb.AppendLine("{");

        for(int i=0;i<paragraph.Length;i++)
        {
            sb.Append(tab);
            sb.Append(tab);
            sb.AppendLine(paragraph[i]);
        }

        sb.AppendLine();
        sb.Append(tab);
        sb.AppendLine("}");
        sb.AppendLine();
    }

    /// <summary>
    /// 尾
    /// </summary>
    public static void CreateTail()
    {
        sb.AppendLine();
        sb.AppendLine("}");
    }
}
