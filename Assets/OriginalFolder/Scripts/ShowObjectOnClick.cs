using UnityEngine;

/// <summary>
/// �N���b�N�ɂ��I�u�W�F�N�g�\���p�X�N���v�g
/// </summary>
public class ShowObjectOnClick : MonoBehaviour
{
    [SerializeField]
    private GameObject showObject;// �\������I�u�W�F�N�g

    private bool clickedFlag;// �N���b�N�t���O

    /// <summary>
    /// �N���b�N������
    /// </summary>
    public void OnClick()
    {
        if (!clickedFlag)
        {
            showObject.SetActive(true);
            clickedFlag = true;
        }
        else
        {
            showObject.SetActive(false);
            clickedFlag = false;
        }
    }
}
