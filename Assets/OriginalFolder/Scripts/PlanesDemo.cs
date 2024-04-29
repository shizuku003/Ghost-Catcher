using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class PlanesDemo : MonoBehaviour
{
    [SerializeField]
    private Transform BBoxTransform;
    [SerializeField]
    private Vector3 BBoxExtents;
    [SerializeField]
    private GameObject planeGameObject;

    private MLPlanes.QueryParams queryParams = new MLPlanes.QueryParams();
    [SerializeField]
    private MLPlanes.QueryFlags queryFlags;

    private static float timeout = 5f;
    private float timeSinceLastRequest = 0f;
    private List<GameObject> planeCache = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        MLPlanes.Start();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastRequest += Time.deltaTime;
        if (timeSinceLastRequest > timeout)
        {
            timeSinceLastRequest = 0f;
            RequestPlanes();
        }
    }

    private void RequestPlanes()
    {
        queryParams.Flags = queryFlags;
        queryParams.MaxResults = 100;
        queryParams.BoundsCenter = BBoxTransform.position;
        queryParams.BoundsRotation = BBoxTransform.rotation;
        queryParams.BoundsExtents = BBoxExtents;

        MLPlanes.GetPlanes(queryParams, HandleOnReceivedPlanes);
    }

    private void HandleOnReceivedPlanes(MLResult result,MLPlanes.Plane[] planes, MLPlanes.Boundaries[] boundaries)
    {
        for(int i = planeCache.Count - 1; i >= 0; --i)
        {
            Destroy(planeCache[i]);
            planeCache.Remove(planeCache[i]);
        }

        GameObject newPlane;
        for(int i = 0; i < planes.Length; ++i)
        {
            newPlane = Instantiate(planeGameObject);
            newPlane.transform.position = planes[i].Center;
            newPlane.transform.rotation = planes[i].Rotation;
            newPlane.transform.localScale = new Vector3(planes[i].Width, planes[i].Height, 1f);
            planeCache.Add(newPlane);
        }
    }
}
