using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    public void OnSpinButtonClicked()
    {
        Debug.Log("Spin button clicked");

        resultText.text = "Spinning...";
    }
}