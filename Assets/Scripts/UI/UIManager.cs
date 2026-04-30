using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public ReelController[] reels;
    public Button spinButton;

    private bool isSpinning = false;

    public void OnSpinButtonClicked()
    {
        if (isSpinning) return;

        if (reels == null || reels.Length == 0)
        {
            Debug.LogError("Reels not assigned!");
            return;
        }

        isSpinning = true;
        spinButton.interactable = false;

        resultText.text = "Spinning...";

        foreach (var reel in reels)
        {
            reel.StartSpin();
        }

        Invoke(nameof(StopReel1), 1.2f); // slightly delayed for better feel
    }

    void StopReel1()
    {
        reels[0].StopSpin();
        Invoke(nameof(StopReel2), 0.6f);
    }

    void StopReel2()
    {
        reels[1].StopSpin();
        Invoke(nameof(StopReel3), 0.6f);
    }

    void StopReel3()
    {
        reels[2].StopSpin();

        CheckWin();

        isSpinning = false;
        spinButton.interactable = true;
    }

    void CheckWin()
    {
        Symbol s1 = reels[0].GetCenterSymbol();
        Symbol s2 = reels[1].GetCenterSymbol();
        Symbol s3 = reels[2].GetCenterSymbol();

        if (s1 == null || s2 == null || s3 == null)
        {
            resultText.text = "Error!";
            Debug.LogError("One or more symbols are null");
            return;
        }

        if (s1.symbolName == s2.symbolName && s2.symbolName == s3.symbolName)
        {
            resultText.text = "🎉 JACKPOT! +" + s1.payout;
        }
        else
        {
            resultText.text = "❌ Try Again";
        }
    }
}