using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class SoundConfigInfo
{
    public int id;
    public string path;
}

public class SoundConfig : MonoBehaviour
{
    private static SoundConfig m_SoundConfig;
    public static SoundConfig singleton{ get{ return m_SoundConfig;}}
    public Dictionary<int,SoundConfigInfo> m_SoundConfigInfo = new Dictionary<int,SoundConfigInfo>();
    
    void Awake()
    {
        m_SoundConfig = this;
        LoadXml();
    }
    void LoadXml()
    {
        TextAsset t;
        string data = Resources.Load("Xml/SoundConfig").ToString();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(data);
        XmlNodeList nodeList = xmlDoc.SelectNodes("root / node");
        SoundConfigInfo info = null;
        foreach (XmlNode xmlNode in nodeList)
        {
            info = new SoundConfigInfo();
            foreach (XmlAttribute s in xmlNode.Attributes)
            {
                switch (s.Name)
                {
                    case "int_id":
                        info.id = int.Parse(s.Value);
                        break;
                    case "string_path":
                        info.path = s.Value;
                        break;
                }
            }
            m_SoundConfigInfo[info.id] = info;
        }
    }
}
