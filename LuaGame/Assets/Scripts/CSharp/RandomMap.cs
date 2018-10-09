﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//随机地图类

public class RandomMap : MonoBehaviour {

    /// <summary>
    /// 地图基本类型
    /// 0,地板
    /// </summary>
    public GameObject[] worldItem;
    /// <summary>
    /// 活体
    /// </summary>
    public GameObject[] lives;
    /// <summary>
    /// 花草树木
    /// </summary>
    public GameObject[] forest;

	void Start () {
		
	}


    /// <summary>
    /// 创建世界
    /// </summary>
    public void CreateWorld()
    {
        //平地,最底的一层,其他的河流山峦等都在上面
        Instantiate(worldItem[0]);
    }
	
}
