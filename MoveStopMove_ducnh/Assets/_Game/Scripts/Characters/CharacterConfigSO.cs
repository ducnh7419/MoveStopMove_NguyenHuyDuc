using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterConfig", menuName = "ScriptableObjects/CharacterConfig", order = 2)]
public class CharacterConfigSO : ScriptableObject
{
    public float Speed;
    public Vector3 DefaultAtackAreaScale;
}
