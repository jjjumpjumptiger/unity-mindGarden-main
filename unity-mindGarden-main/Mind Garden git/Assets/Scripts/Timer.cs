using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] public Image uiFill;
    // [SerializeField] private Text uiText;

    public int Duration;
    private int remainingDuration;

    // public int RemainingDuration // Add a property to access remainingDuration
    // {
    //     get { return remainingDuration; }
    //     set { remainingDuration = value; }
    // }
    // Start is called before the first frame update
    private void Start()
    {
        Being(Duration);
    }

    private void Being(int Second){
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }
    // Update is called once per frame
    private IEnumerator UpdateTimer(){
        while(remainingDuration >= 0){
          
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }


    public void ResetTimer(){
        //添加if条件 score - 5 = 0 或者 score - 8 = 0 button才会enabled
        // if(UIController.num == 0){
        //     remainingDuration = Duration;
        // }
     
        if(UIController.num - 3 == 0 || UIController.num - 8 == 0){
            remainingDuration = Duration;
        }
        
    }
    private void OnEnd(){
        print("End");
    }
}
