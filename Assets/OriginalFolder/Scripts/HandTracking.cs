using UnityEngine;
using UnityEngine.XR.MagicLeap;
using static UnityEngine.XR.MagicLeap.MLHandTracking;

public class HandTracking : MonoBehaviour
{
    [SerializeField]
    private enum HandPoses { Ok, Finger, Thumb, OpenHand, Fist,NoPose};
    [SerializeField]
    private HandPoses pose = HandPoses.NoPose;
    [SerializeField]
    private GameObject sphereThumb, sphereIndex, sphereMiddle, sphereRing, spherePinky, sphereWrist;

    private HandKeyPose[] gestures;

    // Start is called before the first frame update
    void Start()
    {
        MLHandTracking.Start();
        gestures = new HandKeyPose[5];
        gestures[0] = HandKeyPose.Ok;
        gestures[1] = HandKeyPose.Finger;
        gestures[2] = HandKeyPose.OpenHand;
        gestures[3] = HandKeyPose.Fist;
        gestures[4] = HandKeyPose.Thumb;
        KeyPoseManager.EnabledKeyPose(gestures, true, false);
    }

    private void OnDestroy()
    {
        MLHandTracking.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetGesture(Left, HandKeyPose.Ok))
        {
            pose = HandPoses.Ok;
        }
        else if (GetGesture(Left, HandKeyPose.Finger))
        {
            pose = HandPoses.Finger;
        }
        else if (GetGesture(Left, HandKeyPose.OpenHand))
        {
            pose = HandPoses.OpenHand;
        }
        else if (GetGesture(Left, HandKeyPose.Fist))
        {
            pose = HandPoses.Fist;
        }
        else if (GetGesture(Left, HandKeyPose.Thumb))
        {
            pose = HandPoses.Thumb;
        }
        else
        {
            pose = HandPoses.NoPose;
        }

        if (pose != HandPoses.NoPose)
            ShowPoints();
    }

    private void ShowPoints()
    {
        sphereThumb.transform.position = Left.Thumb.KeyPoints[2].Position;
        sphereIndex.transform.position = Left.Index.KeyPoints[2].Position;
        sphereMiddle.transform.position = Left.Middle.KeyPoints[2].Position;
        sphereRing.transform.position = Left.Ring.KeyPoints[1].Position;
        spherePinky.transform.position = Left.Pinky.KeyPoints[1].Position;
        sphereWrist.transform.position = Left.Wrist.KeyPoints[0].Position;
    }

    private bool GetGesture(Hand hand, HandKeyPose type)
    {
        if (hand != null)
        {
            if(hand.KeyPose== type)
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
