using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

    [SerializeField]
    private float playerMovementSpeed;

    private bool isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        HanldlePlayerMovement(horizontal, vertical);
        FlipPlayer(horizontal);
    }

    private void HanldlePlayerMovement(float horizontal, float vertical)
    {
        myRigidbody2D.velocity = new Vector2(horizontal * playerMovementSpeed, vertical * playerMovementSpeed);
    }

    private void FlipPlayer(float horizontal)
    {
        if(horizontal >0 && !isFacingRight || horizontal<0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
        
    }
}
