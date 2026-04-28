using UnityEngine;

public class ReelController : MonoBehaviour
{
    public RectTransform reelContent;
    public float spinSpeed = 1000f;
    private bool isSpinning = false;

    public void StartSpin()
    {
        isSpinning = true;
    }

    public void StopSpin()
    {
        isSpinning = false;
    }

    void Update()
    {
        if (isSpinning)
        {
            reelContent.anchoredPosition += Vector2.down * spinSpeed * Time.deltaTime;

        // Loop logic
            if (reelContent.anchoredPosition.y > reelContent.sizeDelta.y)
            {
                reelContent.anchoredPosition = Vector2.zero;
            } 
        }
    }
}