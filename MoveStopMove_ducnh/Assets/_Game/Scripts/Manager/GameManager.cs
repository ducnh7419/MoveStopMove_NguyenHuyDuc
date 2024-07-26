using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    private static GameManager ins;
    private State currState;

    public ItemDataConfigSO ItemDataConfigSO;
    public WeaponDataSO WeaponDataSO;

    [SerializeField] private CameraFollow m_Camera;

    private Dictionary<Character,int> leaderboard=new Dictionary<Character,int>();
    public DynamicJoystick Joystick;

    private GameResult currentResult;

    public static GameManager Ins=>ins;

    public GameResult CurrentResult { get => currentResult; set => currentResult = value; }
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


    public enum GameResult{
        None=0,
        Win=1,
        Lose=2
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
        ChangeState(State.MainMenu);
    }

    public void SetCharacterScore(Character character ,int score){
        leaderboard[character]=score;
    }

    public Character GetCharacterHaveHighestScore(){
        List<Character> keyList=new List<Character>(leaderboard.Keys);
        List<int> valueList=new List<int>(leaderboard.Values);
        int max=valueList[0];
        int index=0;
        for(int i=1;i<valueList.Count;i++){
            if(valueList[i]>max){
                max=valueList[i];
                index=i;
            }
        }
        return keyList[index];
    }

    

    public void ChangeState(State state){
        currState=state;
        switch (state)
        {
            case State.None:
                break;
            case State.StartScreen:
                // OnStartScreen();
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
                // OnGoingGame();
                break;
            case State.EndGame:
                // OnEndGame();
                break;
        }
    }

    private void OnWeaponShop()
    {
        UIManager.Ins.CloseAll();
        m_Camera.SetCameraPositionAndRotation(new Vector3(0.31f,12f,-10.2f),Quaternion.Euler(0,0,0));
        UIManager.Ins.OpenUI<UICShopWeapon>();
    }

    private void OnSkinShop()
    {
        UIManager.Ins.CloseAll();
        m_Camera.SetCameraPositionAndRotation(new Vector3(0.31f,1.31f,-10.2f),Quaternion.Euler(0,0,0));
        UIManager.Ins.OpenUI<UICShopSkin>();
    }

    // private void OnLevelSelectionMenu()
    // {
    //     UIManager.Ins.OpenUI<LevelSelection>();
    // }

    private void OnMainMenu()
    {
        UIManager.Ins.CloseAll();
        m_Camera.SetCameraPositionAndRotation(new Vector3(0.31f,2.45f,-10.2f),Quaternion.Euler(0,0,0));
        Joystick=UIManager.Ins.OpenUI<UICJoystick>().dynamicJoystick;
        LevelManager.Ins.GenerateLevel();
        Joystick.enabled=false;
        UIManager.Ins.OpenUI<UICMainMenu>();

    }

    // private void OnStartScreen(){
    //     UIManager.Ins.OpenUI<OpeningScreen>();

    // }

    // private void OnGoingGame(){
    //     UIManager.Ins.OpenUI<IngameUI>();
    // }

    private void OnStartGame(){
        SetCameraPositionAndRotation(new Vector3(0.03f,20.4f,-24.9f),Quaternion.Euler(40f,0,0));
        UIManager.Ins.CloseUI<UICMainMenu>();
        Joystick.enabled=true;
        StartCoroutine(DelayChangeState(State.OngoingGame));
        
    }

    IEnumerator DelayChangeState(State state){
        yield return new WaitForSeconds(1f);
        GameManager.Ins.ChangeState(state);
    }

    // IEnumerator DelayShowingEndGameCanvas(){
    //     yield return new WaitForSeconds(7);
    //     Time.timeScale=0;
    //     if(currentResult==GameResult.Win){
    //         UIManager.Ins.OpenUI<Win>();
    //     }else{
    //         UIManager.Ins.OpenUI<Lose>();
    //     }
    // }

    // private void OnEndGame(){
    //     StartCoroutine(DelayShowingEndGameCanvas());

    // }

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
        m_Camera.SetCameraTarget(target);
    }
    public void SetCameraPositionAndRotation(Vector3 offset, Quaternion rotation){
        m_Camera.SetCameraPositionAndRotation(offset,rotation);
    }

    public void GoBackward()
    {
        Debug.Log(CurrState);
        currState--;
        ChangeState(CurrState);
    }
}
