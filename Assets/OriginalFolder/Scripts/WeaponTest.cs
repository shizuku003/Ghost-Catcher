using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : MonoBehaviour
{
    private GameObject caughtGhost;
    private float distance = 0f;
    private GameManager gameManager;

    [SerializeField]
    private GhostGenerator ghostGenerator;
    [SerializeField]
    private string ghostTag = "Ghost";

    [SerializeField]
    private Battery battery;
    private bool batteryLevelFlag;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Ray ray = new Ray(transform.position, Vector3.right);
            RaycastHit hit;
            batteryLevelFlag = battery.BatteryUse();

            if (Physics.Raycast(ray, out hit) && batteryLevelFlag)
            {
                if (hit.collider.gameObject.tag == ghostTag)
                {
                    caughtGhost = hit.collider.gameObject;
                    caughtGhost.GetComponent<Ghost>().Vacuumed(hit.collider.transform.position, transform.position);

                    distance = Vector3.Distance(transform.position, hit.collider.transform.position);
                    if (distance < 0.5f)
                    {
                        gameManager.GetPoint();
                        caughtGhost.GetComponent<Ghost>().DestroyGhost();
                        //ghostGenerator.GenerateGhost();
                        battery.BatteryCharge();
                        caughtGhost = null;
                    }
                }
            }

            if (!batteryLevelFlag && caughtGhost != null)
            {
                caughtGhost.GetComponent<Ghost>().DestroyGhost();
            }
        }
        else{
            battery.BatteryAutoCharge();

            if (distance >= 0.5f)
            {
                if (Input.GetKeyUp(KeyCode.Space) && caughtGhost != null)
                {
                    caughtGhost.GetComponent<Ghost>().DestroyGhost();
                    //ghostGenerator.GenerateGhost();
                    caughtGhost = null;
                }
            }
        }
    }
}
