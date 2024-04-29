using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MLButtonTest : MonoBehaviour
{
    [SerializeField]
    MapCheck mapCheck;
    [SerializeField]
    private GameObject cube;
    private Vector3 originalPosition;
    private Vector3 rotation = new Vector3(0f, 0f, 0f);
    private const float rotationSpeed = 30f;
    private MLInput.Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = cube.transform.localPosition;

        MLInput.OnControllerButtonDown += OnButtonDown;
        MLInput.OnControllerButtonUp += OnButtonUp;
        controller = MLInput.GetController(MLInput.Hand.Left);

        rotation.x = 180f;
        rotation.y = 180f;
    }

    private void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
        MLInput.OnControllerButtonUp -= OnButtonUp;
    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.Rotate(rotation, rotationSpeed * Time.deltaTime);
        CheckTrigger();
    }

    void OnButtonDown(byte controllerId,MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.Bumper)
        {
            cube.transform.parent = null;
            cube.GetComponent<Rigidbody>().AddForce(transform.forward * 80f);
        }
    }

    void OnButtonUp(byte controllerId,MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.Bumper)
        {
            rotation.y = 0f;
        }
        if (button == MLInput.Controller.Button.HomeTap)
        {
            cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
            cube.transform.localPosition = originalPosition;
            cube.transform.parent = this.transform;
        }
    }

    void CheckTrigger()
    {
        if (controller.TriggerValue > 0.2f)
        {
            rotation.x = 90f;
            Debug.Log("trigger");
            mapCheck.PreMapCheck();
        }
    }
}
