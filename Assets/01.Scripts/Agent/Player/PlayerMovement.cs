using DG.Tweening;
using UnityEngine;

public class PlayerMovement : AgentMovement
{
    [HideInInspector] public Vector3 lookAngle = Vector3.zero;

    
    public override void HandleMoveDirection(Vector3 Delta)
    {
        base.HandleMoveDirection(Delta);
        lookAngle += new Vector3(
            -Delta.y * RotateSpeedY, 
            Delta.x * RotateSpeedX * 0.5f, 
            0) * (Time.deltaTime * ((float)CurSpeed/MaxSpeed*0.5f));

        lookAngle.z = -Mathf.Rad2Deg * Delta.x;
        
        //Clamp
        lookAngle.z = Mathf.Clamp(lookAngle.z, -89, 89);
        lookAngle = 
            new Vector3(
                MathExtension.RotateClamp(lookAngle.x, -90f, 90f),
                lookAngle.y, 
                lookAngle.z);
        
        transform.DORotate(lookAngle, 0.1f);
    }
}