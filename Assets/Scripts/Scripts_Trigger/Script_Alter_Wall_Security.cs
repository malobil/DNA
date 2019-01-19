using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Alter_Wall_Security : MonoBehaviour
{
    public Vector3 CheckNearPoint()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, -Vector2.up,1f);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up,1f);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left,1f);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, -Vector2.left,1f);
        

        if(hitDown.collider == null)
        {
            return transform.position - Vector3.up;
        }
        else if (hitUp.collider == null)
        {
            return transform.position + Vector3.up;
        }
        else if (hitLeft.collider == null)
        {
            return transform.position + Vector3.left;
        }
        else if (hitRight.collider == null)
        {
            return transform.position - Vector3.left;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
