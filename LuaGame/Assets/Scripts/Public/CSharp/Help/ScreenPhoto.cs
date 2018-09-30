using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class ScreenPhoto : MonoBehaviour {

    KeyCode key = KeyCode.W;

	
	void Update()
    {
		
        if(Input.GetKeyDown(key))
        {
            Capture();
        }
	}


    [MenuItem("EasyCodeEditor/截图")]
    public static void Capture()
    {
        int name = Random.Range(0, 10000);
        string file = Application.persistentDataPath + "/" + name + ".png";

        while (File.Exists(file))
        {
            name = Random.Range(0, 10000);
            file = Application.persistentDataPath + "/" + name + ".png";
        }


        ScreenCapture.CaptureScreenshot(file);
        Debug.Log(file);
    }

}
