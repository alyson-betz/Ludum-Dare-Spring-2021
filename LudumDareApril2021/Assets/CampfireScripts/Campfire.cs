using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public GameObject Player;
    public Flashlight FlashLight; // We need to assign the Flashlight gameobject
    public float TriggerDistance;

    private bool IsNearby;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector3.Distance(this.transform.position, Player.transform.position);

        if (Distance <= TriggerDistance)
        {
            // Debug.Log("FlashLight function handle in-range");
            // Call the FlashLight function here.
            FlashLight.RestoreBattery();
            return;
        }
    }
}
