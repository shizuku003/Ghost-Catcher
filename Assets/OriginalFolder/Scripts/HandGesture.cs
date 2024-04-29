using UnityEngine;
using UnityEngine.XR.MagicLeap;
using static UnityEngine.XR.MagicLeap.MLHandTracking;

public class HandGesture : MonoBehaviour
{
    private bool OKHandPose = false;
    private float speed = 30f;
    private float distance = 2f;
    [SerializeField]
    private GameObject cube;
    private MLHandTracking.HandKeyPose[] gestures = new MLHandTracking.HandKeyPose[4];

    // Start is called before the first frame update
    void Start()
    {
        MLHandTracking.Start();

        gestures[0] = MLHandTracking.HandKeyPose.Ok;
        gestures[1] = MLHandTracking.HandKeyPose.Fist;
        gestures[2] = MLHandTracking.HandKeyPose.OpenHand;
        gestures[3] = MLHandTracking.HandKeyPose.Finger;
        KeyPoseManager.EnabledKeyPose(gestures, true, false);

        cube.SetActive(false);
    }

    private void OnDestroy()
    {
        MLHandTracking.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (OKHandPose)
        {
            if (GetGesture(Left, HandKeyPose.OpenHand) || GetGesture(Right, HandKeyPose.OpenHand))
                cube.transform.Rotate(Vector3.up, + speed * Time.deltaTime);

            if (GetGesture(Left, HandKeyPose.Fist) || GetGesture(Right, HandKeyPose.Fist))
                cube.transform.Rotate(Vector3.up, - speed * Time.deltaTime);

            if (GetGesture(Left, HandKeyPose.Finger))
                cube.transform.Rotate(Vector3.right, + speed * Time.deltaTime);

            if (GetGesture(Right, HandKeyPose.Finger))
                cube.transform.Rotate(Vector3.left, + speed * Time.deltaTime);
        }
        else
        {
            if (GetGesture(Left, HandKeyPose.Ok) || GetGesture(Right, HandKeyPose.Ok))
            {
                OKHandPose = true;
                cube.SetActive(true);
                cube.transform.position = transform.position + transform.forward * distance;
                cube.transform.rotation = transform.rotation;
            }

        }
    }

    private bool GetGesture(Hand hand, HandKeyPose type)
    {
        if (hand != null)
        {
            if (hand.KeyPose == type)
            {
                if (hand.HandKeyPoseConfidence > 0.9f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
