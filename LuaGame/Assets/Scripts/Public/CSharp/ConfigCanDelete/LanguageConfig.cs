using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class LanguageConfigInfo
{
    public int id;
    public string cn;
    public string tcn;
    public string en;
    public string korean;
    public string japan;
}

public class LanguageConfig : MonoBehaviour
{
    private static LanguageConfig m_LanguageConfig;
    public static LanguageConfig singleton{ get{ return m_LanguageConfig;}}
    public Dictionary<int,LanguageConfigInfo> m_LanguageConfigInfo = new Dictionary<int,LanguageConfigInfo>();
    
    void Awake()
    {
        m_LanguageConfig = this;
        LoadXml();
    }
    void LoadXml()
    {
        TextAsset t;
        string data = Resources.Load("Xml/LanguageConfig").ToString();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(data);
        XmlNodeList nodeList = xmlDoc.SelectNodes("root / node");
        LanguageConfigInfo info = null;
        foreach (XmlNode xmlNode in nodeList)
        {
            info = new LanguageConfigInfo();
            foreach (XmlAttribute s in xmlNode.Attributes)
            {
                switch (s.Name)
                {
                    case "int_id":
                        info.id = int.Parse(s.Value);
                        break;
                    case "string_cn":
                        info.cn = s.Value;
                        break;
                    case "string_tcn":
                        info.tcn = s.Value;
                        break;
                    case "string_en":
                        info.en = s.Value;
                        break;
                    case "string_korean":
                        info.korean = s.Value;
                        break;
                    case "string_japan":
                        info.japan = s.Value;
                        break;
                }
            }
            m_LanguageConfigInfo[info.id] = info;
        }
    }
}
