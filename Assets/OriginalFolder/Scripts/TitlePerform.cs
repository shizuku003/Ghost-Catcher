using UnityEngine;

/// <summary>
/// タイトルシーン演出用スクリプト
/// </summary>
public class TitlePerform : MonoBehaviour
{
    [SerializeField]
    private Transform CamPos;// プレイヤーカメラ位置
    [SerializeField]
    private float rotateSpeed = 0.1f;// ゴースト回転速度

    // Update is called once per frame
    void Update()
    {
        transform.position = CamPos.position;
        transform.Rotate(new Vector3(0, rotateSpeed, 0));
    }
}
