using TMPro;
using UnityEngine;

public class ControlsHider : MonoBehaviour
{
    private TextMeshProUGUI controls;

    private void Start()
    {
        controls = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            controls.enabled = !controls.enabled;
        }
    }
}