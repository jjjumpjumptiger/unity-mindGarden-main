using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUpdater_duck : MonoBehaviour
{
    public static ImageUpdater_duck Instance;
    public static Sprite SelectedImage { get; set; }
    public Image panelImage;
    public GameObject colorImage_duck;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        HideImage(); // Initially hide the image
        //colorImage_duck.SetActive(false);
    }
    void Start()
    {
        if (panelImage != null && SelectedImage != null)
        {
            //colorImage_duck.SetActive(true);
            panelImage.sprite = SelectedImage;
            ShowImage();
        }
    }

    public void HideImage()
    {
        if (panelImage != null) panelImage.enabled = false; // Hide the Image component
    }

    public void ShowImage()
    {
        if (panelImage != null) panelImage.enabled = true; // Show the Image component
    }

    //public void SetPanelImage(Image newPanelImage)
    //{
    //    panelImage = newPanelImage;
    //}

    //public void UpdatePanelImage(Sprite newImage)
    //{
    //    if (panelImage != null)
    //    {
    //        //colorImage_duck.SetActive(true);
    //        panelImage.sprite = newImage;
    //    }
    //    else
    //    {
    //        Debug.LogError("Panel image component not assigned.");
    //    }
    //}
}
