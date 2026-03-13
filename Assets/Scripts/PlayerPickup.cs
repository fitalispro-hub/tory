using NUnit.Framework;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{

    [Header("Player Settings")]
    public GameObject player;

    public GameObject target;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return;
        if (collision.gameObject != player) return;

        hasTriggered = true;
        target.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;

        hasTriggered = false;
        target.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0, 255);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hasTriggered);
    }
}
