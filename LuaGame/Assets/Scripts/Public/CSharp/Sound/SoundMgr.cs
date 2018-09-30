using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundMgr : MonoBehaviour {

    public static SoundMgr Instance;
    public Transform soundNode;
    //小声音
    public Dictionary<int, List<SoundController>> audios = new Dictionary<int, List<SoundController>>();
    //bgm
    public Dictionary<int, SoundController> bgmList = new Dictionary<int, SoundController>();

    public bool switchAudio = true;
    public bool switchBGM = true;
    string audioKey = "audio_";
    string bgmKey = "bgm_";

	void Awake () {
        Instance = this;

        Init();
    }
    void Start()
    {
        Pool(5, 2);
    }
	//播声音(路径配表)
    public void PlaySound(int id)
    {
        if(audios.ContainsKey(id))
        {
            for (int i = 0; i < audios[id].Count; i++)
            {
                if (!audios[id][i].IsPlaying)
                {
                    audios[id][i].Play();
                    return;
                }
                audios[id][0].Play();
            }
        }
        else
        {
            string path = SoundConfig.singleton.m_SoundConfigInfo[id].path;

            GameObject o = Resources.Load<GameObject>(path);

            GameObject sound = Instantiate(o);
            sound.transform.position = Vector3.zero;
            sound.transform.SetParent(soundNode);
            SoundController s = sound.GetComponent<SoundController>();

            if(s.isBGM)
            {
                bgmList[id] = s;
            }
            else
            {
                audios[id] = new List<SoundController>() { s };
            }
            s.Play();
        }

    }
    public SoundController GetSound(int id)
    {
        if(audios.ContainsKey(id))
        {
            return audios[id][0];
        }
        return null;
    }
    //停掉声音
    public void StopSound(int id)
    {
        if(audios.ContainsKey(id))
        {
            for(int i=0;i<audios[id].Count;i++)
            {
                if(audios[id][i].IsPlaying || audios[id][i].IsPause)
                    audios[id][i].Stop();
            }
        }
    }
    public void PauseSound(int id)
    {
        if (audios.ContainsKey(id))
        {
            for (int i = 0; i < audios[id].Count; i++)
            {
                if (audios[id][i].IsPlaying)
                    audios[id][i].Pause();
            }
        }
    }
    public void StopBGM(int id)
    {
        if(bgmList.ContainsKey(id))
        {
            bgmList[id].Stop();
        }
    }
    //播放声音时关掉某些别的声音
    public void PlaySoundStopOthers(int id,params int[] others)
    {
        PlaySound(id);

        for(int i=0;i<others.Length;i++)
        {
            if(audios.ContainsKey(others[i]))
            {
                for(int j=0;j<audios[i].Count;j++)
                {
                    if(audios[i][j].IsPlaying)
                    {
                        audios[i][j].Stop();
                    }
                }
            }
        }
    }

    void Init()
    {
        switchAudio = System.Convert.ToBoolean(PlayerPrefs.GetInt(audioKey,1));
        switchBGM = System.Convert.ToBoolean(PlayerPrefs.GetInt(bgmKey,1));
    }
    //关掉小声音
    public void MuteAudio()
    {

        foreach (var a in audios)
        {
            for (int i = 0; i < a.Value.Count; i++)
            {
                if (a.Value[i].IsPlaying)
                {
                    a.Value[i].IsMute = switchAudio;
                }
            }
        }

        switchAudio = !switchAudio;

        PlayerPrefs.SetInt(audioKey,System.Convert.ToInt32(switchAudio));
    }
    //关掉BGM
    public void MuteBGM()
    {

        foreach (var a in bgmList)
        {
            if (a.Value.IsPlaying)
            {
                a.Value.IsMute = switchBGM;
            }
        }
        switchBGM = !switchBGM;

        PlayerPrefs.SetInt(bgmKey, System.Convert.ToInt32(switchBGM));
    }

    //缓存
    void Pool(params int[] arrayId)
    {
        for(int i=0;i< arrayId.Length;i++)
        {
            int soundId = arrayId[i];
            string path = SoundConfig.singleton.m_SoundConfigInfo[soundId].path;

            GameObject o = Resources.Load<GameObject>(path);
            SoundController sTmp = o.GetComponent<SoundController>();

            for(int j=0;j<sTmp.poolNum;j++)
            {
                GameObject sound = Instantiate(o);
                sound.transform.position = Vector3.zero;
                sound.transform.SetParent(soundNode);
                SoundController s = sound.GetComponent<SoundController>();
                s.Init();
                s.id = arrayId[i];
                s.isLoop = sTmp.isLoop;

                if (s.isBGM)
                {
                    bgmList[s.id] = s;
                }
                else
                {
                    if (audios.ContainsKey(s.id))
                    {
                        audios[s.id].Add(s);
                    }
                    else
                    {
                        audios[s.id] = new List<SoundController>() { s };
                    }
                }
            }

        }
    }

}
