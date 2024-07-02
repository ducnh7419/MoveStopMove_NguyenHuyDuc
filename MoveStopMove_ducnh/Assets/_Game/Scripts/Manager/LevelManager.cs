using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager ins;
    public  DynamicJoystick Joystick;
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
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        level=Instantiate(levels[0]);
    }

    public void SetCameraTarget(Character target){
        m_camera.Target=target.TF;
    }
}
