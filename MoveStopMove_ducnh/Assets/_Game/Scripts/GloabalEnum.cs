using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GloabalEnum
{
    public enum WeaponEnum
    {
        None = 0,
        Hammer = PoolType.Hammer,
        Axe = PoolType.Axe,
        Boomerang = PoolType.Boomerang,
        Candy = PoolType.Candy
    }

    public enum EPants
    {
        None = 0,
        Batman = 1,
        Chambi = 2
    }

    public enum EHairs{
        None=0,
        Arrow=PoolType.Arrow,
        Cowboy=PoolType.Cowboy,
        Crown=PoolType.Crown,
        Ear=PoolType.Ear,
        Hat=PoolType.Hat,
        Hat_Cap=PoolType.Hat_Cap,
        Hat_Yellow=PoolType.Hat_Yellow,
        Headphone=PoolType.Headphone,
        Horn=PoolType.Horn,
        Mustache=PoolType.Mustache
    }

    public enum EShields{
        None=0,
        Shield1=PoolType.Shield1,
        Shield2=PoolType.Shield2,
    }

    public enum EFullSets{
        None=0,
        Devil=PoolType.Devil,
        Angel=PoolType.Angel
    }

    public enum EWeapon
    {
        None = 0,
        Hammer=1,
        Candy=2,
        Boomerang=3
    }

    public enum EBuffType{
        None=0,
        RangeBuff=1,
        SpeedBuff=2,
        GoldBuff=3,
        AttackSpeed=4
    }

    public enum EItemType{
        None=0,
        Hair=1,
        Pant=2,
        Shield=3,
        FullSet=4
    }

    public enum EItemState{
        None=0,
        NotPurchased=1,
        NotEquipped=2,
        Equipped=3
    }

    public enum EGameRule{
        None=0,
        Range=1,
        Gold=2
    }

    public enum EGameResult{
        None=0,
        Win=1,
        Lose=2
    }

    public enum ESound{
        None=0,
        LOSE=1,
        ATTACK=2,
        CLICK=3,
        VICTORY=4,
        PLAYER_DEATH=5,
        WEAPON_HIT=6,
        SIZE_UP=7
    }

    public enum EPowerUps{
        None=0,
        Ulti=1
    }

    public enum EBackgroundMusic{
        None=0,
        InGame=1,
        MainMenu=2
    }
    
}
