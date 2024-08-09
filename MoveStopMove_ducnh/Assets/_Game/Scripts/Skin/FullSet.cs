
using GloabalEnum;
using Unity.VisualScripting;
using UnityEngine;

public class FullSet : GameUnit
{
    public EFullSets eFullSets;
    public SkinnedMeshRenderer MrBody;
    public SkinnedMeshRenderer MrPant;
    public SkinHolder HairHolderPrefab;
    public SkinHolder leftHandHolderPrefab;
    private SkinHolder hairHolder;
    private SkinHolder leftHandHolder;


    public void Setup(SkinnedMeshRenderer mrBody,SkinnedMeshRenderer mrPant,Transform hairHolderTF,Transform leftHandHolderTF)
    {
        mrBody.material = MrBody.sharedMaterial;
        mrPant.material= MrPant.sharedMaterial;
        if (HairHolderPrefab != null)
            hairHolder=SimplePool.Spawn<SkinHolder>(HairHolderPrefab,hairHolderTF);
        if (leftHandHolderPrefab != null){
            leftHandHolder=SimplePool.Spawn<SkinHolder>(leftHandHolderPrefab,leftHandHolderTF);
        }
    }    

    internal void OnDespawn()
    {
        if (hairHolder != null)
            hairHolder.Despawn();
        if(leftHandHolder!=null){
            leftHandHolder.Despawn();
        }
        SimplePool.Despawn(this);
        
    }
}
