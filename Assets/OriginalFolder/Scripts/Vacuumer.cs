using UnityEngine;
using UnityEngine.XR.MagicLeap;

/// <summary>
/// �z����p�X�N���v�g
/// </summary>
public class Vacuumer : MonoBehaviour
{
    [SerializeField]
    MapCheck mapCheck;
    private MLInput.Controller controller;// Magic Leap 1�R���g���[���[

    private GameObject caughtGhost;// �z�������S�[�X�g
    private float distance = 0f;// �S�[�X�g�Ƌz����̋���
    private GameManager gameManager;

    [SerializeField]
    private GhostGenerator ghostGenerator;
    [SerializeField]
    private string ghostTag = "Ghost";// �S�[�X�g�I�u�W�F�N�g�̃^�O��

    [SerializeField]
    private Battery battery;
    private bool batteryLevelFlag;// �o�b�e���[�c�ʃt���O

    [SerializeField]
    private Transform lineStartPos;// �z�������\���p���C���n�_
    [SerializeField]
    private LineRenderer lineRenderer;// �z�������\���p���C��

    [SerializeField]
    private GameObject vacuumedParticle;// �z�����p�[�e�B�N���I�u�W�F�N�g

    [SerializeField]
    private AudioSource audioSource;// �����o�͗p
    [SerializeField]
    private AudioClip vacuumingSound;// �z��������
    [SerializeField]
    private AudioClip vacuumedSound;// �z���㉹��
    private bool vacuumingFlag;// �z�����t���O

    [SerializeField]
    private GameObject trapPrefab;// 㩃v���t�@�u
    [SerializeField]
    private Transform trapPos;// 㩃I�u�W�F�N�g�ʒu
    private GameObject trap;// 㩃I�u�W�F�N�g
    private bool trapFlag;// 㩐����t���O

    void Start()
    {
        MLInput.OnControllerButtonDown += OnButtonDown;
        controller = MLInput.GetController(MLInput.Hand.Left);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, lineStartPos.position);// �z�������\���p���C���n�_�ݒ�
        lineRenderer.SetPosition(1, lineStartPos.forward * 100);// �z�������\���p���C���I�_�ݒ�
        CheckTrigger();
    }

    /// <summary>
    /// �{�^���������������\�b�h
    /// </summary>
    void OnButtonDown(byte controllerId, MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.HomeTap)// �z�[���{�^���������i㩐������j��j
        {
            if (trapFlag)
            {
                trapFlag = false;
                DestroyObject(trap);
            }
            else
            {
                trapFlag = true;
                trap = Instantiate(trapPrefab, trapPos);
                trap.transform.parent = trapPos;
                trap.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

        }
        if (button == MLInput.Controller.Button.Bumper)// �o���p�[�{�^���������i㩎ˏo�j
        {
            if (trapFlag)
            {
                trap.transform.parent = null;
                trap.GetComponent<Rigidbody>().AddForce(trapPos.forward * 80f);
            }
        }
    }

    /// <summary>
    /// �g���K�[�������������\�b�h�i�z�������j
    /// </summary>
    void CheckTrigger()
    {
        if (controller.TriggerValue > 0.3f)// �g���K�[����������
        {
            Ray ray = new Ray(lineStartPos.position, lineStartPos.forward);
            RaycastHit hit;

            batteryLevelFlag = battery.BatteryUse();

            if (Physics.Raycast(ray, out hit) && batteryLevelFlag)
            {
                vacuumedParticle.SetActive(true);
                if (!vacuumingFlag)
                {
                    audioSource.PlayOneShot(vacuumingSound);
                    vacuumingFlag = true;
                }

                if (hit.collider.gameObject.tag == ghostTag)// �o�L���[���Ώۂ��S�[�X�g�̎�
                {
                    caughtGhost = hit.collider.gameObject;
                    caughtGhost.GetComponent<Ghost>().Vacuumed(hit.collider.transform.position, lineStartPos.position);

                    distance = Vector3.Distance(lineStartPos.position, hit.collider.transform.position);
                    if (distance < 0.5f)// �S�[�X�g�Ƌz����̋�������苗���ȉ��ɂȂ����Ƃ�
                    {
                        gameManager.GetPoint();
                        caughtGhost.GetComponent<Ghost>().DestroyGhost();
                        audioSource.PlayOneShot(vacuumedSound);
                        battery.BatteryCharge();
                        caughtGhost = null;
                    }
                }
            }

            if (!batteryLevelFlag)
            {
                vacuumedParticle.SetActive(false);
                audioSource.Stop();
                vacuumingFlag = false;

                if (caughtGhost != null)
                {
                    caughtGhost.GetComponent<Ghost>().DestroyGhost();
                }
            }
        }
        else
        {
            battery.BatteryAutoCharge();
            vacuumedParticle.SetActive(false);
            audioSource.Stop();
            vacuumingFlag = false;

            if (distance >= 0.5f)
            {
                if (caughtGhost != null)
                {
                    caughtGhost.GetComponent<Ghost>().DestroyGhost();
                    caughtGhost = null;
                }
            }
        }
    }
}
