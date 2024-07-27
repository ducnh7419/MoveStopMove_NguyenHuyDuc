

using System;
using GloabalEnum;

public interface ICharacterSkin
{
   public void InitRandomItem();

   public void InitSkin(int id);

   public void SetItemBuff(int id);

   public bool Exists();

   public Tuple<EBuffType,float> GetItemBuff();



}
