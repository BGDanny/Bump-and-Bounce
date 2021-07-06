using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
