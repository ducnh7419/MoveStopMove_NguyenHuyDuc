using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BotIndicator : Indicator
{
    [SerializeField] private TextMeshProUGUI arrowText;
    [SerializeField] private Image arrowImage;
    [SerializeField] private Image circleImage;

    public void SetArrowText(string text)
    {
        arrowText.text = text;
    }

    public void SetIndicatorColor(Material material){
        arrowImage.material=material;
        circleImage.material=material;
    }

    public new void SetImageColor(Color color){
        // SetIndicatorColor()
    }
}
