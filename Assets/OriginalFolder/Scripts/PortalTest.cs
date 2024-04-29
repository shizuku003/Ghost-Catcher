using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTest : MonoBehaviour
{
    [SerializeField]
    private GameObject portal;

    private bool firstFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Touch");
        if (firstFlag)
        {
            Instantiate(portal, this.transform.position, Quaternion.identity);
            firstFlag = false;
        }
    }
}
