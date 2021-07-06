using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TextHightlight : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        currentColor = text.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HightlightText()
    {
        text.color = new Color32(209, 182, 3, 255);
    }

    public void RevertColor()
    {
        text.color = currentColor;
    }
}
