using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public float speed = 5f;
    public float destroyX = 15f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x > destroyX)
        {
            Destroy(gameObject);
        }
    }
}