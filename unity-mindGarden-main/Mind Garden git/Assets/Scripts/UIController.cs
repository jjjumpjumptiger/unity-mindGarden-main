using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    public TMP_Text score;
    public static int num;
    public List<GameObject> popupList;
  
    public void AddScore()
    {
        num += 1;
        score.text = num.ToString();
        if(num == 3){
            popupList[0].SetActive(true);
        }
        if(num == 8){
            popupList[1].SetActive(true);
        }
        if(num == 16){
            popupList[2].SetActive(true);
        }
    }
    
}
