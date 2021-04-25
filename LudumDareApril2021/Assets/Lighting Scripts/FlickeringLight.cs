using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    float baseIntensity;//maybe change to range? 
    bool isFlickering;
    Light lightSource;

    public GameObject lightObject;
    public float maxReduction;
    public float maxIncrease;
    public float rateDamping;
    public float strength;
    public bool stopFlickering;


    public void Reset()
    {
        maxReduction = 0.2f;
        maxIncrease = 0.2f;
        rateDamping = 0.1f;
        strength = 5;
    }
    // Start is called before the first frame update
    void Start()
    {
        lightSource = lightObject.GetComponent<Light>();
        baseIntensity = lightSource.range;
        StartCoroutine(DoFlicker());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DoFlicker()
    {
        isFlickering = true;
        while (!stopFlickering)
        {
            lightSource.range = Mathf.Lerp(lightSource.range, Random.Range(baseIntensity - maxReduction, baseIntensity + maxIncrease),
                strength * Time.deltaTime);
            yield return new WaitForSeconds(rateDamping);
        }
        isFlickering = false;
    }

}
