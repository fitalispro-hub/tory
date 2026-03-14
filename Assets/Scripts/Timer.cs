using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{

    public TrainSpawnerAfterTimer trainSpawner;
    [SerializeField] public TextMeshProUGUI timerText;
    public GameObject progressBar; 
    public TrainMovement timerTrain;
    private bool hasStartedMoving;

    public float targetTime = 60.0f;
    public float maxScaleX = 12f; // The X scale of the bar when fully grown
    
    private float totalTime;
    private float leftAnchorX; 
    private float baseSpriteWidth;

    private void Start()
    {
        totalTime = targetTime;
        
        // 1. Get the actual physical width of the sprite image itself
        baseSpriteWidth = progressBar.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        
        // 2. Find exactly where the left edge is right now in your scene, 
        // regardless of what the scale currently is in the Inspector.
        float currentActualWidth = baseSpriteWidth * progressBar.transform.localScale.x;
        leftAnchorX = progressBar.transform.localPosition.x - (currentActualWidth / 2f);
    }

    public void SomeFunction()
    {
        trainSpawner.SpawnTrain();
    }


    private void Update()
    {
        if (targetTime > 0.0f)
        {
            targetTime -= Time.deltaTime;
            if (targetTime < 0) targetTime = 0; // Prevent going below zero

            timerText.text = string.Format("{0:N2}", targetTime);

            // 1. Calculate progress from 0.0 (start) to 1.0 (end)
            float progress = 1.0f - (targetTime / totalTime);

            // 2. Calculate the scale multiplier AND the actual physical distance it takes up
            float currentScale = progress * maxScaleX;
            float actualWidth = baseSpriteWidth * currentScale;

            // 3. Scale the bar
            progressBar.transform.localScale = new Vector3(currentScale, 0.5f, 1f);

            // 4. Shift the center of the bar to the right so the left edge never moves
            float barCenter = leftAnchorX + (actualWidth / 2f);
            progressBar.transform.localPosition = new Vector3(barCenter, progressBar.transform.localPosition.y, progressBar.transform.localPosition.z);

            // 5. Pin the train exactly to the right edge
            float trainX = leftAnchorX + actualWidth;
            timerTrain.transform.localPosition = new Vector3(trainX, timerTrain.transform.localPosition.y, timerTrain.transform.localPosition.z);
        }
        else
        {
            if (!hasStartedMoving)
            {
                hasStartedMoving = true;
                Debug.Log("Timer finished, calling StartMoving on train");
                SomeFunction();
            }

            timerText.text = "TRAIN IS COMING!!!";
            
            // Lock everything to the final full-width position
            float finalWidth = baseSpriteWidth * maxScaleX;
            progressBar.transform.localScale = new Vector3(maxScaleX, 0.5f, 1f);
            progressBar.transform.localPosition = new Vector3(leftAnchorX + (finalWidth / 2f), progressBar.transform.localPosition.y, progressBar.transform.localPosition.z);
            timerTrain.transform.localPosition = new Vector3(leftAnchorX + finalWidth, timerTrain.transform.localPosition.y, timerTrain.transform.localPosition.z);
        }
    }
}