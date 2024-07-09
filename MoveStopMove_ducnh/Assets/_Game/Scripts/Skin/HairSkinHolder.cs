using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairSkinHolder : MonoBehaviour
{
    public void SetHairSkin(Skin skin)
    {
        Instantiate(skin, this.transform);
        
    }
}
