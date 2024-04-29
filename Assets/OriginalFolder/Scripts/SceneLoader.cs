using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.MagicLeap;

/// <summary>
/// �V�[���J�ڗp�X�N���v�g
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private MapVisualCanceller mapVisualizer;
    [SerializeField]
    private string NextSceneName;// �J�ڐ�V�[����

    // Start is called before the first frame update
    void Start()
    {
        MLInput.OnControllerButtonDown += OnButtonDown;
    }

    private void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
    }

    /// <summary>
    /// �{�^�������������\�b�h
    /// </summary>
    void OnButtonDown(byte controllerId, MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.Bumper)// �o���p�[�{�^�������������ꍇ
        {
            if (SceneManager.GetActiveScene().name == "MappingScene")
            {
                mapVisualizer.enabled = true;
            }
            SceneManager.LoadScene(NextSceneName);
        }
    }
}
