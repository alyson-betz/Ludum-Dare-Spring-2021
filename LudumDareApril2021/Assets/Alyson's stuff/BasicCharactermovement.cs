using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharactermovement : MonoBehaviour
{

    public float speed = 2f;

  
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
