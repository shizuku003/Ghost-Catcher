using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTest : MonoBehaviour
{
    private int r1;
    private int r2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            r1 = Random.Range(0, 5);
            r2 = Random.Range(0, 5);
            Debug.Log(r1 + "+" + r2);
            while (r1 == r2)
            {
                r2 = Random.Range(0, 5);
                Debug.Log(r1 + "+" + r2);
            }
        }
    }
}
