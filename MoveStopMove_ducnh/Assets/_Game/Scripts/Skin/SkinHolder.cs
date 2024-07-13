using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinHolder : MonoBehaviour
{
    public void SetSkin(Skin skin)
    {
        Instantiate(skin, this.transform);
        
    }
}
