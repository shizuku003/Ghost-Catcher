using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���U���g���e�\���p�X�N���v�g
/// </summary>
public class ShowResult : MonoBehaviour
{
    private ScoreData scoreData;
    [SerializeField]
    private Text resultText;

    // Start is called before the first frame update
    void Start()
    {
        DestroyObject(GameObject.Find("MeshingNodes"));
        DestroyObject(GameObject.Find("GameSetting"));
        scoreData = GameObject.Find("ScoreData").GetComponent<ScoreData>();

        resultText.text = "Score: " + scoreData.ScoreSend() + "pts";
    }
}
