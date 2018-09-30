using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class Lauch : MonoBehaviour {

    Image load;
	void Start () {
        AsyncOperation scene = SceneManager.LoadSceneAsync("UI");
        scene.allowSceneActivation = false;

        load = GetComponent<Image>();
        Color c = load.color;
        DOTween.To(() => c.a, x => c.a = x, 0, 1).OnUpdate(()=>
        {
            load.color = new Color(c.r, c.g, c.b, c.a);
        }).OnComplete(()=>
        {
            scene.allowSceneActivation = true;
        });
    }
	
	// Update is called once per frame
	void Update () {
        //Color c = load.color;
        //float a = c.a -= Time.deltaTime;
        //if(a < 0)
        //{
        //    SceneManager.LoadScene("UI");
        //}
        //else
        //{
        //    load.color = new Color(c.r, c.g, c.b, a);
        //}
	}
}
