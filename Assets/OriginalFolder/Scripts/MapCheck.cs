using UnityEngine;

/// <summary>
/// マップ情報確認用スクリプト
/// </summary>
public class MapCheck : MonoBehaviour
{
    [SerializeField]
    public float[] mapInfo = new float[6];// 簡易マップ情報

    private Vector3 castPoint;// 各マップ座標値処理用

    private int process = 0;// 手順確認用

    [SerializeField]
    private GhostGenerator ghostGenerator;

    [SerializeField]
    private GameObject mapCheckObj;// 簡易マップ取得確認用フラグ

    /// <summary>
    /// マップ情報送信メソッド
    /// </summary>
    /// <returns>マップ情報（float）</returns>
    public float[] MapInfoSend()
    {
        return mapInfo;
    }

    /// <summary>
    /// マップ情報確認前段階処理メソッド
    /// </summary>
    public void PreMapCheck()
    {
        castPoint = -transform.right;
        SimpleMapping(castPoint);
        castPoint = transform.right;
        SimpleMapping(castPoint);
        castPoint = -transform.up;
        SimpleMapping(castPoint);
        castPoint = transform.up;
        SimpleMapping(castPoint);
        castPoint = -transform.forward;
        SimpleMapping(castPoint);
        castPoint = transform.forward;
        SimpleMapping(castPoint);
    }

    /// <summary>
    /// マッピング処理メソッド
    /// </summary>
    /// <param name="castPoint">マッピング処理対象座標値</param>
    private void SimpleMapping(Vector3 castPoint)
    {
        Ray ray = new Ray(transform.position, castPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (process < 2)
            {
                mapInfo[process] = hit.point.x;
            }
            else if (process < 4)
            {
                mapInfo[process] = hit.point.y;
            }
            else if (process < 6)
            {
                mapInfo[process] = hit.point.z;
            }

            Instantiate(mapCheckObj, hit.point, Quaternion.identity);// マッピング処理確認用オブジェクトの配置
            process++;
        }

        if (process == 6)
        {
            ghostGenerator.InstantiateGhost();
        }
    }
}
