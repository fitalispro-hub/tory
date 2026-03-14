using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObject : MonoBehaviour
{
    bool inRange = false;
    float change = 90f;
    private Transform currentObject;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
    }

    private void OnDestroy()
    {
        inputActions.Player.Disable();
        inputActions.Dispose();
    }

    public void OnInteract(InputValue value)
    {
        if (inputActions.Player.Interact.WasPressedThisFrame() && inRange && currentObject != null)
        {
            currentObject.Rotate(0f, 0f, change);
            Debug.Log("Obr�cono obiekt: " + currentObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Track_Straight") || other.CompareTag("Track_Left") || other.CompareTag("Track_Right"))
        {
            inRange = true;
            currentObject = other.transform;
            Debug.Log("W zasi�gu: " + currentObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Track_Straight") || other.CompareTag("Track_Left") || other.CompareTag("Track_Right"))
        {
            inRange = false;
            currentObject = null;
            Debug.Log("Opuszczono zasi�g obiektu");
        }
    }
}