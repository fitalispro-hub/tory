using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectTrackEnd : MonoBehaviour
{
    public GameObject train;
    public float rotationSpeed = 50000000000f; // Increased for better feel

    // Turn control variables
    private bool isTurning = false;
    private Vector3 currentRotation = new Vector3(0, 0, 0);
    private Quaternion targetRotation;

    public string sceneName = "GameOver";

    private int coveredTracksAmount = 1;

    private bool start = false;

private void OnTriggerEnter2D(Collider2D other)
{
    if (!start)
    {
        coveredTracksAmount--;
        start = true;
    }
    if (other.CompareTag("Track_Straight"))
    {
        coveredTracksAmount++;
        // Teleport pociągu na pozycję pierwszego dziecka tracka, jeśli istnieje
        if (other.transform.childCount > 0)
        {
            train.transform.position = other.transform.GetChild(0).position;
        }
    }
    
    if (other.CompareTag("Track_Left"))
    {
        // Instantly rotate the train by adding 90 degrees to its current rotation
        train.transform.Rotate(0, 0, 90);
        train.transform.position += train.transform.right * 0.5f;
        coveredTracksAmount++;
        // Teleport pociągu na pozycję pierwszego dziecka tracka, jeśli istnieje
        if (other.transform.childCount > 0)
        {
            train.transform.position = other.transform.GetChild(0).position;
        }
    }
    if (other.CompareTag("Track_Right"))
    {
        train.transform.Rotate(currentRotation - new Vector3(0, 0, 90));
        train.transform.position += train.transform.up * -0.5f;
        coveredTracksAmount++;
        // Teleport pociągu na pozycję pierwszego dziecka tracka, jeśli istnieje
        if (other.transform.childCount > 0)
        {
            train.transform.position = other.transform.GetChild(0).position;
        }
    }
}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Track_Straight"))
        {
            coveredTracksAmount--;
        }
        if (other.CompareTag("Track_Left"))
        {
            coveredTracksAmount--;
        }
        if (other.CompareTag("Track_Right"))
        {
            coveredTracksAmount--;
        }
    }


    public void LoadScene()
            {
                SceneManager.LoadScene(sceneName);
            }
    void Update()
    {
        // 1. Handle Turning
        if (isTurning)
        {
            train.transform.rotation = Quaternion.RotateTowards(
                train.transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );

            // Stop turning once we reach the target
            if (train.transform.rotation == targetRotation)
            {
                isTurning = false;
            }
        }
        
        
        // 2. Handle Crash Detection
        if (coveredTracksAmount <= 0)
        {
            // Optional: Add a delay here so it doesn't log every frame
            Debug.Log("Cho cho crash :(");
            // timerTrain?.StopMoving();
            LoadScene();
    }
}}