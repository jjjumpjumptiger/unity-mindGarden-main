using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    public void ShowCircle()
    {
        //获取物体的父物体
        Transform parentTransform = transform.parent;
        if(parentTransform != null){
            //获取父物体下的所有物体（包括自身）
            Transform[] children = parentTransform.GetComponentsInChildren<Transform>(true);
            //输出每个子物体的名称
            foreach(Transform child in children){
                if(child.gameObject.GetComponent<SpriteRenderer>() != null &&
                   child.gameObject.GetComponent<Collider2D>() != null){
                    //进行显示处理
                    child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    //移除掉Collider组件
                    child.gameObject.GetComponent<Collider2D>().enabled = false;
                }
            }
        }
        
    }
}
