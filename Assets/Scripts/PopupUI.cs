using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    public TextMeshProUGUI MouseoverTextUI;
    public RectTransform TextboxHeightAdjustment;
    public void OnMouseExit()
    {
        // Hide the info box
        gameObject.SetActive(false);
    }
    public void ChangeText(string ChangedText)
    {
        MouseoverTextUI.text = ChangedText;
        //the 0.4f is because we have scaled the text
        TextboxHeightAdjustment.sizeDelta = new Vector2(TextboxHeightAdjustment.sizeDelta.x, MouseoverTextUI.preferredHeight*0.4f-25);
    }
}
