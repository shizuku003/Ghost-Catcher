using UnityEngine;

/// <summary>
/// マップ情報非表示化用スクリプト
/// </summary>
public class MapVisualCanceller : MonoBehaviour
{
    [SerializeField]
    private Material hideMaterial;// マップ情報非表示用マテリアル

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform mesh in transform)
        {
            mesh.gameObject.GetComponent<MeshRenderer>().material = hideMaterial;
        }
    }
}
