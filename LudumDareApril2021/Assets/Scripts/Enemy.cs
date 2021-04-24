using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float alertRadius;
    public float moveSpeed;

    private Animator animation;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        animation = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= alertRadius)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            changeDirection(newPosition - transform.position);
            transform.position = newPosition;
        }
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

}
