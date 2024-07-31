
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager ins;
    [SerializeField] CameraFollow m_camera;
    public static LevelManager Ins => ins;
    private Level level;
    [SerializeField] private List<Level> levels;

    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
        
    }

    

    public void GenerateLevel()
    {
        if(level==null){
            level=Instantiate(levels[0]);
        }
            
    }

    public void RestartLevel(){
        DestroyLevel();
        GenerateLevel();
        StartCoroutine(GameManager.Ins.DelayChangeState(GameManager.State.StartGame,.5f));
    }


    public void DestroyLevel(){
        if(level!=null){
            Destroy(level.gameObject);
            level=null;
        }
    }

    public void SetController(DynamicJoystick joystick){
        level.SetController(joystick);
    }
    
    public int GetNORemainBots(){
         return level.GetNumberOfRemainBots();
    }

    public void DecreseNORemainBots(){
        level.DecreseNORemainBots();
    }

    public void RevivePlayer(){
        level.RevivePlayer();
        SetController(GameManager.Ins.Joystick);
        
    }
    
}
