using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public AudioSource click;
    public GameObject spotlight;
    float charge = 100f;
    public float maxCharge = 100f;
    public float deltaCharge = 10f;
    public float maxRange = 15f;
    public float threshold = 20f;
    public bool isOn = true;
    bool firstPress = true;
    public Battery battery;
    
    // Start is called before the first frame update
    void Start()
    {
        charge = maxCharge;
        threshold = maxCharge - maxCharge / 5;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Flashlight") && firstPress)
        {
            firstPress = false;

            if (!isOn)
            {
                spotlight.SetActive(true);
                click.Play();
                isOn = true;
            }
            else
            {
                spotlight.SetActive(false);
                click.Play();
                isOn = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            battery.RestoreBattery();
            charge = maxCharge;
            threshold = maxCharge - maxCharge / 5;
        }

        if (isOn && charge > 0)
        {
            charge -= deltaCharge * Time.deltaTime;
            spotlight.GetComponent<Light>().range = maxRange * charge/ maxCharge;

            if (charge <= threshold)
            {
                battery.DecreaseBattery();
                threshold -= maxCharge / 5;
            }
        }


        if (Input.GetButtonUp("Flashlight"))
        {
            firstPress = true;
        }


    }
}
