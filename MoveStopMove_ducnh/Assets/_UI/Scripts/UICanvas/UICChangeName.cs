
using System;
using TMPro;
using UnityEngine.UI;

public class UICChangeName : UICanvas
{
   public TMP_InputField inputField;
   public Button okButton;

   private void Start() {
        inputField.text=UserDataManager.Ins.GetPlayerName();
        okButton.onClick.AddListener(OnOKButtonClicked);    
   }

    private void OnOKButtonClicked()
    {
        UserDataManager.Ins.SavePlayerName(inputField.text);
        Close(0f);
    }
}
