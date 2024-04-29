using UnityEngine;

/// <summary>
/// �}�b�v���m�F�p�X�N���v�g
/// </summary>
public class MapCheck : MonoBehaviour
{
    [SerializeField]
    public float[] mapInfo = new float[6];// �ȈՃ}�b�v���

    private Vector3 castPoint;// �e�}�b�v���W�l�����p

    private int process = 0;// �菇�m�F�p

    [SerializeField]
    private GhostGenerator ghostGenerator;

    [SerializeField]
    private GameObject mapCheckObj;// �ȈՃ}�b�v�擾�m�F�p�t���O

    /// <summary>
    /// �}�b�v��񑗐M���\�b�h
    /// </summary>
    /// <returns>�}�b�v���ifloat�j</returns>
    public float[] MapInfoSend()
    {
        return mapInfo;
    }

    /// <summary>
    /// �}�b�v���m�F�O�i�K�������\�b�h
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
    /// �}�b�s���O�������\�b�h
    /// </summary>
    /// <param name="castPoint">�}�b�s���O�����Ώۍ��W�l</param>
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

            Instantiate(mapCheckObj, hit.point, Quaternion.identity);// �}�b�s���O�����m�F�p�I�u�W�F�N�g�̔z�u
            process++;
        }

        if (process == 6)
        {
            ghostGenerator.InstantiateGhost();
        }
    }
}
