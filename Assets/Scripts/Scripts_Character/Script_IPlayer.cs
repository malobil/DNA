using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Script_IPlayer : MonoBehaviour
{
    #region Movement variables

    public float f_move_speed_horizontal = 10f ;
    public float f_move_speed_vertical = 10f ;

    private Rigidbody2D player_rb;

    #endregion

    #region Animation variable

    private Animator player_animator;

    #endregion

    private void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        player_animator = GetComponent<Animator>();
    }

    public virtual void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        float f_horizontal_move = Input.GetAxis("Horizontal") * f_move_speed_horizontal;
        float f_vertical_move = Input.GetAxis("Vertical") * f_move_speed_vertical;

        player_rb.velocity = new Vector2(f_horizontal_move, f_vertical_move);

        if(f_horizontal_move == 0 && f_vertical_move ==0)
        {
            player_animator.SetBool("Idle", true);
        }
        else
        {
            player_animator.SetBool("Idle", false);
        }

        player_animator.SetFloat("horizontal_movement", f_horizontal_move);
        player_animator.SetFloat("vertical_movement", f_vertical_move);
    }
}
