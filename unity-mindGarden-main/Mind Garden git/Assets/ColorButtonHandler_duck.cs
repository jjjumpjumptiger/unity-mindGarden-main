using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorButtonHandler_duck : MonoBehaviour
{
    public Sprite imageForPeace_complex;
    public Sprite imageForEnergetic_complex;
    public Sprite imageForDark_complex;
    public Sprite imageForPeace_middle;
    public Sprite imageForEnergetic_middle;
    public Sprite imageForDark_middle;
    public Sprite imageForPeace_easy;
    public Sprite imageForEnergetic_easy;
    public Sprite imageForDark_easy;

    public void OnPeaceClicked()
    {
        Debug.Log("fjasgfkskhfkjs");
        Debug.Log(GameManager_Duck.HighestLevelPassed);
        if (GameManager_Duck.HighestLevelPassed == 3) // Assuming level 2 is the middle difficulty
        {
            ImageUpdater_duck.SelectedImage = imageForPeace_complex;
        }
        if (GameManager_Duck.HighestLevelPassed == 2) // Assuming all three levels passed
        {
            ImageUpdater_duck.SelectedImage = imageForPeace_middle;
        }
        if (GameManager_Duck.HighestLevelPassed == 1) // Assuming all three levels passed
        {
            ImageUpdater_duck.SelectedImage = imageForPeace_easy;
        }

        SceneManager.LoadScene("SampleScene");

    }

    public void OnEngergeticClicked()
    {
        //ImageUpdater_duck.Instance.UpdatePanelImage(imageForEnergetic_complex);
        ImageUpdater_duck.SelectedImage = imageForEnergetic_complex;
        SceneManager.LoadScene("SampleScene");
    }

    public void OnDarkClicked()
    {
        //ImageUpdater_duck.Instance.UpdatePanelImage(imageForDark_complex);
        ImageUpdater_duck.SelectedImage = imageForDark_complex;
        SceneManager.LoadScene("SampleScene");
    }
}
