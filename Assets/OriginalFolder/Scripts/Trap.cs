using UnityEngine;

/// <summary>
/// 罠処理用スクリプト
/// </summary>
public class Trap : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer zoneMesh;// 罠判定表示メッシュ
    [SerializeField]
    private GameObject cube;// 罠オブジェクト
    private float rotationSpeed = 1f;// 罠演出用回転速度
    private bool putFlag;// 罠設置フラグ

    [SerializeField]
    private string ghostTag = "Ghost";// ゴーストオブジェクトが持つタグ名

    private GameObject caughtGhost;// 吸引対象ゴースト

    [SerializeField]
    private float destroyTimer = 10f;// 罠オブジェクト破壊までのタイマー
    private int catchNum;// 吸引済みゴースト数

    private GameManager gameManager;

    [SerializeField]
    private AudioSource audioSource;// 音源出力用
    [SerializeField]
    private AudioClip vacuumedSound;// 吸引後音源
    [SerializeField]
    private AudioClip destroySound;// 罠破壊時音源

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.Rotate(rotationSpeed, rotationSpeed, rotationSpeed);

        if (putFlag)
        {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer < 0f)
            {
                for(int i = 0; i < catchNum; i++)
                {
                    Debug.Log("GetPoint");
                    gameManager.GetPoint();
                }
                audioSource.PlayOneShot(destroySound);
                DestroyObject(gameObject);
            }
        }
    }

    /// <summary>
    /// 接触判定（吸引処理）
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (!putFlag)
        {
            putFlag = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<SphereCollider>().radius = 10f;
            zoneMesh.enabled = true;
        }
        else// 罠が設置されたとき
        {
            if (other.gameObject.tag == ghostTag)// 接触対象がゴーストの時
            {
                caughtGhost = other.gameObject;
                caughtGhost.GetComponent<Ghost>().Vacuumed(other.transform.position, transform.position);
                if(Vector3.Distance(transform.position, other.transform.position) < 0.5f)
                {
                    catchNum++;
                    caughtGhost.GetComponent<Ghost>().DestroyGhost();
                    audioSource.PlayOneShot(vacuumedSound);
                    caughtGhost = null;
                }
            }
        }
    }
}
