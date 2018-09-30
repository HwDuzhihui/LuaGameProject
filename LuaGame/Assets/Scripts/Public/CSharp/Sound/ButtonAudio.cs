using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour {


    public int soundId = 0;

	void Start () {
        Button btn = GetComponent<Button>();
        if(btn != null)
        {
            btn.onClick.AddListener(Play);
        }
	}
	
    void Play()
    {
        SoundMgr.Instance.PlaySound(soundId);
    }
}
