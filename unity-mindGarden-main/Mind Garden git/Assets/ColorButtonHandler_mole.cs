using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorButtonHandler_mole : MonoBehaviour
{
    public Sprite imageForPeace_complex_mole;
    public Sprite imageForEnergetic_complex_mole;
    public Sprite imageForDark_complex_mole;
    public Sprite imageForPeace_middle_mole;
    public Sprite imageForEnergetic_middle_mole;
    public Sprite imageForDark_middle_mole;
    public Sprite imageForPeace_easy_mole;
    public Sprite imageForEnergetic_easy_mole;
    public Sprite imageForDark_easy_mole;

    public void OnPeaceClicked()
    {
        //Debug.Log($"ImageUpdater_duck Instance: {ImageUpdater_duck.Instance}");
        ImageUpdater_mole.SelectedImage = imageForPeace_complex_mole;
        SceneManager.LoadScene("SampleScene");

    }

    public void OnEngergeticClicked()
    {
        //ImageUpdater_duck.Instance.UpdatePanelImage(imageForEnergetic_complex);
        ImageUpdater_mole.SelectedImage = imageForEnergetic_complex_mole;
        SceneManager.LoadScene("SampleScene");
    }

    public void OnDarkClicked()
    {
        //ImageUpdater_duck.Instance.UpdatePanelImage(imageForDark_complex);
        ImageUpdater_mole.SelectedImage = imageForDark_complex_mole;
        SceneManager.LoadScene("SampleScene");
    }
}
