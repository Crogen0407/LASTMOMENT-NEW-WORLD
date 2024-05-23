using DG.Tweening;
using UnityEngine;

public class PlayerMovement : AgentMovement
{
    public float aimingPosition;
    [HideInInspector] public Vector3 lookAngle = Vector3.zero;

    public override void HandleMoveDirection(Vector3 position)
    {
        Vector3 lookDirection = transform.forward*aimingPosition;
        
        lookAngle += new Vector3(
            -position.y * RotateSpeedY, 
            position.x * RotateSpeedX * 0.5f, 
            0) * (Time.deltaTime * ((float)CurSpeed/MaxSpeed*0.5f));

        lookAngle.z = -Mathf.Rad2Deg * position.x;
        
        //Clamp
        lookAngle.z = Mathf.Clamp(lookAngle.z, -89, 89);
        lookAngle = new Vector3(
            MathExtension.RotateClamp(lookAngle.x, -90f, 90f),
            lookAngle.y, 
            lookAngle.z);
        
        transform.DORotate(lookAngle, 0.1f);
    }
}