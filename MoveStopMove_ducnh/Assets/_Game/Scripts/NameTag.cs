using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NameTag : MonoBehaviour
{
    public Transform TF;
    public GameObject ScoreTextGO;
    public TextMeshPro nameText;
    public TextMeshPro scoreText;
    private Transform cameraTF;

    protected virtual void Start(){
        cameraTF = GameManager.Ins.M_Camera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Ins.IsState(GameManager.State.OngoingGame)){
            ScoreTextGO.SetActive(true);
        }else{
            ScoreTextGO.SetActive(false);
        }
        TF.LookAt(cameraTF);
    }

    public void SetNameText(string name){
        nameText.text=name;
    }

    public void SetScoreText(int score){
        scoreText.text=score.ToString();
    }
}
