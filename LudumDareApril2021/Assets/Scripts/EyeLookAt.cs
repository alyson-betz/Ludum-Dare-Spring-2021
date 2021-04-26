using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLookAt : MonoBehaviour
{
    public float eyeRadius;
    public float maxRadiusX;
    public float maxRadiusY;
    public bool switchTags;
    public string tag1;
    public string tag2;

    private Player player;
    private string tag;
    private Vector3 centerPosition;
    private Transform lookAt;

    void Start()
    {
        tag = tag1;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        centerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        faceMouse();
    }

    private void faceMouse()
    {
        if (switchTags && player.HasBaby())
        {
            tag = tag2;
        }

        lookAt = GameObject.FindWithTag(tag).transform;

        Vector3 lookDir = (lookAt.position - centerPosition).normalized;
        transform.position = new Vector2(Mathf.Clamp(centerPosition.x + (lookDir.x * eyeRadius), centerPosition.x - (maxRadiusX * transform.lossyScale.x), centerPosition.x + (maxRadiusX * transform.lossyScale.x)),
            Mathf.Clamp(centerPosition.y + (lookDir.y * eyeRadius), centerPosition.y - (maxRadiusY * transform.lossyScale.y), centerPosition.y + (maxRadiusY * transform.lossyScale.y)));
    }
}
