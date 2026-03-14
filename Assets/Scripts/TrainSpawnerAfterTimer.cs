using UnityEngine;

public class TrainSpawnerAfterTimer : MonoBehaviour
{
    public GameObject trainPrefab;
    public Transform spawnPoint;

    public void SpawnTrain()
    {
        Instantiate(trainPrefab, spawnPoint.position, Quaternion.identity);
    }
}