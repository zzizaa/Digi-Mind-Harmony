using Unity.VisualScripting;
using UnityEngine;

public class PgManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    [SerializeField] private GameObject homeMenu;
    [SerializeField] private GameObject taskMenu;
    [SerializeField] private GameObject pageMenu;

    public void ActivateMenu(int i)
    {
        homeMenu.SetActive(false);
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }

        pages[i].SetActive(true);
    }

    public void GoBackToHome()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        taskMenu.SetActive(true);
        pageMenu.SetActive(false);
        homeMenu.SetActive(true);
    }
}
