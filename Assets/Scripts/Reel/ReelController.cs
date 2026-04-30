using UnityEngine;
using System.Collections.Generic;

public class ReelController : MonoBehaviour
{
    public float spinSpeed = 200f;
    public float symbolSpacing = 100f;

    public List<Symbol> availableSymbols;

    private List<RectTransform> symbols = new List<RectTransform>();
    private bool isSpinning = false;
    private int targetIndex = 0;

    void Start()
    {
        symbols.Clear();

        SymbolView[] symbolViews = GetComponentsInChildren<SymbolView>();

        foreach (var sv in symbolViews)
        {
            symbols.Add(sv.GetComponent<RectTransform>());
        }

        Debug.Log($"{gameObject.name} Total Symbols Found: {symbols.Count}");
    }

    void Update()
    {
        if (!isSpinning) return;

        foreach (var symbol in symbols)
        {
            symbol.anchoredPosition += Vector2.down * spinSpeed * Time.deltaTime;

            if (symbol.anchoredPosition.y < -300f)
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

    Symbol GetRandomSymbol()
    {
        if (availableSymbols == null || availableSymbols.Count == 0)
        {
            Debug.LogError("No symbols assigned in availableSymbols!");
            return null;
        }

        int index = Random.Range(0, availableSymbols.Count);
        return availableSymbols[index];
    }

    public void StartSpin()
    {
        if (symbols.Count == 0)
        {
            Debug.LogError("No symbols found in reel!");
            return;
        }

        isSpinning = true;

        targetIndex = Random.Range(0, symbols.Count);
        targetIndex = Mathf.Clamp(targetIndex, 0, symbols.Count - 1);

        // Assign random symbols visually
        foreach (var symbol in symbols)
        {
            Symbol randomSymbol = GetRandomSymbol();

            if (randomSymbol != null)
                symbol.GetComponent<SymbolView>().SetSymbol(randomSymbol);
        }

        Debug.Log($"{gameObject.name} Target Index: {targetIndex}");
    }

    public void StopSpin()
    {
        isSpinning = false;
        SnapToTarget();
    }

    void SnapToTarget()
    {
        if (symbols.Count == 0) return;

        if (targetIndex < 0 || targetIndex >= symbols.Count)
        {
            Debug.LogWarning($"Invalid targetIndex: {targetIndex}");
            targetIndex = 0;
        }

        RectTransform targetSymbol = symbols[targetIndex];
        float offset = targetSymbol.anchoredPosition.y;

        foreach (var symbol in symbols)
        {
            symbol.anchoredPosition -= new Vector2(0, offset);
        }
    }

    public Symbol GetCenterSymbol()
    {
        if (symbols.Count == 0) return null;

        float closest = float.MaxValue;
        RectTransform closestSymbol = null;

        foreach (var symbol in symbols)
        {
            float distance = Mathf.Abs(symbol.anchoredPosition.y);

            if (distance < closest)
            {
                closest = distance;
                closestSymbol = symbol;
            }
        }

        if (closestSymbol == null) return null;

        return closestSymbol.GetComponent<SymbolView>().currentSymbol;
    }
}