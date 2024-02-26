using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorButtonHandler_panda : MonoBehaviour
{
    public Sprite imageForPeace_complex_panda;
    public Sprite imageForEnergetic_complex_panda;
    public Sprite imageForDark_complex_panda;
    public Sprite imageForPeace_middle_panda;
    public Sprite imageForEnergetic_middle_panda;
    public Sprite imageForDark_middle_panda;
    public Sprite imageForPeace_easy_panda;
    public Sprite imageForEnergetic_easy_panda;
    public Sprite imageForDark_easy_panda;

    public void OnPeaceClicked()
    {
        //Debug.Log($"ImageUpdater_duck Instance: {ImageUpdater_duck.Instance}");
        ImageUpdater_panda.SelectedImage = imageForPeace_complex_panda;
        SceneManager.LoadScene("SampleScene");

    }

    public void OnEngergeticClicked()
    {
        //ImageUpdater_duck.Instance.UpdatePanelImage(imageForEnergetic_complex);
        ImageUpdater_panda.SelectedImage = imageForEnergetic_complex_panda;
        SceneManager.LoadScene("SampleScene");
    }

    public void OnDarkClicked()
    {
        //ImageUpdater_duck.Instance.UpdatePanelImage(imageForDark_complex);
        ImageUpdater_panda.SelectedImage = imageForDark_complex_panda;
        SceneManager.LoadScene("SampleScene");
    }
}
