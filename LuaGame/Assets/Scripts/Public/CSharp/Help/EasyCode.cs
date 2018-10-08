using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class EasyCode : MonoBehaviour {

    /// <summary>
    /// 延迟执行
    /// </summary>
    /// <param name="action"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    public static IEnumerator DelayInvoke(System.Action action,float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    /// <summary>
    /// 射线
    /// </summary>
    /// <param name="start"></param>
    /// <param name="director"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    public static RaycastHit Ray3D(Vector3 start, Vector3 director, float line)
    {
        RaycastHit hit;
        Physics.Raycast(start, director, out hit, line);
        return hit;
    }
    public static RaycastHit2D Ray2D(Vector2 start, Vector2 director, float line)
    {
        return Physics2D.Raycast(start, director, line);
    }
    /// <summary>
    /// t1朝着target
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="target"></param>
    /// <param name="lerp"></param>
    /// <param name="ladder">增量</param>
    public static void LookAtPos(Transform t1,Transform target,float lerp = 0,float ladder = 10)
    {
        Vector3 dir = t1.position - target.position;
        dir.z = 0;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + lerp;
        target.rotation = Quaternion.Slerp(target.rotation, Quaternion.Euler(0, 0, angle), ladder * Time.deltaTime);
    }

    public static Vector3 GetVector_CameraMain(Vector3 targetPos)
    {
        if (Camera.main == null) return Vector3.zero;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetPos);
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
        Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
        return newPos;
    }
    /// <summary>
    /// 在范围内选出n个不同对象
    /// </summary>
    /// <param name="list"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static List<int> RandomCount(List<int> list, int n)
    {
        if (list.Count <= n) return null;

        List<int> newList = new List<int>();

        int m = UnityEngine.Random.Range(0, list.Count);
        newList.Add(list[m]);



        while (newList.Count < n)
        {
            int a = UnityEngine.Random.Range(0, list.Count);
            int num = list[a];
            if (!newList.Contains(num))
                newList.Add(num);
        }
        return newList;
    }
    /// <summary>
    /// 分割字符串
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="str"></param>
    /// <param name="c">字符</param>
    /// <returns></returns>
    public static List<T> ParseString<T>(string str,char c)
    {
        List<T> list = new List<T>();

        string[] strs = str.Split(c);

        foreach(string s in strs)
        {
            if(typeof(T) == typeof(int))
            {
                object obj = (object)int.Parse(s);
                list.Add((T)obj);
            }
            if (typeof(T) == typeof(string))
            {
                object obj = (object)s;
                list.Add((T)obj);
            }
        }
        return list;
    }
    public static int[] GetInts(string str,char c)
    {
        int[] arrInt = System.Array.ConvertAll<string, int>(str.Split(c), s => int.Parse(s));
        return arrInt;
    }
    /// <summary>
    /// 震动
    /// </summary>
    public static void DeviceShake()
    {
#if UNITY_EDITOR
#else
        //Handheld.Vibrate();
#endif
    }


    /// <summary>
    /// 读取Resources文件夹下所有文件
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="path">文件夹路径</param>
    /// <returns></returns>
    public static List<T> LoadAllResource<T>(string path) where T : UnityEngine.Object
    {
        object[] objs = Resources.LoadAll(path);

        List<T> list = new List<T>();
        for (int i = 0; i < objs.Length; i++)
        {
            try
            {
                T o = objs[i] as T;
                list.Add(o);
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
                throw new System.Exception("类型不对");
            }
        }
        return list;
    }

    //hexColor转color
    public static string ColorToHex(Color color)
    {
        int r = Mathf.RoundToInt(color.r * 255.0f);
        int g = Mathf.RoundToInt(color.g * 255.0f);
        int b = Mathf.RoundToInt(color.b * 255.0f);
        int a = Mathf.RoundToInt(color.a * 255.0f);
        string hex = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
        return hex;
    }
    /// <summary>
    /// hex转换到color
    /// </summary>
    /// <param name="hex">ABCE56AE</param>
    /// <returns></returns>
    public static Color HexToColor(string hex)
    {
        byte br = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte bg = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte bb = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte cc = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        float r = br / 255f;
        float g = bg / 255f;
        float b = bb / 255f;
        float a = cc / 255f;
        return new Color(r, g, b, a);
    }



    /// <summary>
    /// 时间
    /// </summary>
    /// <returns>距离明天多少秒</returns>
    public static int LocalTimeByTomorrow()
    {
        int hour = DateTime.Now.Hour;
        int min = DateTime.Now.Minute;
        int sec = DateTime.Now.Second;

        int flow = hour * 3600 + min * 60 + sec;

        int ready = 24 * 3600 - flow;

        return ready;
    }
    public static string LocalTimeByTomorrowData()
    {
        int sec = LocalTimeByTomorrow();
        int hour = sec / 3600;
        int min = (sec % 3600) / 60;
        int secNow = sec % 60;
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hour, min, secNow);
    }

}


public class TweenClass
{

    public static Tweener TweenAlpha(Image target, float time, float alpha = 0)
    {
        return DOTween.To(() => target.color, r => target.color = r,
               new Color(1, 1, 1, alpha), time).SetEase(Ease.Linear);
    }
    public static Tweener TwenBoard(Transform trans)
    {
        return trans.DOScale(0.8f, 0.1f).OnComplete(() =>
        {
            trans.DOScale(1.5f, 0.15f).OnComplete(() =>
            {
                trans.DOScale(0.9f, 0.1f).OnComplete(() =>
                {
                    trans.DOScale(1f, 0.08f);
                });
            });
        });
    }

    public static Tweener TweenBack(Transform target,float scale,float time)
    {
        return target.DOScale(scale, time).SetEase(Ease.Linear).OnComplete(() =>
        {
            target.DOScale(1f, time).SetEase(Ease.Linear);
        });
    }
    public static Tweener TweenColorBack(Image target,float color,float time)
    {
        return target.DOColor(new Color(color, color, color, 1),time).SetEase(Ease.Linear).OnComplete(()=>
        {
            target.DOColor(new Color(1, 1, 1, 1), time).SetEase(Ease.Linear);
        });
    }
    public static Tweener TweenBedRoom(Transform target)
    {
        return target.DOScale(1.2f, 0.2f).OnComplete(() =>
         {
             target.DOScale(0.8f, 0.1f).OnComplete(()=>
             {
                 target.DOScale(1.1f, 0.1f).OnComplete(()=>
                 {
                     target.DOScale(1, 0.05f);
                 });
             });
         });
    }
    public static Tweener TweenYazi(Transform target)
    {
        return target.DOLocalRotate(new Vector3(0,0, 15), 0.1f).SetEase(Ease.Linear).OnComplete(() =>
           {
               target.DOLocalRotate(new Vector3(0,0, -10), 0.15f).SetEase(Ease.Linear).OnComplete(() =>
               {
                   target.DOLocalRotate(new Vector3(0, 0, 8), 0.1f).SetEase(Ease.Linear).OnComplete(() =>
                   {
                       target.DOLocalRotate(new Vector3(0,0, -6), 0.08f).SetEase(Ease.Linear).OnComplete(() =>
                       {
                           target.DOLocalRotate(Vector3.zero, 0.05f).SetEase(Ease.Linear);
                       });
                   });
               });
           });
    }
    public static Tweener TextNum(Text text,int startNum,int toNum,float time = 1.5f,float delay = 0)
    {
        return DOTween.To(() => startNum, x => startNum = x, toNum,time).SetEase(Ease.Linear).OnUpdate(()=>
        {
            text.text = startNum + "";
        });
    }

    public static void CreateGoldIcon(Vector3 startPos, Vector3 endPos, Transform parent, Sprite iconSpr, float scale = 1)
    {
        GameObject o = new GameObject();
        o.transform.SetParent(parent);
        o.transform.position = startPos;
        o.transform.localScale = Vector3.one * scale;
        int x = UnityEngine.Random.Range(-50, 50);
        int y = UnityEngine.Random.Range(-50, 50);
        Vector3 newV = new Vector3(x, y);
        Image ima = o.AddComponent<Image>();
        ima.sprite = iconSpr;
        ima.SetNativeSize();
        o.transform.DOLocalMove(o.transform.localPosition + newV, 0.15f).OnComplete(() =>
        {
            o.transform.DOMove(endPos, 0.3f).OnComplete(() =>
            {
                GameObject.Destroy(o.gameObject);
                SoundMgr.Instance.PlaySound(5);
            });
        });
    }

}

public class DebugS
{
    public static void Log(object message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#endif
    }
    public static void LogError(object message)
    {
#if UNITY_EDITOR
        Debug.LogError(message);
#endif
    }
}
