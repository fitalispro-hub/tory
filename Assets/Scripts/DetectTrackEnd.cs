using UnityEngine;

public class DetectTrackEnd : MonoBehaviour
{
    public GameObject train;

    private bool trainCrash = false;

    private int coveredTracksAmount = 0;

    // This method is called when the train's collider starts touching another collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the "Track" tag
        if (other.CompareTag("Track_Straight"))
        {
            Debug.Log("Train entered straight track: " + other.gameObject.name);
            coveredTracksAmount++;
            Debug.Log(coveredTracksAmount);
            // Add your track collision logic here
            // For example: stop the train, trigger events, etc.
        }
        if (other.CompareTag("Track_Left"))
        {
            
        }
    }

    // This method is called when the train's collider stops touching another collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Track_Straight"))
        {
            Debug.Log("Train exited straight track: " + other.gameObject.name);
            coveredTracksAmount++;
            Debug.Log(coveredTracksAmount);
            // Add your track exit logic here
        }
    }

    // Optional: This method is called every frame while colliding
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.CompareTag("Track_Straight"))
    //     {
    //         // This runs continuously while touching the track
    //         // Useful for things like "train is on track" checks
    //     }
    // }

    void Start()
    {

    }

    void Update()
    {
        if (coveredTracksAmount <= 0)
        {
            Debug.Log("Cho cho crash :(");
        }
    }
}
