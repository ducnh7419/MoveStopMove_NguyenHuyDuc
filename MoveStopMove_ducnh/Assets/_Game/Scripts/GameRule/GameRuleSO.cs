
using UnityEngine;


[CreateAssetMenu(fileName = "GameRule", menuName = "ScriptableObjects/GameRuleSO", order = 5)]
public class GameRuleSO : ScriptableObject
{
    public float ScalePerScore;
    public int BonusPointPerKill;

    public float ImmortalTime;

}
