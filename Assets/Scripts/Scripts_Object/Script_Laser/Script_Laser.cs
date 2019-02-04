using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Laser : MonoBehaviour
{
    public float f_max_range = 10f;
    private LineRenderer lineRenderer_component;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer_component = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, f_max_range);


        if (hit.collider != null)
        {
            //lineRenderer_component.SetPosition(1, new Vector3(0,0,hit.transform.position.z));
            Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            lineRenderer_component.SetPosition(1, new Vector3(0, 0, f_max_range));
        }
    }
}
