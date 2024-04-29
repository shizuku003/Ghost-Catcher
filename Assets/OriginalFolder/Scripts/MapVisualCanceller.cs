using UnityEngine;

/// <summary>
/// �}�b�v����\�����p�X�N���v�g
/// </summary>
public class MapVisualCanceller : MonoBehaviour
{
    [SerializeField]
    private Material hideMaterial;// �}�b�v����\���p�}�e���A��

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform mesh in transform)
        {
            mesh.gameObject.GetComponent<MeshRenderer>().material = hideMaterial;
        }
    }
}
