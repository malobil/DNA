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

    #region Interaction variables

    public Transform t_interaction_holder_trigger;
    public List<Script_IObject> list_interactible_objects;
    public Script_IObject obj_current_target;

    #endregion

    #region Animation variable

    private Animator a_player_animator;

    #endregion

    private void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        a_player_animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.A) && obj_current_target != null)
        {
            Interaction();
        }
    }

    public virtual void Interaction()
    {
        obj_current_target.Interact();
    }

    public virtual void Move()
    {
        float f_horizontal_move = Input.GetAxis("Horizontal") * f_move_speed_horizontal;
        float f_vertical_move = Input.GetAxis("Vertical") * f_move_speed_vertical;

        player_rb.velocity = new Vector2(f_horizontal_move, f_vertical_move);

        if(f_horizontal_move == 0 && f_vertical_move ==0)
        {
            a_player_animator.SetBool("Idle", true);
        }
        else
        {
            a_player_animator.SetBool("Idle", false);
        }

        #region Interaction rotation

        if (f_vertical_move >= 0.5f)
        {
            t_interaction_holder_trigger.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if(f_vertical_move <= -0.5f)
        {
            t_interaction_holder_trigger.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(f_vertical_move < 1 || f_vertical_move > -1)
        {
            
            if(f_horizontal_move > 0)
            {
                t_interaction_holder_trigger.localRotation = Quaternion.Euler(0, 0, 90);
            }
            else if (f_horizontal_move < 0)
            {
                t_interaction_holder_trigger.localRotation = Quaternion.Euler(0, 0, -90);
            }

        }

        #endregion Interaction 


        a_player_animator.SetFloat("horizontal_movement", f_horizontal_move);
        a_player_animator.SetFloat("vertical_movement", f_vertical_move);
    }

    public void AddInteractibleObject(Script_IObject obj_interactible_object)
    {
        list_interactible_objects.Add(obj_interactible_object);
    }

    public void RemoveInteractibleObject(Script_IObject obj_interactible_object)
    {
        list_interactible_objects.Remove(obj_interactible_object);
    }
}
