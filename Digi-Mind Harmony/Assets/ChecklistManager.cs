using UnityEngine;
using TMPro;
using System;

public class ChecklistManager : MonoBehaviour
{
    public TextMeshProUGUI sportCounterText; // Contatore per la categoria "Sport"
    public TextMeshProUGUI waterCounterText;  // Contatore per la categoria "Water"
    public TextMeshProUGUI studyCounterText; // Contatore per la categoria "Study"

    public GameObject[] sportButtons; // Pulsanti per la categoria "Sport"
    public GameObject[] waterButtons;  // Pulsanti per la categoria "Water"
    public GameObject[] studyButtons; // Pulsanti per la categoria "Study"

    private GameObject[] checklistButtons; // Unico array per tutti i pulsanti
    private int[] buttonStates; // Stato dei pulsanti

    private int sportCounter;
    private int waterCounter;
    private int studyCounter;

    private void Start()
    {
        // Carica i contatori persistenti
        sportCounter = PlayerPrefs.GetInt("SportCounter", 0);
        waterCounter = PlayerPrefs.GetInt("WaterCounter", 0);
        studyCounter = PlayerPrefs.GetInt("StudyCounter", 0);

        // Combinare tutti i bottoni in un unico array
        checklistButtons = new GameObject[sportButtons.Length + waterButtons.Length + studyButtons.Length];
        sportButtons.CopyTo(checklistButtons, 0);
        waterButtons.CopyTo(checklistButtons, sportButtons.Length);
        studyButtons.CopyTo(checklistButtons, sportButtons.Length + waterButtons.Length);

        buttonStates = new int[checklistButtons.Length];

        // Controlla se è un nuovo giorno e resetta i task
        string lastReset = PlayerPrefs.GetString("LastResetDate", DateTime.MinValue.ToString());
        if (string.IsNullOrEmpty(lastReset) || IsNewDay(DateTime.Parse(lastReset)))
        {
            ResetTasks();
        }
        else
        {
            LoadButtonStates();
        }

        UpdateCounterTexts();
    }

    // Metodi per completare i task delle diverse categorie
    public void CompleteSportTask(GameObject button)
    {
        CompleteTask(button, "Sport");
    }

    public void CompleteWaterTask(GameObject button)
    {
        CompleteTask(button, "Water");
    }

    public void CompleteStudyTask(GameObject button)
    {
        CompleteTask(button, "Study");
    }

    // Metodo per completare un task (disattivare il pulsante e incrementare il contatore)
    public void CompleteTask(GameObject button, string category)
    {
        button.SetActive(false); // Disattiva il bottone quando premuto

        // Trova l'indice del pulsante nell'array
        int index = Array.IndexOf(checklistButtons, button);
        if (index >= 0)
        {
            // Salva lo stato del pulsante come disattivato (0)
            buttonStates[index] = 0;
            PlayerPrefs.SetInt($"ButtonState_{index}", 0);
        }

        // Incrementa il contatore per la categoria specificata
        if (category == "Sport")
        {
            sportCounter++;
            PlayerPrefs.SetInt("SportCounter", sportCounter);
        }
        else if (category == "Water")
        {
            waterCounter++;
            PlayerPrefs.SetInt("WaterCounter", waterCounter);
        }
        else if (category == "Study")
        {
            studyCounter++;
            PlayerPrefs.SetInt("StudyCounter", studyCounter);
        }

        PlayerPrefs.Save(); // Salva i dati
        UpdateCounterTexts();
    }

    // Metodo per aggiornare i testi dei contatori
    private void UpdateCounterTexts()
    {
        sportCounterText.text = sportCounter.ToString();
        waterCounterText.text = waterCounter.ToString();
        studyCounterText.text = studyCounter.ToString();
    }

    // Metodo per resettare le task giornaliere
    public void ResetTasks()
    {
        // Rende visibili tutti i pulsanti
        foreach (GameObject button in checklistButtons)
        {
            button.SetActive(true);
        }

        // Aggiorna la data dell'ultimo reset
        PlayerPrefs.SetString("LastResetDate", DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    // Metodo per caricare lo stato dei pulsanti da PlayerPrefs
    private void LoadButtonStates()
    {
        if (buttonStates == null || checklistButtons == null)
        {
            Debug.LogError("buttonStates o checklistButtons non è configurato correttamente.");
            return;
        }

        for (int i = 0; i < checklistButtons.Length; i++)
        {
            // Carica lo stato del pulsante dai dati salvati
            buttonStates[i] = PlayerPrefs.GetInt($"ButtonState_{i}", 1); // Default: attivo

            // Attiva o disattiva il pulsante in base allo stato
            if (checklistButtons[i] != null)
            {
                checklistButtons[i].SetActive(buttonStates[i] == 1); // Attivo se 1, disattivo se 0
            }
            else
            {
                Debug.LogWarning($"Il pulsante all'indice {i} è null. Controlla la configurazione.");
            }
        }
    }

    // Metodo per verificare se è un nuovo giorno
    private bool IsNewDay(DateTime lastResetDate)
    {
        // Confronta la data dell'ultimo reset con quella di oggi
        return lastResetDate.Date < DateTime.Now.Date;
    }

    public void ResetApp()
    {
        // Reset dei contatori
        sportCounter = 0;
        waterCounter = 0;
        studyCounter = 0;
        PlayerPrefs.SetInt("SportCounter", sportCounter);
        PlayerPrefs.SetInt("WaterCounter", waterCounter);
        PlayerPrefs.SetInt("StudyCounter", studyCounter);

        // Reset delle preferenze per lo stato dei bottoni
        for (int i = 0; i < checklistButtons.Length; i++)
        {
            PlayerPrefs.SetInt($"ButtonState_{i}", 1); // Rende tutti i bottoni attivi
        }

        PlayerPrefs.SetString("LastResetDate", DateTime.Now.ToString()); // Imposta la data dell'ultimo reset
        PlayerPrefs.Save();

        // Rende visibili tutti i bottoni
        foreach (GameObject button in checklistButtons)
        {
            button.SetActive(true);
        }

        // Aggiorna i testi dei contatori
        UpdateCounterTexts();
    }
}


