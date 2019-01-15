using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

[RequireComponent(typeof(Rigidbody2D))]
public class Script_Player : MonoBehaviour
{
    #region Movement variables
    [Header("Movement")]
    public float f_move_speed_horizontal = 10f ;
    public float f_move_speed_vertical = 10f ;
    private bool b_can_move = true;

    private Rigidbody2D player_rb;

    #endregion

    #region Interaction variables

    [Header("Interaction")]
    public Transform t_interaction_holder_trigger;
    private List<Script_Interactable> list_interactible_objects = new List<Script_Interactable>() ;
    private Script_Interactable obj_current_target;
    private bool b_is_interacting = false;
    private GameObject obj_current_object_hold;

    #endregion

    #region Throw variables

    [Header("Throw")]
    public float f_max_throw_force = 10f;
    public float f_time_to_max_force = 5f;
    public UnityEngine.UI.Image img_throw_feedback;
    private float f_current_force = 0f;

    #endregion

    #region Animation variable

    private Animator a_player_animator;

    #endregion

    private void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        a_player_animator = GetComponent<Animator>();
    }

    public void Update()
    {
        Move();

        if (!Script_UI_Manager.Instance.IsInMenu())
        {
            if (Input.GetButtonDown("Interact") && obj_current_target != null)
            {
                Interact();
            }

           /* if (Input.GetButtonDown("Attack"))
            {
                if (obj_current_object_hold != null)
                {
                   
                }
                else
                {
                    Debug.Log("ATTACK");
                }
            }*/

            if (Input.GetButtonUp("Throw") && obj_current_object_hold != null)
            {
                Throw();
            }

            if (Input.GetButton("Throw") && obj_current_object_hold != null)
            {
                AddForceToThrow();
            }
        }
    }

    #region Interact

    public void Interact()
    {
        obj_current_target.Interact(this);
    }

    public void AddInteractibleObject(Script_Interactable obj_interactible_object)
    {
        list_interactible_objects.Add(obj_interactible_object);

        if (obj_current_target == null)
        {
            SelectTarget(obj_interactible_object);
        }
    }

    public void SelectTarget(Script_Interactable target)
    {
        obj_current_target = target;

        if(target !=null)
        {
            obj_current_target.GetComponent<Outline>().EnableOutline();
        }
    }

    public void RemoveInteractibleObject(Script_Interactable obj_interactible_object)
    {
        list_interactible_objects.Remove(obj_interactible_object);
        obj_interactible_object.GetComponent<Outline>().DisableOutline();

        if (obj_interactible_object == obj_current_target)
        {
            obj_current_target = null;

            if (list_interactible_objects.Count > 0)
            {
                SelectTarget(list_interactible_objects[0]);
            }
        }
    }
    #endregion

    #region Movement
    public  void Move()
    {
        if(!b_can_move)
        {
            player_rb.velocity = new Vector2(0, 0);
            return;
        }

        float f_horizontal_move = Input.GetAxis("Horizontal") * f_move_speed_horizontal;
        float f_vertical_move = Input.GetAxis("Vertical") * f_move_speed_vertical;

        float f_horizontal_move_raw = Input.GetAxisRaw("Horizontal");
        float f_vertical_move_raw = Input.GetAxisRaw("Vertical");

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

        if (f_vertical_move_raw > 0)
        {
            t_interaction_holder_trigger.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(f_vertical_move_raw < 0)
        {
            t_interaction_holder_trigger.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if(f_vertical_move_raw < 1 || f_vertical_move_raw > -1)
        {
            if(f_horizontal_move_raw > 0)
            {
                t_interaction_holder_trigger.localRotation = Quaternion.Euler(0, 0, -90);
            }
            else if (f_horizontal_move_raw < 0)
            {
                t_interaction_holder_trigger.localRotation = Quaternion.Euler(0, 0, 90);
            }

        }

        #endregion Interaction 


        a_player_animator.SetFloat("horizontal_movement", f_horizontal_move_raw);
        a_player_animator.SetFloat("vertical_movement", f_vertical_move_raw);
    }
    #endregion

    #region Hold & Throw

    public void Hold()
    {
        obj_current_object_hold = obj_current_target.gameObject ;
        obj_current_object_hold.transform.SetParent(transform);
        obj_current_object_hold.gameObject.SetActive(false);
        obj_current_object_hold.transform.localPosition = new Vector2(0, 0); 
        Script_UI_Manager.Instance.NewObjectHold(obj_current_object_hold.GetComponent<SpriteRenderer>().sprite); // UI
        RemoveInteractibleObject(obj_current_target); // Remove object from the list
        SelectTarget(null); // Remove the target
    }

    private void AddForceToThrow()
    {
        if(f_current_force < f_max_throw_force)
        {
            f_current_force += f_max_throw_force / f_time_to_max_force * Time.deltaTime;
            img_throw_feedback.fillAmount = f_current_force/f_max_throw_force ;
            
        }
    }

    private void Throw()
    {
        obj_current_object_hold.transform.SetParent(null);
        obj_current_object_hold.gameObject.SetActive(true);
        obj_current_object_hold.GetComponent<Rigidbody2D>().AddForce(t_interaction_holder_trigger.up * f_current_force, ForceMode2D.Impulse);
        Script_UI_Manager.Instance.NewObjectHold(null);
        obj_current_object_hold = null;
        f_current_force = 0f;
        img_throw_feedback.fillAmount = 0;
    }

    #endregion
}
