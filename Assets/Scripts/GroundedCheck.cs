using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
   //Grounded Raycast
    [SerializeField] private float floorDetectDistance = 1f;
    [SerializeField] private Vector2 floorDetectOffset;
    [SerializeField] private Vector2 footOffset;
    [SerializeField] private LayerMask floorDetectMask;

    public bool CheckGrounded() {
        return Utils.Boxcast(transform.position + (Vector3)floorDetectOffset, footOffset + floorDetectDistance * 0.5f * Vector2.up,
        Vector2.down, floorDetectDistance, floorDetectMask);
        //RaycastHit2D left = Utils.Raycast(transform.position + (Vector3)(floorDetectOffset + footOffset), Vector2.down, floorDetectDistance, floorDetectMask);
        //RaycastHit2D right = Utils.Raycast(transform.position + (Vector3)(floorDetectOffset - footOffset), Vector2.down, floorDetectDistance, floorDetectMask);
        //return left || right;
    }


}