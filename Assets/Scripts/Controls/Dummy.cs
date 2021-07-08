using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    private Rigidbody dummyRb;

    // Start is called before the first frame update
    void Start()
    {
        dummyRb = GetComponent<Rigidbody>();
        dummyRb.sleepThreshold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
