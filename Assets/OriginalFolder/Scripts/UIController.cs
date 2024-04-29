using UnityEngine;
using UnityEngine.XR.MagicLeap;

/// <summary>
/// �X�e�[�W�Z���N�g�V�[���ɂ�����UI�Ǘ��p�X�N���v�g
/// </summary>
public class UIController : MonoBehaviour
{
    private LineRenderer lineRenderer;// �{�^���I��p���C��

    [SerializeField]
    private string stageSelectButtonTag = "StageSelectButton";// �X�e�[�W�Z���N�g�{�^���p�^�O��
    [SerializeField]
    private string gameSettingButtonTag = "GameSettingButton";// �Q�[���ݒ�{�^���p�^�O��
    [SerializeField]
    private string showObjectButtonTag = "ShowObjectButton";// �I�u�W�F�N�g�\���{�^���p�^�O��

    void Start()
    {
        MLInput.OnControllerButtonDown += OnButtonDown;

        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, transform.position);// �{�^���I��p���C���n�_�ݒ�
        lineRenderer.SetPosition(1, transform.forward * 100);// �{�^���I��p���C���I�_�ݒ�
    }

    private void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
    }

    /// <summary>
    /// �{�^���������������\�b�h
    /// </summary>
    void OnButtonDown(byte controllerId, MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.Bumper)// �o���p�[�{�^��������
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == stageSelectButtonTag)
                {
                    hit.transform.gameObject.GetComponent<StageSelectButton>().OnClick();
                }
                if (hit.transform.gameObject.tag == gameSettingButtonTag)
                {
                    hit.transform.gameObject.GetComponent<GameSettingButton>().OnClick();
                }
                if (hit.transform.gameObject.tag == showObjectButtonTag)
                {
                    hit.transform.gameObject.GetComponent<ShowObjectOnClick>().OnClick();
                }
            }
        }
    }
}
