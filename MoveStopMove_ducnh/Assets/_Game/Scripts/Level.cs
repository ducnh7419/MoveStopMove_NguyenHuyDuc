using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using GlobalConstants;
using UnityEngine;

public class Level : MonoBehaviour
{
    /// <summary>
    /// List Contain Start Spawn Location Of character Where each is at 4 conners of map
    /// It's store in order
    /// 0. Top Left conner
    /// 1. Top Right conner
    /// 2. Bot Left conner
    /// 3. Bot Right conner
    /// </summary>
    [SerializeField] private List<Transform> characterSpawnLocations;
    private Player player;
    [SerializeField] private Player playerPrefab;
    List<Character> listChars=new();
    [SerializeField] private Bot botPrefab;
    [SerializeField] private Ulti ultiPowerUpsPrefab;
    private float max_x, min_x, max_z, min_z;
    private int id;
    List<Vector3> spawnPositions = new List<Vector3>();
    private Transform characterSpawnLocation;
    [SerializeField] private int totalCharacter;
    [SerializeField] private int maximumNoExistedBot;
    private int numberOfExistedBots;
    private int remainedNoBots;
    private float timer;
    private float randomTime;

    public void OnInit()
    {
        id=0;
        remainedNoBots = totalCharacter - 1;
        numberOfExistedBots = 0;
        SpawnPlayer();
        SpawnBots();
        timer=0;
        randomTime=Mathf.Round(Random.Range(10f, 20f));
    }

    private void Awake()
    {
        characterSpawnLocation = characterSpawnLocations[Random.Range(0, characterSpawnLocations.Count)];
        max_x = characterSpawnLocations[1].position.x;
        min_x = characterSpawnLocations[0].position.x;
        max_z = characterSpawnLocations[0].position.z;
        min_z = characterSpawnLocations[3].position.z;
        remainedNoBots = totalCharacter - 1;
        GenerateSpawnPoint();
    }

    // Start is called before the first frame update
    private void Start()
    {
        OnInit();
    }

    private void FixedUpdate(){
        timer+=Time.fixedDeltaTime;

    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.Ins.IsState(GameManager.State.OngoingGame))
        {
            return;
        }
        SpawnBotAtRandomPos();
        GenerateRandomPowerUps(EPowerUps.Ulti);
        if (remainedNoBots <= 0)
        {
            SoundManager.Ins.PlaySFX(ESound.VICTORY);
            GameManager.Ins.SetGameResult(EGameResult.Win);
            GameManager.Ins.ChangeState(GameManager.State.EndGame);
        }
    }

    private string GetRandomName(){
        return CharName.CHAR_NAMES[Random.Range(0, CharName.CHAR_NAMES.Length)];
    }

    private void RemoveCharFromList(Character character){
        listChars.Remove(character);
        DecreseNORemainBots();
    }

    public void OnReset(){
        listChars.Clear();
        CollectALL();
        spawnPositions.Clear();
        GenerateSpawnPoint();
        OnInit();
        
    }

    private void GenerateSpawnPoint()
    {
        float pos_x = characterSpawnLocation.position.x;
        float pos_y = characterSpawnLocation.position.y;
        float pos_z = characterSpawnLocation.position.z;
        while (spawnPositions.Count < maximumNoExistedBot + 1)
        {
            pos_x = Random.Range(min_x, max_x);
            pos_z = Random.Range(min_z, max_z);
            Vector3 pos = new Vector3(pos_x, pos_y, pos_z);
            if (HasObtacle(pos)) continue;
            spawnPositions.Add(pos);
        }
    }

    private void GenerateRandomPowerUps(EPowerUps ePowerUps){
        if(Mathf.Round(timer)!=randomTime){
            return;
        }
        timer=0;
        float pos_y = characterSpawnLocation.position.y+0.5f;
        float pos_x = Random.Range(min_x, max_x);
        float pos_z = Random.Range(min_z, max_z);
        Vector3 pos=new(pos_x, pos_y, pos_z);
        switch (ePowerUps){
            case EPowerUps.Ulti:
                SimplePool.Spawn<Ulti>(ultiPowerUpsPrefab,pos,ultiPowerUpsPrefab.TF.rotation);
                break;
        }
    }

    private void SpawnPlayer()
    {
        int rdn = Random.Range(0, spawnPositions.Count);
        Vector3 spawnPos = spawnPositions[rdn];
        
        SpawnPlayer(spawnPos);
        spawnPositions.RemoveAt(rdn);
        UserDataManager.Ins.Player = player;
        player.OnInit(id);
        // player.SetName(name);
        player.AddUnityAction(EndGame);
        listChars.Add(player);
        id++;
    }

    private void SpawnPlayer(Vector3 pos)
    {
        player = SimplePool.Spawn<Player>(playerPrefab, pos, playerPrefab.TF.rotation);
        GameManager.Ins.SetCameraTarget(player);

    }

    private void SpawnBotAtRandomPos()
    {
        if (!((remainedNoBots > 0) && (numberOfExistedBots < maximumNoExistedBot)&&(remainedNoBots>=maximumNoExistedBot))) return;
        string name=GetRandomName();
        int rdn = Random.Range(0, spawnPositions.Count);
        Vector3 spawnPos = spawnPositions[rdn];
        Bot bot = SimplePool.Spawn<Bot>(botPrefab, spawnPos, botPrefab.TF.rotation);
        bot.OnInit(id);
        bot.SetName(name);
        bot.AddUnityAction(()=>RemoveCharFromList(bot));
        listChars.Add(player);
        id++;
        numberOfExistedBots++;
    }

    private void SpawnBots()
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            string name=GetRandomName();
            Vector3 spawnPos = spawnPositions[i];
            Bot bot = SimplePool.Spawn<Bot>(botPrefab, spawnPos, botPrefab.TF.rotation);
            bot.OnInit(id);
            bot.AddUnityAction(()=>RemoveCharFromList(bot));
            bot.SetName(name);
            listChars.Add(bot);
            id++;
            numberOfExistedBots++;
        }
    }

    private bool HasObtacle(Vector3 pos)
    {
        if (Physics.Raycast(pos, Vector3.up, out RaycastHit hit, 1f))
        {
            return true;
        }
        return false;
    }

    public void SetController(DynamicJoystick dynamicJoystick)
    {
        if (player == null) return;
        player.SetJoyStickController(GameManager.Ins.Joystick);
    }

    public int GetNumberOfRemainBots()
    {
        return remainedNoBots;
    }

    public void DecreseNORemainBots()
    {
        remainedNoBots--;
        numberOfExistedBots--;
    }

    public void CollectALL()
    {
        SimplePool.CollectAll();
    }

    public void RevivePlayer()
    {
        if (player == null) return;
        if (player.CanRevive)
        {
            SpawnPlayer(player.TF.position);
            player.Score = UserDataManager.Ins.Player.Score;
            player.OnInit(UserDataManager.Ins.Player.Id);
            player.CanRevive = false;
            player.AddUnityAction(EndGame);
            UserDataManager.Ins.Player = player;
            StartCoroutine(player.SetImmortalState(5f));
        }
    }
    public void EndGame(){
        SoundManager.Ins.PlaySFX(ESound.LOSE);
        GameManager.Ins.SetGameResult(EGameResult.Lose);
        StartCoroutine(GameManager.Ins.DelayChangeState(GameManager.State.EndGame,.10f));
    }
}
