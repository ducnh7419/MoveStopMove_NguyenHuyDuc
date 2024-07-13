using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
   private static UserDataManager ins;
   public static UserDataManager Ins=>ins;
   private Player player;
   public Player Player { get => player; set => player = value; }


    internal bool CheckPurchasedItem()
    {
        throw new NotImplementedException();
    }
    
}
