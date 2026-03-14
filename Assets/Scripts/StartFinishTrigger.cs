using UnityEngine;

public class StartFinishTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        TrainMovement train = other.GetComponent<TrainMovement>();
        if (train != null)
            train.StartMoving();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.84f, 0f, 0.5f);
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        Vector3 size = col != null ? (Vector3)col.size : Vector3.one * 0.5f;
        Gizmos.DrawCube(transform.position, size);
    }
}
