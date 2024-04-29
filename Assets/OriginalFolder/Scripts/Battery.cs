using UnityEngine;

/// <summary>
/// バッテリー用スクリプト
/// </summary>
public class Battery : MonoBehaviour
{
    private float batteryPer = 100f;//　バッテリーのパーセンテージ
    private Vector3 batterySize;//　最大バッテリー量
    [SerializeField]
    private float decleasingSpeed = 10f;//　バッテリー現象スピード
    [SerializeField]
    private float caughtChargeValue = 20f;//　バッテリー回復量
    [SerializeField]
    private float autoChargeSpeed = 0.1f;//　バッテリー自動回復量

    // Start is called before the first frame update
    void Start()
    {
        batterySize = gameObject.transform.localScale;
    }

    /// <summary>
    /// バッテリー使用メソッド
    /// </summary>
    /// <returns>バッテリー使用中フラグ（true）</returns>
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
    /// バッテリー回復メソッド
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
    /// バッテリー自動回復メソッド
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
