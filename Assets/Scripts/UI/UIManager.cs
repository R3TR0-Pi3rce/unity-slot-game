using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public ReelController[] reels;

    private bool isSpinning = false;

    public void OnSpinButtonClicked()
    {
        if (isSpinning) return;

        isSpinning = true;
        resultText.text = "Spinning...";

        foreach (var reel in reels)
        {
            reel.StartSpin();
        }

        Invoke(nameof(StopReel1), 1f);
    }

    void StopReel1()
    {
        reels[0].StopSpin();
        Invoke(nameof(StopReel2), 0.5f);
    }

    void StopReel2()
    {
        reels[1].StopSpin();
        Invoke(nameof(StopReel3), 0.5f);
    }

    void StopReel3()
    {
        reels[2].StopSpin();

        CheckWin();

        isSpinning = false;
    }

    void CheckWin()
    {
        Symbol s1 = reels[0].GetCenterSymbol();
        Symbol s2 = reels[1].GetCenterSymbol();
        Symbol s3 = reels[2].GetCenterSymbol();

        // 🔥 FIX: compare names instead of references
        if (s1.symbolName == s2.symbolName && s2.symbolName == s3.symbolName)
        {
            resultText.text = "WIN! +" + s1.payout;
        }
        else
        {
            resultText.text = "Try Again";
        }
    }
}