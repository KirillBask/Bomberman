using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }
    private Vector2 _direction = Vector2.down;
    public float Speed = 5f;

    public KeyCode InputUp = KeyCode.W;
    public KeyCode InputDown = KeyCode.S;
    public KeyCode InputLeft = KeyCode.A;
    public KeyCode InputRight = KeyCode.D;

    public AnimatedSpriteRenderer SpriteRendererUp;
    public AnimatedSpriteRenderer SpriteRendererDown;
    public AnimatedSpriteRenderer SpriteRendererLeft;
    public AnimatedSpriteRenderer SpriteRendererRight;
    public AnimatedSpriteRenderer SpriteRendererDeath;
    private AnimatedSpriteRenderer _activeSpriteRenderer;




    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        _activeSpriteRenderer = SpriteRendererDown;
    }

    private void Update()
    {
        if (Input.GetKey(InputUp))
        {
            SetDirection(Vector2.up, SpriteRendererUp);
        }
        else if (Input.GetKey(InputDown))
        {
            SetDirection(Vector2.down, SpriteRendererDown);
        }
        else if (Input.GetKey(InputLeft))
        {
            SetDirection(Vector2.left, SpriteRendererLeft);
        }
        else if (Input.GetKey(InputRight))
        {
            SetDirection(Vector2.right, SpriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero, _activeSpriteRenderer);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = Rigidbody.position;
        Vector2 translation = _direction * Speed * Time.fixedDeltaTime; 

        Rigidbody.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        _direction = newDirection;

        SpriteRendererUp.enabled = spriteRenderer == SpriteRendererUp;
        SpriteRendererDown.enabled = spriteRenderer == SpriteRendererDown;
        SpriteRendererLeft.enabled = spriteRenderer == SpriteRendererLeft;
        SpriteRendererRight.enabled = spriteRenderer == SpriteRendererRight;

        _activeSpriteRenderer = spriteRenderer;
        _activeSpriteRenderer.Idle = _direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        SpriteRendererUp.enabled = false;
        SpriteRendererDown.enabled = false;
        SpriteRendererLeft.enabled = false;
        SpriteRendererRight.enabled = false;
        SpriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }
}
