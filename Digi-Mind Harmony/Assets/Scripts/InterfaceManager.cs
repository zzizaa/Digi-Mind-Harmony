using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    //Variables:
    public TextMeshProUGUI title;
    public GameObject[] menus;
    public GameObject[] menuOnePages;
    public Sprite[] sprites;
    public GameObject imageCanvas;
    private Image image;

    private void Start()
    {
        image = imageCanvas.GetComponent<Image>();
    }

    //Change Title
    public void ChangeText(string text)
    {
       title.text = text;
    }

    //Change Menu
    public void ActivateMenu(int i)
    {
        foreach (var menu in menus)
        {
           menu.SetActive(false);
        }
        menus[i].SetActive(true);
    }

    //Change Pages inside Menu
    public void ActivatePages(int i)
    {
        foreach(var page in menuOnePages)
        {
            page.SetActive(false);
        }
        menuOnePages[i].SetActive(true);
    }

    //Set Image when open
    public void SetupImage(int i)
    {
        image.sprite = sprites[i];
    }
}
