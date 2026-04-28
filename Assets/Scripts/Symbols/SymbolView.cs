using UnityEngine;
using UnityEngine.UI;

public class SymbolView : MonoBehaviour
{
    public Image image;

    public void SetSymbol(Symbol symbol)
    {
        image.sprite = symbol.sprite;
    }
}