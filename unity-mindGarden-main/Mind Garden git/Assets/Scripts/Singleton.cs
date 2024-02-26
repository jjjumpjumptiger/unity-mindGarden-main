using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Start is called before the first frame update
    private static T instance;
    public static T Instance{
        get{
            if(instance ==null){
                //尝试从场景中找到已经存在的实例
                instance = FindObjectOfType<T>();

                //如果场景中不存在该实例，则创建一个新的实例
                if(instance == null){
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }
    //实例被销毁时调用
    protected virtual void OnDestroy(){
        if(instance == this){
            instance = null;
        }
    }
}
