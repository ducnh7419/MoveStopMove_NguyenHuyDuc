using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class FullSet : GameUnit
{
    public SkinnedMeshRenderer MrBody;
    public SkinnedMeshRenderer MrPant;
    public SkinHolder HairHolder;
    public SkinHolder WingHolder;
    public SkinHolder TailHolder;


    public void Setup(SkinnedMeshRenderer mrBody,SkinnedMeshRenderer mrPant)
    {
        mrBody.material = MrBody.sharedMaterial;
        mrPant.material= MrPant.sharedMaterial;
        // if (HairHolder != null)
        //     SimplePool.Spawn<SkinHolder>(HairHolder,parent);
        // if (WingHolder != null)
        //     SimplePool.Spawn<SkinHolder>(WingHolder,parent);
        // if (TailHolder != null)
        //     SimplePool.Spawn<SkinHolder>(TailHolder,parent);
    }

    internal void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
