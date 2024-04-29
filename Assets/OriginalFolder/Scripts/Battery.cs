using UnityEngine;

/// <summary>
/// �o�b�e���[�p�X�N���v�g
/// </summary>
public class Battery : MonoBehaviour
{
    private float batteryPer = 100f;//�@�o�b�e���[�̃p�[�Z���e�[�W
    private Vector3 batterySize;//�@�ő�o�b�e���[��
    [SerializeField]
    private float decleasingSpeed = 10f;//�@�o�b�e���[���ۃX�s�[�h
    [SerializeField]
    private float caughtChargeValue = 20f;//�@�o�b�e���[�񕜗�
    [SerializeField]
    private float autoChargeSpeed = 0.1f;//�@�o�b�e���[�����񕜗�

    // Start is called before the first frame update
    void Start()
    {
        batterySize = gameObject.transform.localScale;
    }

    /// <summary>
    /// �o�b�e���[�g�p���\�b�h
    /// </summary>
    /// <returns>�o�b�e���[�g�p���t���O�itrue�j</returns>
    public bool BatteryUse()
    {
        if (batteryPer <= 0f)
        {
            batteryPer = 0f;
            return false;
        }

        batteryPer -= Time.deltaTime * decleasingSpeed;
        gameObject.transform.localScale = new Vector3(batterySize.x * batteryPer * 0.01f, batterySize.y, batterySize.z);

        return true;
    }

    /// <summary>
    /// �o�b�e���[�񕜃��\�b�h
    /// </summary>
    public void BatteryCharge()
    {
        batteryPer += caughtChargeValue;

        if (batteryPer > 100f)
        {
            batteryPer = 100f;
        }

        gameObject.transform.localScale = new Vector3(batterySize.x * batteryPer * 0.01f, batterySize.y, batterySize.z);
    }

    /// <summary>
    /// �o�b�e���[�����񕜃��\�b�h
    /// </summary>
    public void BatteryAutoCharge()
    {
        batteryPer += autoChargeSpeed;

        if (batteryPer > 100f)
        {
            batteryPer = 100f;
        }

        gameObject.transform.localScale = new Vector3(batterySize.x * batteryPer * 0.01f, batterySize.y, batterySize.z);
    }
}
