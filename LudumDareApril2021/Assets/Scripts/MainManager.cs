using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    public GameObject player;
    public TileManager tileManager;

    void Start()
    {
    }

    public Vector2 GetStartPos()
    {
        return tileManager.GetStartPlayerPosition();
    }
}
