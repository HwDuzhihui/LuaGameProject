

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XLua;
using System;


[LuaCallCSharp]
public class LuaBehaviourMono : MonoBehaviour {


    public TextAsset luaScript;

    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;

    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnDestroy;

    public LuaTable scriptEnv;

    void Awake()
    {
        if (luaScript == null)
        {
            Debug.Log("awake lusscript = null");
            return;
        }

        scriptEnv = LuaMgr.luaEnv.NewTable();

        LuaTable meta = LuaMgr.luaEnv.NewTable();
        meta.Set("__index", LuaMgr.luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", this);

        LuaMgr.luaEnv.DoString(luaScript.text, luaScript.text, scriptEnv);

        Action luaAwake = scriptEnv.Get<Action>("awake");
        scriptEnv.Get("start", out luaStart);
        scriptEnv.Get("update", out luaUpdate);
        scriptEnv.Get("ondestroy", out luaOnDestroy);

        if (luaAwake != null) luaAwake();
    }

	void Start ()
    {
        if (luaStart != null) luaStart();
    }
	void Update ()
    {
        if (luaUpdate != null) luaUpdate();
        if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
        {
            LuaMgr.luaEnv.Tick();
            LuaBehaviour.lastGCTime = Time.time;
        }
	}
    void OnDestroy()
    {
        if (luaOnDestroy != null) luaOnDestroy();
        luaOnDestroy = null;
        luaUpdate = null;
        luaStart = null;
        if (scriptEnv != null) scriptEnv.Dispose();
    }



    public void LoadLuaAsset (TextAsset _luaScript)
    {
        luaScript = _luaScript;

        scriptEnv = LuaMgr.luaEnv.NewTable();

        //LuaTable meta = LuaMgr.luaEnv.NewTable();
        //meta.Set("__index", LuaMgr.luaEnv.Global);
        //scriptEnv.SetMetaTable(meta);
        //meta.Dispose();

        scriptEnv.Set("self", this);

        //LuaMgr.luaEnv.DoString(luaScript.text, luaScript.text, scriptEnv);

        //Action luaAwake = scriptEnv.Get<Action>("awake");
        //scriptEnv.Get("start", out luaStart);
        //scriptEnv.Get("update", out luaUpdate);
        //scriptEnv.Get("ondestroy", out luaOnDestroy);

        //if (luaAwake != null) luaAwake();
    }
}
