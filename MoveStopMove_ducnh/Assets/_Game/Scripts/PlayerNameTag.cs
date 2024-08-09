using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameTag : NameTag
{
    public TMP_InputField inputField;

    protected override void Start() {
        base.Start();
        inputField.onEndEdit.AddListener(EndInputHandler);
    }

    private void EndInputHandler(string arg0)
    {
        
    }
}
