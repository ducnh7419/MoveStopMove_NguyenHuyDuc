using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICStartScreen : UICanvas
{
    [SerializeField] private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        StartBlinking();
    }

    IEnumerator Blink()
    {
        while (true)
        {
            switch(text.color.a.ToString()){
                case "0":
                    text.color=new Color(text.color.g, text.color.r, text.color.b,1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    text.color=new Color(text.color.g, text.color.r, text.color.b,0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    
    }

    void StartBlinking(){
        StartCoroutine(Blink());
    }
    void StopBlinking()
    {
        StopCoroutine(Blink());
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            GameManager.Ins.ChangeState(GameManager.State.MainMenu);
        }
    }
}
