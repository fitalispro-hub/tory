using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public float speed = 0f;
    public float destroyX = 15f;

    private bool moving;

    void Update()
    {
        Debug.Log("Train moving, position: " + transform.position.x);
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x > destroyX)
        {
            Destroy(gameObject);
        }
    }

    public void StartMoving()
    {
        speed = 5f;
    }

    public void StopMoving()
    {
        speed = 0f;
    }
}