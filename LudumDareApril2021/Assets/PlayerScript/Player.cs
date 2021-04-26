using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

    private Animator myAnimator;

    [SerializeField]
    private float playerMovementSpeed;

    private bool isFacingRight;
    private bool dead = false;
    private bool hasBaby = false;
    private bool won = false;

    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            HanldlePlayerMovement(horizontal, vertical);
            FlipPlayer(horizontal);
        }
    }

    public bool IsDead()
    {
        return dead;
    }

    public bool HasBaby()
    {
        return hasBaby;
    }

    public bool HasWon()
    {
        return won;
    }

    public void ResetPlayer(Vector2 startPos)
    {
        transform.position = startPos;
        dead = false;
        hasBaby = false;
        won = false;
    }

    private void HanldlePlayerMovement(float horizontal, float vertical)
    {
        myRigidbody2D.velocity = new Vector2(horizontal * playerMovementSpeed, vertical * playerMovementSpeed);
        myAnimator.SetFloat("horizontal_speed", Mathf.Abs(horizontal));
        myAnimator.SetFloat("vertical_speed", vertical);
    }

    private void FlipPlayer(float horizontal)
    {
        if (horizontal > 0 && !isFacingRight || horizontal < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            dead = true;
            myRigidbody2D.velocity = Vector2.zero;
        }
        else if (other.gameObject.CompareTag("Baby"))
        {
            hasBaby = true;
            Destroy(other.gameObject);
        }
        else if (hasBaby && other.gameObject.CompareTag("Finish"))
        {
            won = true;
        }
    }
}
