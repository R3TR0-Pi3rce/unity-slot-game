using UnityEngine;
using System.Collections.Generic;

public class ReelController : MonoBehaviour
{
    public float spinSpeed = 200f;

    private List<RectTransform> symbols = new List<RectTransform>();
    private bool isSpinning = false;

    private float symbolSpacing = 100f; // adjust to your symbol size

    void Start()
    {
        foreach (Transform child in transform.GetChild(0)) // ReelContent
        {
            symbols.Add(child.GetComponent<RectTransform>());
        }
    }

    void Update()
    {
        if (!isSpinning) return;

        foreach (var symbol in symbols)
        {
            // Move each symbol down
            symbol.anchoredPosition += Vector2.down * spinSpeed * Time.deltaTime;

            // If symbol goes too low → bring it to top
            if (symbol.anchoredPosition.y < -300f) // adjust if needed
            {
                float highestY = GetHighestY();
                symbol.anchoredPosition = new Vector2(
                    symbol.anchoredPosition.x,
                    highestY + symbolSpacing
                );
            }
        }
    }

    float GetHighestY()
    {
        float maxY = float.MinValue;

        foreach (var s in symbols)
        {
            if (s.anchoredPosition.y > maxY)
                maxY = s.anchoredPosition.y;
        }

        return maxY;
    }

    public void StartSpin()
    {
        isSpinning = true;
    }

    public void StopSpin()
    {
        isSpinning = false;
    }
}