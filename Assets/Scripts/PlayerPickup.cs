using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    [Header("Player Settings")]
    public GameObject player;

    private bool canInteract = false;
    private GameObject currentItem = null; // Item player is currently near
    private GameObject heldItem = null; // Item player is currently holding

    private InputSystem_Actions inputActions;

    void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Dispose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player) return;

        currentItem = collision.gameObject;
        canInteract = true;

        Debug.Log("Near item: " + currentItem.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player) return;

        if (collision.gameObject == currentItem)
        {
            canInteract = false;
            currentItem = null;
            Debug.Log("Left item area");
        }
    }

    void Update()
    {
        if(heldItem!=null) {
            canInteract = true;
        }
        if (canInteract && inputActions.Player.Pickup.WasPressedThisFrame())
        {
            if (heldItem == null)
            {
                // Not holding anything - pickup the current item
                PickupItem(currentItem);
            }
            else
            {
                // Holding something - drop it
                DropItem();
            }
        }
    }

    private void PickupItem(GameObject item)
    {
        Debug.Log("PICKED UP: " + item.name);
        
        // Make item invisible instead of destroying it
        SpriteRenderer sprite = item.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
        }
        
        heldItem = item;
        Debug.Log("Now holding: " + heldItem.name);
    }

    private void DropItem()
    {
        Debug.Log("Dropping item");
        if (heldItem == null) return;
        Debug.Log("Held item =/= null");
        Debug.Log("DROPPED: " + heldItem.name);
        
        // Make item visible again and position it
        heldItem.transform.position = transform.position;
        SpriteRenderer sprite = heldItem.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        }
        
        heldItem = null;
        Debug.Log("No longer holding anything");
    }
}
