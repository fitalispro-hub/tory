using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour
{
    public float speed = 30f;
    public string sceneToLoad = "MainMenu";

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > 700)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}