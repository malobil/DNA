using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Laser : MonoBehaviour
{
    public float f_max_range = 10f;
    public LayerMask collision_layer;
    public LineRenderer lineRenderer_component;
    private BoxCollider2D laser_collider;


    private void Start()
    {
        laser_collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(transform.position, transform.up, Color.red, f_max_range);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, f_max_range, collision_layer);
      
        if (hit.collider != null)
        {
            float f_z_distance = Vector3.Distance(transform.position, hit.point);
            lineRenderer_component.SetPosition(1, new Vector3(0,0, f_z_distance));
            laser_collider.size = new Vector2 (0.1f,f_z_distance) ;
            laser_collider.offset = new Vector2(0f, laser_collider.size.y/2);
            Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            lineRenderer_component.SetPosition(1, new Vector3(0, 0, f_max_range));
            laser_collider.size = new Vector2(0.1f, f_max_range);
            laser_collider.offset = new Vector2(0f, laser_collider.size.y / 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Vector2 difference =  Script_Player.Instance.transform.position - transform.position;
            Script_Player.Instance.Knockback();
        }
    }
}
