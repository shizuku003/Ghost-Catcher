using UnityEngine;

/// <summary>
/// マップ情報保存用スクリプト（シングルトン）
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
