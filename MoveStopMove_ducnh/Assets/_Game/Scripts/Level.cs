using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
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
    Player player;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Bot botPrefab;
    private float max_x, min_x, max_z, min_z;
    private int id;
    private bool flag;
    List<Vector3> spawnPositions = new List<Vector3>();
    private Transform characterSpawnLocation;
    [SerializeField] private int totalCharacter;
    [SerializeField] private int maximumNoExistedBot;
    private int numberOfExistedBots;
    private int remainedNoBots;

    public void OnInit(){
        numberOfExistedBots=0;
        SpawnPlayer();
        SpawnBots();
        flag=false;
    }

    private void Awake()
    {
        characterSpawnLocation = characterSpawnLocations[Random.Range(0, characterSpawnLocations.Count)];
        max_x = characterSpawnLocations[1].position.x;
        min_x = characterSpawnLocations[0].position.x;
        max_z = characterSpawnLocations[0].position.z;
        min_z = characterSpawnLocations[3].position.z;
        GenerateSpawnPoint();
        remainedNoBots=totalCharacter-1;
    }

    // Start is called before the first frame update
    private void Start()
    {
       OnInit();
    }

    // Update is called once per frame
    private void Update()
    {
        if(!flag) return;
        if (numberOfExistedBots <= maximumNoExistedBot && remainedNoBots > 0)
        {
            SpawnBotAtRandomPos();
        }
        if(remainedNoBots<=0){
            GameManager.Ins.SetGameResult(EGameResult.Win);
            GameManager.Ins.ChangeState(GameManager.State.EndGame);
        }
    }

    private void FixedUpdate(){
        
    }

    private void GenerateSpawnPoint()
    {
        float pos_x = characterSpawnLocation.position.x;
        float pos_y = characterSpawnLocation.position.y;
        float pos_z = characterSpawnLocation.position.z;
        float sign_x = Mathf.Sign(pos_x);
        float sign_z = Mathf.Sign(pos_z);

        while (spawnPositions.Count < maximumNoExistedBot + 1)
        {
            float offset = Random.Range(-40f, 40f);
            pos_x = Random.Range(min_x, max_x);
            pos_z = Random.Range(min_z, max_z);
            Vector3 pos = new Vector3(pos_x, pos_y, pos_z);
            if (HasObtacle(pos)) continue;
            spawnPositions.Add(pos);

        }
    }

    private void SpawnPlayer()
    {
        int rdn = Random.Range(0, spawnPositions.Count);
        Vector3 spawnPos = spawnPositions[rdn];
        SpawnPlayer(spawnPos);
        spawnPositions.RemoveAt(rdn);
        UserDataManager.Ins.Player=player;
        player.OnInit(id);
        id++;
    }

    private void SpawnPlayer(Vector3 pos){
        player = SimplePool.Spawn<Player>(playerPrefab, pos, playerPrefab.TF.rotation);
        GameManager.Ins.SetCameraTarget(player);
    }

    private void SpawnBotAtRandomPos()
    {
        int rdn = Random.Range(0, spawnPositions.Count);
        Vector3 spawnPos = spawnPositions[rdn];
        Bot bot = SimplePool.Spawn<Bot>(botPrefab, spawnPos, botPrefab.TF.rotation);
        bot.OnInit(id);
        id++;
        numberOfExistedBots++;
    }

    private void SpawnBots()
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            Vector3 spawnPos = spawnPositions[i];
            Bot bot = SimplePool.Spawn<Bot>(botPrefab, spawnPos, botPrefab.TF.rotation);
            bot.OnInit(id);
            id++;
            totalCharacter--;
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

    public void SetController(DynamicJoystick dynamicJoystick){
        if(player==null) return;
        player.SetJoyStickController(GameManager.Ins.Joystick);
    }

    public int GetNumberOfRemainBots(){
        return remainedNoBots;
    }

    public void DecreseNORemainBots(){
        remainedNoBots--;
        numberOfExistedBots--;
    }

    private void OnDestroy(){
        SimplePool.Collect(PoolType.Bot);
    }

    public void RevivePlayer(){
        if(player==null) return;
        if(player.CanRevive){
            SpawnPlayer(player.TF.position);
            player.Score=UserDataManager.Ins.Player.Score;
            player.OnInit(UserDataManager.Ins.Player.Id);
            player.CanRevive=false;
            UserDataManager.Ins.Player=player;
            StartCoroutine(player.SetImmortalState(5f));
            
        }
    }
}
