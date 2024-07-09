using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;

[CreateAssetMenu(fileName = "PantDataConfig", menuName = "ScriptableObjects/PantDataConfig", order = 4)]
public class PantDataConfig : ScriptableObject
{
    [SerializeField] private List<Material> pantSkinMats;

    public Material GetPantsSkinByEnum(PantSkinEnum pantSkinEnum)
    {
        int index = (int)pantSkinEnum;
        for (int i = 0; i < pantSkinMats.Count; i++)
        {
            if (i == index)
            {
                return pantSkinMats[i];
            }
        }
        return pantSkinMats[0];
    }

    public Material GetRandomPantSkin()
    {
        int rdn = Random.Range(0, pantSkinMats.Count);
        return pantSkinMats[rdn];
    }
}
