using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICJoystick : UICanvas
{
    [SerializeField]public DynamicJoystick dynamicJoystick;
    

    public void Enabled(bool enabled){
        this.enabled = enabled;
    }
}
