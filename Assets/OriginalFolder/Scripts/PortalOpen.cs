using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOpen : MonoBehaviour
{
    [SerializeField]
    private GameObject portal;
    private GameObject portalCache;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Main Camera").transform;
        Vector3 difference = transform.position - player.position;
        Debug.Log(difference.normalized);
        portalCache = Instantiate(portal, transform.position, Quaternion.identity);
        portalCache.transform.LookAt(player.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
