using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Laser : MonoBehaviour
{
    public float f_max_range = 10f;
    public LayerMask collision_layer;
    public LineRenderer lineRenderer_component;

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(transform.position, transform.up, Color.red, f_max_range);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, f_max_range, collision_layer);
      
        if (hit.collider != null)
        {
            float f_z_distance = Vector3.Distance(transform.position, hit.point);
            lineRenderer_component.SetPosition(1, new Vector3(0,0, f_z_distance)); 
            Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            lineRenderer_component.SetPosition(1, new Vector3(0, 0, f_max_range));
        }
    }
}
