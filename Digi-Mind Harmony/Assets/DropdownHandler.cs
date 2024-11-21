using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public GameObject[] contents;
    public string[] contentsNames;

    private void Start()
    {
        // Add a listener for when the dropdown value changes
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDestroy()
    {
        // Remove the listener to prevent memory leaks
        dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
    }

   private void OnDropdownValueChanged(int index)
    {
        // Get the selected option
        string selectedOption = dropdown.options[index].text;

        // Call your custom method based on the selected option
        Debug.Log($"Selected Option: {selectedOption}");
        ActivateContent(selectedOption);
    }

    private void ActivateContent(string option)
    {
        foreach (GameObject content in contents) {
            content.SetActive(false);
        }
        
        for (int i = 0; i < contentsNames.Length; i++) {
            if (contentsNames[i] == option) { 
                contents[i].SetActive(true);      
            }
        }

    }
}

