using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MainManager gameManager;

    private Rigidbody2D myRigidbody2D;

    [SerializeField]
    private float playerMovementSpeed;

    private bool isFacingRight;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetPosition();
        isFacingRight = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
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
        
        if(dead && Input.GetKey(KeyCode.Space))
        {
            ResetPosition();
            dead = false;
        }
        
    }

    private void ResetPosition()
    {
        transform.position = gameManager.GetStartPos();
    }

    private void HanldlePlayerMovement(float horizontal, float vertical)
    {
        myRigidbody2D.velocity = new Vector2(horizontal * playerMovementSpeed, vertical * playerMovementSpeed);
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
        if (other.gameObject.CompareTag("Respawn"))
        {
            dead = true;
            myRigidbody2D.velocity = Vector2.zero;
        }
    }
}
