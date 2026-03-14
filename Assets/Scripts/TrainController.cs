using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TrainController : MonoBehaviour
{
    [Header("Ruch")]
    public float speed = 3f;
    public string nextSceneName = "NextScene";

    [Header("Wykrywanie toru")]
    public LayerMask trackLayer;        // Layer na którym są kafelki toru
    public float rayLength = 0.6f;      // Jak daleko sprawdza przed sobą
    public float rayOffset = 0.3f;      // Jak daleko w bok sprawdza

    [Header("Kierunek startowy")]
    public Vector2 startDirection = Vector2.right;

    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private bool finished = false;
    private int lapPassCount = 0;       // liczy przejazdy przez meta

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //co to 
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        moveDirection = startDirection.normalized;
        UpdateVisualRotation();
    }

    void FixedUpdate()
    {
        if (finished) return;
        DetectAndSteer();
        rb.linearVelocity = moveDirection * speed;
    }

    void DetectAndSteer()
    {
        Vector2 pos = transform.position;

        // Wektory boczne
        Vector2 left  = new Vector2(-moveDirection.y,  moveDirection.x);
        Vector2 right = new Vector2( moveDirection.y, -moveDirection.x);

        bool frontClear = CheckRay(pos, moveDirection, rayLength);
        bool leftClear  = CheckRay(pos, left,          rayOffset);
        bool rightClear = CheckRay(pos, right,         rayOffset);

        if (frontClear)
        {
            // Jedź prosto — nie zmieniaj kierunku
            return;
        }
        else if (leftClear)
        {
            moveDirection = left;
            UpdateVisualRotation();
        }
        else if (rightClear)
        {
            moveDirection = right;
            UpdateVisualRotation();
        }
        else
        {
            // Brak toru w żadną stronę — zatrzymaj
            rb.linearVelocity = Vector2.zero;
            Debug.Log("Brak toru! Zatrzymuję pociąg.");
        }
    }

    bool CheckRay(Vector2 origin, Vector2 direction, float length)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, length, trackLayer);
        Debug.DrawRay(origin, direction * length, hit ? Color.green : Color.red);
        return hit.collider != null;
    }

    // Wywoływane przez StartFinishTrigger
    public void OnPassedStart()
    {
        if (finished) return;
        lapPassCount++;
        Debug.Log($"Przejazd przez start #{lapPassCount}");

        if (lapPassCount >= 2)
        {
            finished = true;
            rb.linearVelocity = Vector2.zero;
            Invoke(nameof(LoadNextScene), 0.5f); // mała pauza dla efektu
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    void UpdateVisualRotation()
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}