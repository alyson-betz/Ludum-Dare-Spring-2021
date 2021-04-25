using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject gameManager;
    public Transform targetTransform;
    public float alertRadius;
    public float moveSpeed;

    public float randRange;
    public float timeToRandMove;

    public AudioClip walking;

    public BoxCollider2D myHorizontalBoxColl;
    public BoxCollider2D myVerticalBoxColl;

    private Animator animation;
    private Rigidbody2D myRigidbody;

    private Vector3 randPosition;
    private float randMoveTimer;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = GameObject.FindWithTag("Player").transform;
        animation = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        randPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        gameManager = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = CheckDistance();

        if (!isMoving)
        {
            MoveToRandomSpot();
        }
    }

    bool CheckDistance()
    {
        if (Vector3.Distance(targetTransform.position, transform.position) <= alertRadius)
        {
            // Play sound of bunny chasing you (depends on bunny size)
            SoundManager.Sound walkingSound;
            SoundManager soundManager = gameManager.GetComponent<SoundManager>();
            if (transform.localScale.x > 1.1f && transform.localScale.y > 1.1f)
            {
                walkingSound = SoundManager.Sound.BunnyWalkingSlow;
            }
            else if (transform.localScale.x < 0.7f && transform.localScale.y < 0.7f)
            {
                walkingSound = SoundManager.Sound.BunnyWalkingFast;
            }
            else
            {
                walkingSound = SoundManager.Sound.BunnyWalkingMedium;
            }
            soundManager.PlaySoundOneShot3D(walkingSound, transform.position);

            // Move to position
            MoveToPosition(targetTransform.position);
            
            return true;
        }
        return false;
    }

    private void setAnimationDirection(Vector3 aDirection)
    {
        animation.SetFloat("moveX", aDirection.x);
        animation.SetFloat("moveY", aDirection.y);
    }

    private void changeDirection(Vector3 aDirection)
    {
        if (Mathf.Abs(aDirection.x) > Mathf.Abs(aDirection.y))
        {
            myHorizontalBoxColl.enabled = true;
            myVerticalBoxColl.enabled = false;
            if (aDirection.x > 0)
            {
                setAnimationDirection(Vector2.right);
            }
            else if (aDirection.x < 0)
            {
                setAnimationDirection(Vector2.left);
            }
        }
        else if (Mathf.Abs(aDirection.x) < Mathf.Abs(aDirection.y))
        {
            myHorizontalBoxColl.enabled = false;
            myVerticalBoxColl.enabled = true;
            if (aDirection.y > 0)
            {
                setAnimationDirection(Vector2.up);
            }
            else if (aDirection.y < 0)
            {
                setAnimationDirection(Vector2.down);
            }
        }
    }

    private void MoveToRandomSpot()
    {
        randMoveTimer += Time.deltaTime;

        float randX = Random.Range(-randRange, randRange);
        float randY = Random.Range(-randRange, randRange);
        float randZ = Random.Range(-randRange, randRange);

        if (randMoveTimer >= timeToRandMove)
        {
            Vector3 myPosition = transform.position;
            randPosition = new Vector3(myPosition.x + randX, myPosition.y + randY, myPosition.z + randZ);

            randMoveTimer = 0;
            timeToRandMove = Random.Range(0, 10);
        }

        MoveToPosition(randPosition);
    }

    private void MoveToPosition(Vector3 newPosition)
    {
        Vector3 position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        changeDirection(position - transform.position);
        transform.position = position;
    }
}
