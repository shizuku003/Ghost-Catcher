using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    [SerializeField]
    private Transform index;
    [SerializeField]
    private Transform wrist;

    // Update is called once per frame
    void Update()
    {
        transform.position = (index.position + wrist.position) / 2;
    }
}
