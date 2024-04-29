using UnityEngine;
using UnityEngine.XR.MagicLeap;

/// <summary>
/// コントローラー位置同期用スクリプト
/// </summary>
public class ControllerTest : MonoBehaviour
{
    private MLInput.Controller controller;// Magic Leap 1コントローラー

    void Start()
    {
        controller = MLInput.GetController(MLInput.Hand.Left);
    }


    void Update()
    {
        transform.position = controller.Position;
        transform.rotation = controller.Orientation;
    }
}
