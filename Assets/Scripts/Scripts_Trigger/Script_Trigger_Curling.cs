using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Trigger_Curling : MonoBehaviour
{
    public Vector2 v_limit_red;
    public Vector2 v_limit_orange;
    public Vector2 v_limit_yellow;

    private int i_score;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || !other.gameObject.CompareTag("Ennemy"))
        {
            if (other.gameObject.GetComponent<Rigidbody2D>() && other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0)
            {
                EvaluateDistance(other.transform);
                Debug.Log("Circle");
            }
        }
    }

    public void EvaluateDistance(Transform t_item_in_target)
    {
        float f_red_target = Vector2.Distance(new Vector2(0f,0f) , v_limit_red);
        float f_orange_target = Vector2.Distance(v_limit_red, v_limit_orange);
        float f_yellow_target = Vector2.Distance(v_limit_orange, v_limit_yellow);

        if(Vector2.Distance(new Vector2(0f, 0f), t_item_in_target.position) < f_red_target)
        {
            UpdateScore(100);
        }

        if (Vector2.Distance(v_limit_red, t_item_in_target.position) < f_orange_target)
        {
            UpdateScore(50);
        }

        if (Vector2.Distance(v_limit_orange, t_item_in_target.position) < f_yellow_target)
        {
            UpdateScore(20);
        }
    }

    public void UpdateScore(int i_score_to_add)
    {
        i_score = i_score + i_score_to_add;
    }

}
