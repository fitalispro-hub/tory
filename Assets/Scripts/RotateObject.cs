using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObject : MonoBehaviour
{
    bool inRange = false;
    float change = 90f;
    private Transform currentObject;

    public void OnInteract(InputValue value)
    {
        if (value.isPressed && inRange && currentObject != null)
        {
            currentObject.Rotate(0f, 0f, change);
            Debug.Log("Obrócono obiekt: " + currentObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            inRange = true;
            currentObject = other.transform;
            Debug.Log("W zasiêgu: " + currentObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            inRange = false;
            currentObject = null;
            Debug.Log("Opuszczono zasiêg obiektu");
        }
    }
}