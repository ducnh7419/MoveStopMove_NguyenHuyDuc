using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
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
