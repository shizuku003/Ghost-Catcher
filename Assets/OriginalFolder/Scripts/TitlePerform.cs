using UnityEngine;

/// <summary>
/// �^�C�g���V�[�����o�p�X�N���v�g
/// </summary>
public class TitlePerform : MonoBehaviour
{
    [SerializeField]
    private Transform CamPos;// �v���C���[�J�����ʒu
    [SerializeField]
    private float rotateSpeed = 0.1f;// �S�[�X�g��]���x

    // Update is called once per frame
    void Update()
    {
        transform.position = CamPos.position;
        transform.Rotate(new Vector3(0, rotateSpeed, 0));
    }
}
