using UnityEngine;
using UnityEngine.UI;

public class SymbolView : MonoBehaviour
{
    public Image image;
    public Symbol currentSymbol;

    public void SetSymbol(Symbol symbol)
    {
        currentSymbol = symbol;
        image.sprite = symbol.sprite;
    }
}