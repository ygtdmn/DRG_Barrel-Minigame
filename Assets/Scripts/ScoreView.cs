using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private string initialText;

    private void Start()
    {
        initialText = text.text;
    }

    private void Update()
    {
        text.text = initialText.Replace("%1%", GameManager.Instance.point.ToString()).Replace("%2%", GameManager.Instance.kickCount.ToString());
    }
}