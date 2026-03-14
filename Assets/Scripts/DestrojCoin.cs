using UnityEngine;

public class DestroyCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Train"))
        {
            string coinName = gameObject.name;
            Debug.Log("Niszczę coin: " + coinName);

            Destroy(gameObject);
        }
    }
}