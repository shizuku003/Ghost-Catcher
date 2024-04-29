using UnityEngine;

/// <summary>
/// �}�b�v���ۑ��p�X�N���v�g�i�V���O���g���j
/// </summary>
public class MapSaving : MonoBehaviour
{
    public static MapSaving instance;

    private void Awake()
    {
        CheckInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
