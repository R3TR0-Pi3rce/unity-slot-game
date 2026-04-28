using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public ReelController[] reels;

    public void OnSpinButtonClicked()
    {
        resultText.text = "Spinning...";

        foreach (var reel in reels)
        {
            reel.StartSpin();
        }

        Invoke("StopAllReels", 2f);
    }

    void StopAllReels()
    {
        foreach (var reel in reels)
        {
            reel.StopSpin();
        }

        resultText.text = "Stopped!";
    }
}