using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----- 전역 싱글톤 클래스 -----
//        씬 전환 시 유지
//          재사용 가능
public class SingletonGlobal<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;
    static GameObject singleton_obj;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
                if (_instance == null)
                {                    
                    SingletonGlobal<T> sub_instance = _instance as SingletonGlobal<T>;
                    sub_instance.Init_settings();
                }
                else singleton_obj = _instance.gameObject;

                DontDestroyOnLoad(singleton_obj);
            }
            return _instance;
        }
    }

    // 변수 초기화
    protected virtual void Init_settings()
    {
        singleton_obj = new GameObject();
        singleton_obj.name = "(Singleton) " + typeof(T).ToString();
        _instance = singleton_obj.AddComponent<T>();
    }
}