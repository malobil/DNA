using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Script_IPlayer : MonoBehaviour
{
    #region Movement variables

    public float f_move_speed_horizontal = 10f ;
    public float f_move_speed_vertical = 10f ;
    private bool b_can_move = true;

    private Rigidbody2D player_rb;

    #endregion

    #region Interaction variables

    public Transform t_interaction_holder_trigger;
    private List<Script_IObject> list_interactible_objects = new List<Script_IObject>() ;
    public Script_IObject obj_current_target;
    private bool b_is_interacting = false;

    #endregion

    #region Animation variable

    private Animator a_player_animator;

    #endregion

    public Script_IObject obj_current_object_hold;

    private void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        a_player_animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        if (Input.GetButtonDown("Hold") && obj_current_target != null && !b_is_interacting)
        {
            if(obj_current_target.b_can_be_hold)
            {
                Hold();
            }
            else if (obj_current_target.b_special_case)
            {
                SpecialInteraction();
                Debug.Log("DDD");
            }
        }

        if(Input.GetButtonDown("UnInteract") && b_is_interacting)
        {
            UnInteract();
        }

        if(Input.GetButtonDown("Attack") && !b_is_interacting)
        {
            if(obj_current_object_hold != null)
            {
                // Use the interaction of the object
                Interact();
            }
            else
            {
                Debug.Log("ATTACK");
            }
        }

        if (Input.GetButtonDown("Throw") && obj_current_object_hold != null && !b_is_interacting)
        {
                Throw();
        }

        Move();
    }

    #region Interact

    public virtual void SpecialInteraction()
    {
            obj_current_target.Interact();
            Debug.Log("test");
            b_is_interacting = true;
            b_can_move = false;
    }
  
    public virtual void UnInteract()
    {
            obj_current_target.UnInteract();
            b_is_interacting = false;
            b_can_move = true;
    }

    public virtual void Interact()
    {
        obj_current_object_hold.Interact();
    }
    #endregion

    #region Movement
    public virtual void Move()
    {
        if(!b_can_move)
        {
            player_rb.velocity = new Vector2(0, 0);
            return;
        }

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
    #endregion

    public void AddInteractibleObject(Script_IObject obj_interactible_object)
    {
        list_interactible_objects.Add(obj_interactible_object);

        if(obj_current_target == null)
        {
            obj_current_target = obj_interactible_object;
        }
    }

    public void RemoveInteractibleObject(Script_IObject obj_interactible_object)
    {
        list_interactible_objects.Remove(obj_interactible_object);

        if (obj_interactible_object == obj_current_target)
        {
            obj_current_target = null;

            if(list_interactible_objects.Count > 0)
            {
                obj_current_target = list_interactible_objects[0];
            } 
        }
    }

    private void Hold()
    {
        obj_current_object_hold = obj_current_target;
        list_interactible_objects.Remove(obj_current_target);
        obj_current_target = null;
        obj_current_object_hold.transform.SetParent(transform);
        obj_current_object_hold.gameObject.SetActive(false);
        obj_current_object_hold.transform.localPosition = new Vector2(0, 0); 
        Script_UI_Manager.Instance.NewObjectHold(obj_current_object_hold.GetComponent<SpriteRenderer>().sprite);
    }

    private void Throw()
    {
        
        obj_current_object_hold.transform.SetParent(null);
        obj_current_object_hold.gameObject.SetActive(true);
        Script_UI_Manager.Instance.NewObjectHold(null);
        obj_current_object_hold = null;
    }
}
