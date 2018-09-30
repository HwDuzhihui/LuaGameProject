using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;
using System.Text;
using System;

[Hotfix]
public class LuaMgr : MonoBehaviour {


    public static LuaEnv luaEnv = new LuaEnv();


    public TextAsset luaScript;

    void Awake () {

        //1自定义地址加载，或者只能放在Resources文件夹下
        //luaEnv.AddLoader(CustomLoaderLuaFile);
        //luaEnv.DoString("require 'EffectMgr'");


        //热修复的脚本打上[Hotfix]的标签,编辑器加HOTFIX_ENABLE标签，生成代码、注入，见GameMgr.lua
        LuaTable scriptEnv = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        scriptEnv.Set("self", this);

        luaEnv.DoString(luaScript.text, luaScript.text, scriptEnv);

        Action luaAwake = scriptEnv.Get<Action>("awake");
        if (luaAwake != null) luaAwake();

    }
	

	void Update () {

        Debug.Log("C#Update");

	}

    //1
    //byte[] CustomLoaderLuaFile(ref string luaName)
    //{
    //    string path = Application.dataPath + "/Temp/" + luaName + ".lua.txt";
    //    return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(path));
    //}



}
