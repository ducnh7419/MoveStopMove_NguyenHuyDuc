using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;

[CreateAssetMenu(fileName = "PantDataConfig", menuName = "ScriptableObjects/PantDataConfig", order = 4)]
public class PantDataConfig : ScriptableObject
{
    [SerializeField] private List<PantSkin> pantSkin;

    public List<PantSkin> PantSkin => pantSkin;

    public PantSkin GetPantsSkinByEnum(PantSkinEnum pantSkinEnum)
    {
        int index = (int)pantSkinEnum;
        for (int i = 0; i < PantSkin.Count; i++)
        {
            if (i == index)
            {
                return PantSkin[i];
            }
        }
        return PantSkin[0];
    }

    public PantSkin GetRandomPantSkin()
    {
        int rdn = Random.Range(0, PantSkin.Count);
        return PantSkin[rdn];
    }
}
