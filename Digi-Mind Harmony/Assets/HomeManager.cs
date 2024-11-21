using UnityEngine;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private GameObject taskMenu;
    [SerializeField] private GameObject pagesList;
    private bool isPageListActive = false;

    public void ActivateHomeMenu()
    {
        if (isPageListActive) {
            pagesList.SetActive(false);
            taskMenu.SetActive(true);
            isPageListActive = false;
        } else
        {
            taskMenu.SetActive(false);
            pagesList.SetActive(true);
            isPageListActive = true;
        }
    }

    public void ChangingMenu()
    {
        isPageListActive = false;
    }
}
