using System.Collections;
using System.Collections.Generic;
using GloabalEnum;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldDataConfig", menuName = "ScriptableObjects/ShieldDataConfigSO", order = 5)]
public class ShieldDataConfigSO : ScriptableObject
{
    [SerializeField] private List<ShieldSkin> shieldSkin;

    public List<ShieldSkin> ShieldSkin => shieldSkin;

    public ShieldSkin GetShieldSkinByEnum(ShieldEnum shieldEnum)
    {
        int index = (int)shieldEnum;
        for (int i = 0; i < ShieldSkin.Count; i++)
        {
            if (i == index)
            {
                return ShieldSkin[i];
            }
        }
        return ShieldSkin[0];
    }

    public ShieldSkin GetRandomShieldSkin()
    {
        int rdn = Random.Range(0, ShieldSkin.Count);
        return ShieldSkin[rdn];
    }
}
