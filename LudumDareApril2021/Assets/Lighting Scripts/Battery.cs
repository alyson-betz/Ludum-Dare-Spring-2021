using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{

    public Sprite FIVE_BARS;
    public Sprite FOUR_BARS;
    public Sprite THREE_BARS;
    public Sprite TWO_BARS;
    public Sprite ONE_BAR;
    public Sprite NO_BARS;
    int currentBars = 5;

    private void Start()
    {
        GetComponent<Image>().sprite = FIVE_BARS;
    }

    public int DecreaseBattery()
    {
        if (currentBars == 0)
            return 0;

        currentBars--;

        switch(currentBars)
        {
            case 4:
                GetComponent<Image>().sprite = FOUR_BARS;
                break;
            case 3:
                GetComponent<Image>().sprite = THREE_BARS;
                break;
            case 2:
                GetComponent<Image>().sprite = TWO_BARS;
                break;
            case 1:
                GetComponent<Image>().sprite = ONE_BAR;
                break;
            case 0:
                GetComponent<Image>().sprite = NO_BARS;
                break;
        }
        return currentBars;
    }

    public int RestoreBattery()
    {
        currentBars = 5;
        GetComponent<Image>().sprite = FIVE_BARS;
        return currentBars;
    }



}
