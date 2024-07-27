using System.Collections;
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

    public void StartGame(){
        level.OnInit();
    }

    public void GenerateLevel()
    {
        if(level==null){
            level=Instantiate(levels[0]);
        }
            
    }

    public void RestartLevel(){
        
        GenerateLevel();
        GameManager.Ins.ChangeState(GameManager.State.StartGame);
    }

    public void DestroyLevel(){
        if(level!=null){
            Destroy(level.gameObject);
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
    
}
