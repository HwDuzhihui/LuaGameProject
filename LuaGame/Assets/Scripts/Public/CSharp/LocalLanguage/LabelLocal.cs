using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelLocal : MonoBehaviour {

    public int languageId;

	void Start () {

        Text t = GetComponent<Text>();

        if(t != null)
        {
            t.text = LanguageMgr.Instance.GetById(languageId);
        }

	}
	
}
