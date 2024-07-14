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

    [SerializeField] private Player playerPrefab;
    [SerializeField] private Bot botPrefab;
    private float max_x, min_x, max_z, min_z;
    private int id = 0;

    List<Vector3> spawnPositions = new List<Vector3>();
    private Transform characterSpawnLocation;
    [SerializeField] private int totalCharacter;
    [SerializeField] private int maximumNoExistedBot;
    private int numberOfSpawnedBots;
    private int numberOfExistedBots;
    private int remainedNoBots => totalCharacter - numberOfSpawnedBots - 1;

    public int NumberOfExistedBots { get => numberOfExistedBots; set => numberOfExistedBots = value; }

    private void Awake()
    {
        characterSpawnLocation = characterSpawnLocations[Random.Range(0, characterSpawnLocations.Count)];
        max_x = characterSpawnLocations[1].position.x;
        min_x = characterSpawnLocations[0].position.x;
        max_z = characterSpawnLocations[0].position.z;
        min_z = characterSpawnLocations[3].position.z;
        GenerateSpawnPoint();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log(spawnPositions.Count);
        SpawnPlayer();
        SpawnBots();
    }

    // Update is called once per frame
    void Update()
    {
        if (NumberOfExistedBots < maximumNoExistedBot && remainedNoBots > 0)
        {
            SpawnBotAtRandomPos();
        }
    }

    public void GenerateSpawnPoint()
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
            // pos_x -= sign_x * offset;
            // pos_z -= sign_z * offset;
            // pos_x=(pos_x<max_x&&pos_x>min_x)?pos_x:pos_x-(-2)*sign_x*offset;
            // pos_z=(pos_z<max_z&&pos_z>min_z)?pos_z:pos_z-(-2)*sign_z*offset;        
            Vector3 pos = new Vector3(pos_x, pos_y, pos_z);
            if (HasObtacle(pos)) continue;
            spawnPositions.Add(pos);

        }
    }

    public void SpawnPlayer()
    {
        int rdn = Random.Range(0, spawnPositions.Count);
        Vector3 spawnPos = spawnPositions[rdn];
        Player player = SimplePool.Spawn<Player>(playerPrefab, spawnPos, playerPrefab.TF.rotation);
        player.SetJoyStickController(GameManager.Ins.Joystick);
        // Skin hairSkin=hairDataConfigSO.GetHairSkinByEnum(HairSkinEnum.Horn);
        player.InitFullSetSkin(0);
        player.OnInit(id);
        id++;
        GameManager.Ins.SetCameraTarget(player);
        // UserDataManager.Ins.Player=player;
        totalCharacter--;
        NumberOfExistedBots++;
        spawnPositions.RemoveAt(rdn);
    }

    public void SpawnBotAtRandomPos()
    {
        int rdn = Random.Range(0, spawnPositions.Count);
        Vector3 spawnPos = spawnPositions[rdn];
        Bot bot = SimplePool.Spawn<Bot>(botPrefab, spawnPos, botPrefab.TF.rotation);
        bot.OnInit(id);
        id++;
        totalCharacter--;
        NumberOfExistedBots++;
    }

    public void SpawnBots()
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            Vector3 spawnPos = spawnPositions[i];
            Bot bot = SimplePool.Spawn<Bot>(botPrefab, spawnPos, botPrefab.TF.rotation);
            bot.InitRandomItem();
            bot.OnInit(id);
            id++;
            totalCharacter--;
            NumberOfExistedBots++;
        }
    }

    public bool HasObtacle(Vector3 pos)
    {
        if (Physics.Raycast(pos, Vector3.up, out RaycastHit hit, 1f))
        {
            return true;
        }
        return false;
    }


}
