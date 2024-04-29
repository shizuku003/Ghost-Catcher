using System.Collections;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Raycast : MonoBehaviour
{
    [SerializeField]
    private GameObject cubePref;
    private Transform controllerTransform;
    private bool prefabCreated = false;
    private bool triggerPulled = false;
    private GameObject cubeObject;
    private MLInput.Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = MLInput.GetController(MLInput.Hand.Left);
    }

    // Update is called once per frame
    void Update()
    {
        if ((controller.TriggerValue > 0.8f) && !triggerPulled)
        {
            controllerTransform.rotation = controller.Orientation;
            MLRaycast.QueryParams raycastParams = new MLRaycast.QueryParams
            {
                Position = controller.Position,
                Direction = controllerTransform.forward,
                UpVector = controllerTransform.up,

                Width = 1,
                Height = 1
            };

            MLRaycast.Raycast(raycastParams, HandleOnReceiveRaycast);
            triggerPulled = true;
        }

        if ((controller.TriggerValue < 0.2f))
        {
            triggerPulled = false;
        }
    }

    private void NormalMarker(Vector3 point, Vector3 normal)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, normal);
        if (!prefabCreated)
        {
            cubeObject = Instantiate(cubePref, point, rotation);
            prefabCreated = true;
        }
        cubeObject.transform.position = point;
        cubeObject.transform.rotation = rotation;
    }

    void HandleOnReceiveRaycast(MLRaycast.ResultState state,Vector3 point,Vector3 normal,float confidence)
    {
        if (state == MLRaycast.ResultState.HitObserved)
        {
            NormalMarker(point, normal);
        }
    }
}
