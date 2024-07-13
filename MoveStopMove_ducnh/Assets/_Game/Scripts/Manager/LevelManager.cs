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

    public void GenerateLevel()
    {
        level=Instantiate(levels[0]);
    }
    

    
}
