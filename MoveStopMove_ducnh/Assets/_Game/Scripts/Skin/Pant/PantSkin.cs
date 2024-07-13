using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PantSkin : Skin
{
    [SerializeField] private MeshRenderer renderers;
    public Material Material=>renderers.material;
    public float MoveSpeedBuff;

}
