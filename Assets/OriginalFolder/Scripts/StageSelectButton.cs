using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �X�e�[�W�Z���N�g�{�^���p�X�N���v�g
/// </summary>
public class StageSelectButton : MonoBehaviour
{
    [SerializeField]
    private string sceneName;// �J�ڐ�V�[����

    public void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
