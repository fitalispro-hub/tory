using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void GoBack()
    {
        string previousScene = PlayerPrefs.GetString("PreviousScene");
        SceneManager.LoadScene(previousScene);
    }
}