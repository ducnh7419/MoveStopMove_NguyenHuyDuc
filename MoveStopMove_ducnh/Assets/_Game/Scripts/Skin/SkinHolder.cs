using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinHolder : GameUnit
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Despawn(){
        SimplePool.Despawn(this);
    }
}
