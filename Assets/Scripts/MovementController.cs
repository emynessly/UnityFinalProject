using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speedMove = 6f;
    [SerializeField] private float speedJump = 16f;
    private AudioSource soundJump;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private ContactFilter2D ContactFilter;
    private bool grounded => rb.IsTouching(ContactFilter);
    public bool flipX => spriteRenderer.flipX;

    void Start()    
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ContactFilter.SetNormalAngle(45.0f, 135.0f);
        ContactFilter.useNormalAngle = true;
        soundJump = GetComponents<AudioSource>()[0];
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        // перемещение
        rb.linearVelocity = new Vector2(moveX * speedMove, rb.linearVelocity.y);

        animator.SetFloat("Speed", Mathf.Abs(moveX));

        // поворот персонажа
        if (moveX > 0.1f)
            spriteRenderer.flipX = false;
        else if (moveX < -0.1f)
            spriteRenderer.flipX = true;

        // прыжок
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * speedJump, ForceMode2D.Impulse);
            soundJump.Play();
        }

        // смена прыжка на падение
        if (rb.linearVelocityY > 0 & !grounded)
        {
            GetComponent<Animator>().SetBool("Jump", true);
        }
        else if (rb.linearVelocityY < 0 & !grounded)
        {
            GetComponent<Animator>().SetBool("Fall", true);
        }
        else if (grounded)
        {
            GetComponent<Animator>().SetBool("Fall", false);
            GetComponent<Animator>().SetBool("Jump", false);
        }
    }
}