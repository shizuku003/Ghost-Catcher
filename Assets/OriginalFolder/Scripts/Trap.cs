using UnityEngine;

/// <summary>
/// 㩏����p�X�N���v�g
/// </summary>
public class Trap : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer zoneMesh;// 㩔���\�����b�V��
    [SerializeField]
    private GameObject cube;// 㩃I�u�W�F�N�g
    private float rotationSpeed = 1f;// 㩉��o�p��]���x
    private bool putFlag;// 㩐ݒu�t���O

    [SerializeField]
    private string ghostTag = "Ghost";// �S�[�X�g�I�u�W�F�N�g�����^�O��

    private GameObject caughtGhost;// �z���ΏۃS�[�X�g

    [SerializeField]
    private float destroyTimer = 10f;// 㩃I�u�W�F�N�g�j��܂ł̃^�C�}�[
    private int catchNum;// �z���ς݃S�[�X�g��

    private GameManager gameManager;

    [SerializeField]
    private AudioSource audioSource;// �����o�͗p
    [SerializeField]
    private AudioClip vacuumedSound;// �z���㉹��
    [SerializeField]
    private AudioClip destroySound;// 㩔j�󎞉���

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
    /// �ڐG����i�z�������j
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
        else// 㩂��ݒu���ꂽ�Ƃ�
        {
            if (other.gameObject.tag == ghostTag)// �ڐG�Ώۂ��S�[�X�g�̎�
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
