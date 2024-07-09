using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
   [SerializeField] MeshRenderer meshRenderer;
   [SerializeField] private Material transparentMaterial;
   [SerializeField] private Material defaultMaterial;
   private void OnCollisionEnter(Collision other) {
        if(!other.collider.CompareTag(GlobalConstants.Tag.CHARACTER)) return;
        meshRenderer.material=transparentMaterial;
   }

   private void OnCollisionExit(Collision other) {
        if(!other.collider.CompareTag(GlobalConstants.Tag.CHARACTER)) return;
        meshRenderer.material=defaultMaterial;
   }
}
