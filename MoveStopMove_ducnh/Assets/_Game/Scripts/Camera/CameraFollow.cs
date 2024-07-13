using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float smoothSpeed;
    [SerializeField] public Vector3 Offset;

    public Transform Target { get => target; set => target = value; }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null){
            Vector3 desiredPosition = target.position + Offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void SetCameraPositionAndRotation(Vector3 offset,Quaternion rot){
        this.Offset=offset;
        transform.rotation = rot;
    }

    public void SetCameraTarget(Character target){
        Target=target.TF;
    }
}