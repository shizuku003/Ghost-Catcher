using UnityEngine;
using UnityEngine.XR.MagicLeap;

/// <summary>
/// �R���g���[���[�ʒu�����p�X�N���v�g
/// </summary>
public class ControllerTest : MonoBehaviour
{
    private MLInput.Controller controller;// Magic Leap 1�R���g���[���[

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
