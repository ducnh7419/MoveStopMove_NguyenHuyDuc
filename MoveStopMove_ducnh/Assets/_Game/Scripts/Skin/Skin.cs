
using GloabalEnum;
using UnityEngine;

public class Skin : GameUnit
{

    public FullSet FullSet;
    public SkinnedMeshRenderer MrPant;

    public void OnDespawn(){
        SimplePool.Despawn(this);
    }

    public void SetScale(Vector3 scale){
        TF.localScale = scale;
    }
}
