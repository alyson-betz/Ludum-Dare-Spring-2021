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

    Vector2 movement;

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
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            HanldlePlayerMovement(movement);
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
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        HanldlePlayerMovement(movement);
    }

    private void HanldlePlayerMovement(Vector2 movement)
    {
        myRigidbody2D.velocity = new Vector2(movement.x * playerMovementSpeed, movement.y * playerMovementSpeed);
        myAnimator.SetFloat("Horizontal", movement.x);
        myAnimator.SetFloat("Vertical", movement.y);
        myAnimator.SetFloat("Speed", movement.magnitude);
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
