using System;
using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager ins;
    private State currState;
    public GameRuleSO GameRuleSO;
    public ItemDataConfigSO ItemDataConfigSO;
    public WeaponDataSO WeaponDataSO;

    public CameraFollow M_Camera;

    private Dictionary<Character,int> leaderboard=new Dictionary<Character,int>();
    public DynamicJoystick Joystick;

    private EGameResult eGameResult;

    public static GameManager Ins=>ins;

    public State CurrState { get => currState; }
    

    public enum State{
        None=0,
        StartScreen=1,
        MainMenu=2,
        LevelSelection=3,
        SkinShop=4,
        WeaponShop=5, 
        StartGame=6,
        OngoingGame=7,
        EndGame=8
    }


    // Start is called before the first frame update
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
        
        OnInit();
        ChangeState(State.StartScreen);
    }

    public bool IsState(State state){
        if(currState==state){
            return true;
        }
        return false;
    }

   
    public void SetGameResult(EGameResult gameResult){
        eGameResult=gameResult;
    }

    public void ChangeState(State state){
        currState=state;
        Debug.Log(state);
        switch (state)
        {
            case State.None:
                break;
            case State.StartScreen:
                currState=State.StartScreen;
                OnStartScreen();
                break;
            case State.MainMenu:
                currState=State.MainMenu;
                OnMainMenu();
                break;
            case State.SkinShop:
                currState=State.SkinShop;
                OnSkinShop();
                break;
            case State.WeaponShop:
                currState=State.WeaponShop;
                OnWeaponShop();
                break;
            case State.StartGame:
                currState=State.StartGame;
                OnStartGame();
                break;
            case State.OngoingGame:
                currState=State.OngoingGame;
                OnGoingGame();
                break;
            case State.EndGame:
                currState=State.EndGame;
                OnEndGame();
                break;
        }
    }

    private void OnWeaponShop()
    {
        UIManager.Ins.CloseAll();
        M_Camera.SetCameraPositionAndRotation(new Vector3(0.31f,12f,-10.2f),Quaternion.Euler(0,0,0));
        UIManager.Ins.OpenUI<UICShopWeapon>();
        UIManager.Ins.OpenUI<UICBudget>();
    }

    private void OnSkinShop()
    {
        UIManager.Ins.CloseAll();
        M_Camera.SetCameraPositionAndRotation(new Vector3(0.31f,0.7f,-10.2f),Quaternion.Euler(0,0,0));
        UIManager.Ins.OpenUI<UICShopSkin>();
        UIManager.Ins.OpenUI<UICBudget>();
    }

    // private void OnLevelSelectionMenu()
    // {
    //     UIManager.Ins.OpenUI<LevelSelection>();
    // }

    private void OnMainMenu()
    {
        UIManager.Ins.CloseAll();
        M_Camera.SetCameraPositionAndRotation(new Vector3(0.31f,2.45f,-10.2f),Quaternion.Euler(0,0,0));
        UIManager.Ins.OpenUI<UICMainMenu>();
    }

    

    private void OnStartScreen(){
        LevelManager.Ins.GenerateLevel();
        UIManager.Ins.OpenUI<UICStartScreen>();
    }

    private void OnGoingGame(){
        UIManager.Ins.OpenUI<UICInGameUI>();
    }

    private void OnStartGame(){
        
        SetCameraPositionAndRotation(new Vector3(0.03f,20.4f,-24.9f),Quaternion.Euler(40f,0,0));
        UIManager.Ins.CloseAll();
        UICJoystick uICJoystick=UIManager.Ins.OpenUI<UICJoystick>();
        Joystick=uICJoystick.DynamicJoystick;
        LevelManager.Ins.SetController(Joystick);
        ChangeState(State.OngoingGame);
        
    }

    public IEnumerator DelayChangeState(State state,float time){
        yield return new WaitForSeconds(time);
        ChangeState(state);
    }


    private void OnEndGame(){
        Time.timeScale=0;
        switch(eGameResult){
            case EGameResult.Win:
                UIManager.Ins.OpenUI<UICWin>();
                break;
            case EGameResult.Lose:
                UIManager.Ins.OpenUI<UICLose>();
                break;
        }

    }

    protected void OnInit()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
        //csv.OnInit();
        //userData?.OnInitData();

        //ChangeState(GameState.MainMenu);

        // OnStartScreen();
    }

    public void SetCameraTarget(Character target){
        M_Camera.SetCameraTarget(target);
    }
    public void SetCameraPositionAndRotation(Vector3 offset, Quaternion rotation){
        M_Camera.SetCameraPositionAndRotation(offset,rotation);
    }

    public void GoBackward()
    {
        Debug.Log(CurrState);
        currState--;
        ChangeState(CurrState);
    }
}
