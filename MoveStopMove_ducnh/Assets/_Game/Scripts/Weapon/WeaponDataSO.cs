using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GloabalEnum;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponDataSO", order = 3)]
public class WeaponDataSO : ScriptableObject
{
    public List<WeaponData> Weapons = new();

    public WeaponData GetWeaponDataById(int id)
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            if (Weapons[i].Id == id)
            {
                return Weapons[i];
            }
        }
        return null;
    }

    public WeaponData GetRandomWeapon(){
        int rdn=Random.Range(0, Weapons.Count);
        return Weapons[rdn];
    }

}

[System.Serializable]
public class WeaponData
{
    public int Id;
    public string Name;
    public string Description;
    public Sprite Icon;
    public EWeapon EWeapon;
    public List<WeaponSkinData> Skins;
    public int Price;
    public Weapon WeaponPrefab;

    public WeaponData()
    {
        Skins = new();
    }

    public WeaponSkinData GetWeaponSkinById(int id){
        for (int i = 0;i<Skins.Count;i++){
            if (Skins[i].Id == id)
            {
                return Skins[i];
            }
        }
        return null;
    }

    public WeaponSkinData GetRandomSkin(){
        int rdn=Random.Range(0,Skins.Count);
        return Skins[rdn];
    }
}

[System.Serializable]
public class WeaponSkinData
{
    public int Id;
    public string Name;
    public Sprite Icon;
    public WeaponSkin weaponSkinPrefab;
}



