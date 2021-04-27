using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
  

    // Update is called once per frame
    void Update()
    {
        // Vector3 lookTarget = Vector3.zero;
        // var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit;
        // if (Physics.Raycast(ray, out hit))
        // {
        //     lookTarget = hit.point;
        // }

        // Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector2 worldPoint2d = new Vector3(worldPoint.x, 0, worldPoint.y);        // transform.LookAt(lookTarget);
        // transform.LookAt(lookTarget);

        var mousePos = Input.mousePosition;
        mousePos.z = 10; 
        // Debug.Log(Camera.main.ScreenToWorldPoint(mousePos));
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = -(mouseScreenPosition - (Vector2)transform.position).normalized;

        transform.up = direction;
    }
}
