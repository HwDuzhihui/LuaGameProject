using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageMgr : MonoBehaviour {

    public static LanguageMgr Instance;
    public enum LanguageType
    {
        cn,
        cnt,
        en,
        korean,
        japan,
    }
    public LanguageType type = LanguageType.cn;

	void Awake () {
        Instance = this;


        if(Application.systemLanguage == SystemLanguage.Chinese)
        {
            type = LanguageType.cn;
        }
        else if (Application.systemLanguage == SystemLanguage.ChineseSimplified)
        {
            type = LanguageType.cn;
        }
        else if (Application.systemLanguage == SystemLanguage.ChineseTraditional)
        {
            type = LanguageType.cnt;
        }
        else if(Application.systemLanguage == SystemLanguage.English)
        {
            type = LanguageType.en;
        }
        //else if (Application.systemLanguage == SystemLanguage.Japanese)
        //{
        //    type = LanguageType.japan;
        //}
        //else if (Application.systemLanguage == SystemLanguage.Korean)
        //{
        //    type = LanguageType.korean;
        //}
        else
        {
            type = LanguageType.en;
        }

    }
	
    public void ChangeLanguage(LanguageType _type)
    {
        type = _type;

        UpdateLanguage();
    }
    public void UpdateLanguage()
    {
        LabelLocal[] locals = FindObjectsOfType<LabelLocal>();

        for(int i=0;i<locals.Length;i++)
        {
            Text t = locals[i].GetComponent<Text>();

            if(t != null)
            {
                LanguageConfigInfo info = LanguageConfig.singleton.m_LanguageConfigInfo[locals[i].languageId];

                if(type == LanguageType.cn)
                {
                    t.text = info.cn;
                }
                else if(type == LanguageType.cnt)
                {
                    t.text = info.tcn;
                }
                else if (type == LanguageType.en)
                {
                    t.text = info.en;
                }
                else if (type == LanguageType.japan)
                {
                    t.text = info.japan;
                }
                else if (type == LanguageType.korean)
                {
                    t.text = info.korean;
                }

            }
        }
    }

    public string GetById(int id)
    {
        if (!LanguageConfig.singleton.m_LanguageConfigInfo.ContainsKey(id)) return "";

        LanguageConfigInfo info = LanguageConfig.singleton.m_LanguageConfigInfo[id];

        if (type == LanguageType.cn)
        {
            return info.cn;
        }
        else if (type == LanguageType.cnt)
        {
            return info.tcn;
        }
        else if (type == LanguageType.en)
        {
            return info.en;
        }
        else if (type == LanguageType.japan)
        {
            return info.japan;
        }
        else if (type == LanguageType.korean)
        {
            return info.korean;
        }

        return info.en;
    }


}
