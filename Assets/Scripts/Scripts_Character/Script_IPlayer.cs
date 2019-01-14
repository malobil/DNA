using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Script_IPlayer : MonoBehaviour
{
    public float f_move_speed_horizontal = 10f ;
    public float f_move_speed_vertical = 10f ;

    #region Private variables

    private Rigidbody2D player_rb;

    #endregion

    private void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
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

    }
}
