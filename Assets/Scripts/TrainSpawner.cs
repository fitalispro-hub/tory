using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    public GameObject trainPrefab;
    public Transform spawnPoint;

    private GameObject currentTrain;

    void Update()
    {
        if (currentTrain == null)
        {
            currentTrain = Instantiate(trainPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}