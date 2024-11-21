using UnityEngine;

public class OpenVideo : MonoBehaviour
{
    

    // Method to open the URL
    public void OpenVideoURL(string videoURL)
    {
        if (!string.IsNullOrEmpty(videoURL))
        {
            Application.OpenURL(videoURL);
        }
        else
        {
            Debug.LogWarning("Video URL is not set.");
        }
    }
}
