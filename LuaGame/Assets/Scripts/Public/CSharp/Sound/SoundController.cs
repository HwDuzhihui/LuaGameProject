using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundController : MonoBehaviour {

    AudioSource audio;
    public int poolNum = 1;              //缓存个数
    public int id;                       //id
    public float maxVolum = 1;           //最大音量
    public float tweenTime = 0.5f;       //渐变时间
    public bool isBGM;
    public bool isLoop;
    public Tweener tweenVolume;

    //循环
    public bool Loop
    {
        get { return audio.loop; }
        set { audio.loop = value; }
    }
    //播放中
    public bool IsPlaying
    {
        get { return audio.isPlaying; }
    }
    //静音中
    public bool IsMute
    {
        get { return audio.mute; }
        set { audio.mute = value; }
    }
    //暂停中
    public bool IsPause
    {
        get;
        private set;
    }
    //音量
    public float Volume
    {
        get { return audio.volume; }
        set { audio.volume = value; }
    }

    public void Init()
    {
        if (audio == null)
        {
            audio = GetComponent<AudioSource>();
            maxVolum = audio.volume;
            if (isBGM || isLoop)
                Loop = true;

        }
    }
    //播放(回调事件)
    public void Play(System.Action action = null)
    {
        //没初始化就先初始化
        Init();
        //总开关是不是打开
        if(isBGM)
        {
            IsMute = !SoundMgr.Instance.switchBGM;
        }
        else
        {
            IsMute = !SoundMgr.Instance.switchAudio;
        }
        //正在播就停掉
        if (audio.isPlaying)
        {
            Stop();
        }
        //可以播了
        audio.Play();
        StartCoroutine(EndAction(action));
    }
    IEnumerator EndAction(System.Action action = null)
    {
        while (IsPlaying || IsPause)
            yield return new WaitForSeconds(0.01f);

        if(action != null)
            action();
    }
    //停止
    public void Stop()
    {
        audio.Stop();
    }
    //暂停
    public void Pause()
    {
        IsPause = true;
        audio.Pause();
    }
    //继续
    public void UnPause()
    {
        IsPause = false;
        audio.UnPause();
    }

    //渐变播放
    public void FadeMax()
    {
        if (tweenVolume != null)
            if (tweenVolume.IsPlaying())
                tweenVolume.Kill();
        Tween tween = DOTween.To(() => Volume, r => Volume = r, maxVolum, tweenTime);
    }
    public void FadeMin(float min = 0.2f)
    {
        if (tweenVolume != null)
            if (tweenVolume.IsPlaying())
                tweenVolume.Kill();
        Tween tween = DOTween.To(() => Volume, r => Volume = r, min, tweenTime);
    }
}
