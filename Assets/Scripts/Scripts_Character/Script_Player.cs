﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

[RequireComponent(typeof(Rigidbody2D))]
public class Script_Player : MonoBehaviour
{
    public static Script_Player Instance { get; private set; }

    #region Movement variables
    [Header("Movement")]
    public float f_move_speed_horizontal = 10f ;
    public float f_move_speed_vertical = 10f ;

    private bool b_can_move = true;

    [Header("Knockback")]
    public float f_knockback_force = 10f;
    public float f_knockback_duration = 10f;
   
    private bool b_is_knockback = false;
    private float f_current_knockback_duration = 0f;

    private Rigidbody2D player_rb;

    #endregion

    #region Interaction variables

    [Header("Interaction")]
    public Transform t_interaction_holder_trigger;

    private List<GameObject> list_interactible_objects = new List<GameObject>() ;
    private GameObject obj_current_target;
    private bool b_can_interact = true ;
    private GameObject obj_current_object_hold;

    #endregion

    #region Throw variables

    [Header("Throw")]
    public float f_max_throw_force = 10f;
    public float f_time_to_max_force = 5f;
    public UnityEngine.UI.Image img_throw_feedback;
    public Transform t_throw_pivot_point;
    private float f_current_force = 0f;
    private bool b_can_throw = true;

    #endregion

    #region Animation variable

    private Animator a_player_animator;

    #endregion

    #region Distort variable

    public int playerLevel = 0;
    private bool b_can_Distort = true ;
    private bool b_have_use_distort = false;
    private GameObject g_current_distortable_target;

    #endregion

    #region Alter variable

    private bool b_can_alter = true;
    private bool b_have_use_alter = false ;
    private GameObject g_current_alterable_target;
    private GameObject g_last_transformation;

    #endregion

    private bool b_can_use_item = true;

    private List<Vector2> v_list_last_position = new List<Vector2>();

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        a_player_animator = GetComponent<Animator>();
    }

    public void Update()
    {
        Move();

        if (Input.GetButtonDown("Pause"))
        {
            Script_Game_Manager.Instance.TogglePause();

            if(!Script_Game_Manager.Instance.GetGameState())
            {
                CheckInputAfterPause();
            }
        }

        if(b_is_knockback && !Script_Game_Manager.Instance.GetGameState())
        {
            KnockbackEffect();
        }

        if (!Script_Game_Manager.Instance.GetGameState() && !b_is_knockback)
        {
            if (Input.GetButtonDown("Interact") && obj_current_target != null && b_can_interact)
            {
                Interact();
            }

            if (Input.GetButtonUp("Throw") && obj_current_object_hold != null && b_can_throw)
            {
                Throw();
            }

            if (Input.GetButton("Throw") && obj_current_object_hold != null && b_can_throw)
            {
                AddForceToThrow();
            }

            if(Input.GetButtonDown("Use") && obj_current_object_hold != null && b_can_use_item)
            {
                UseItem();
            }

            if(Input.GetButton("Distort") && b_can_Distort && !b_have_use_distort)
            {
                Distort();
            }

            if (Input.GetButtonDown("Distort") && b_can_Distort)
            {
                StartDistort();
            }

            if (Input.GetButtonUp("Distort") && b_can_Distort)
            {
                StopDistort();
            }

            if (Input.GetButton("Alter") && b_can_alter && !b_have_use_alter)
            {
                Alter();
            }

            if (Input.GetButtonDown("Alter") && b_can_alter)
            {
                StartAlter();
            }

            if (Input.GetButtonUp("Alter") && b_can_alter)
            {
                StopAlter();
            }
        }
    }
   
    #region Interact

    public void Interact()
    {
        if (obj_current_target.GetComponent<Script_Interactable>())
        {
            obj_current_target.GetComponent<Script_Interactable>().Interact(this);
        }
    }

    public void AddInteractibleObject(GameObject obj_interactible_object)
    {
        list_interactible_objects.Add(obj_interactible_object);

        if (obj_current_target == null)
        {
            SelectTarget(obj_interactible_object);
        }
    }

    public void SelectTarget(GameObject target)
    {
        obj_current_target = target;

        if (target != null)
        {
            obj_current_target.GetComponent<Outline>().EnableOutline();
        }
    }

    public void RemoveInteractibleObject(GameObject obj_interactible_object)
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

    private void AllowInteract()
    {
        b_can_interact = true;
    }

    private void DisableInteract()
    {
        b_can_interact = false;
    }

    #endregion

    #region Movement
    public  void Move()
    {
        if(!b_can_move || b_is_knockback || Script_Game_Manager.Instance.GetGameState())
        {
            return;
        }

        float f_horizontal_move = Input.GetAxis("Horizontal") * f_move_speed_horizontal;
        float f_vertical_move = Input.GetAxis("Vertical") * f_move_speed_vertical;

        float f_horizontal_move_raw = Input.GetAxisRaw("Horizontal");
        float f_vertical_move_raw = Input.GetAxisRaw("Vertical");

        player_rb.velocity = new Vector2(f_horizontal_move, f_vertical_move);

        if(f_horizontal_move == 0 && f_vertical_move == 0)
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

    void Knockback(Vector3 direction)
    {
        player_rb.AddForce(direction * f_knockback_force, ForceMode2D.Impulse);
        b_is_knockback = true;
    }

    void KnockbackEffect() // call if the player is knockback in update function
    {
        if (f_current_knockback_duration < f_knockback_duration)
        {
            f_current_knockback_duration += Time.deltaTime;
        }
        else
        {
            b_is_knockback = false;
            f_current_knockback_duration = 0f;
        }
    }

    private void AllowMove()
    {
        b_can_move = true;
    }

    private void DisableMove()
    {
        player_rb.velocity = Vector3.zero;
        b_can_move = false;
    }
    #endregion

    #region Hold & Throw

    public void Hold()
    {
        if(obj_current_object_hold == null)
        {
            obj_current_object_hold = obj_current_target.gameObject;
            obj_current_object_hold.transform.SetParent(transform);
            obj_current_object_hold.gameObject.SetActive(false);
            obj_current_object_hold.transform.localPosition = new Vector2(0, 0);
            Script_UI_Manager.Instance.NewObjectHold(obj_current_object_hold.GetComponent<SpriteRenderer>().sprite); // UI
            RemoveInteractibleObject(obj_current_target); // Remove object from the list
            SelectTarget(null); // Remove the target
        }
    }

    public void DestroyHoldObject()
    {
        Destroy(obj_current_object_hold);
        Script_UI_Manager.Instance.NewObjectHold(null);
    }

    private void AddForceToThrow()
    {
        DisableDistort();
        DisableAlter();
        DisableUse();
        //DisableInteract()
        if(f_current_force < f_max_throw_force)
        {
            f_current_force += f_max_throw_force / f_time_to_max_force * Time.deltaTime;
            img_throw_feedback.fillAmount = f_current_force/f_max_throw_force ; 
        }

        /* Direction */

        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        t_throw_pivot_point.transform.up = -direction;
    }

    private void Throw()
    {
        obj_current_object_hold.transform.SetParent(null);
        obj_current_object_hold.gameObject.SetActive(true);
        obj_current_object_hold.GetComponent<Rigidbody2D>().AddForce(-t_throw_pivot_point.transform.up * f_current_force, ForceMode2D.Impulse);
        Script_UI_Manager.Instance.NewObjectHold(null);
        obj_current_object_hold = null;
        f_current_force = 0f;
        img_throw_feedback.fillAmount = 0;
        AllowDistort();
        AllowAlter();
        AllowUse();
    }

    private void AllowThrow()
    {
        b_can_throw = true;
    }

    private void DisableThrow()
    {
        b_can_throw = false;
    }

    #endregion

    #region Distort

    public void StartDistort()
    {
        b_have_use_distort = false;
        DisableMove();
        DisableThrow();
        DisableInteract();
        DisableAlter();
        if (obj_current_target != null && obj_current_target.GetComponent<Script_Distortable>())
        {
            g_current_distortable_target = obj_current_target ;
        }
    }

    private void Distort() // call when the right mouse is hold
    {
        a_player_animator.SetBool("Distort", true);
    }

    public void StopDistort() // is use at end of Distort animation
    {
        a_player_animator.SetBool("Distort", false);
        AllowMove();
        AllowThrow();
        AllowInteract();
        AllowAlter();
        
    }

    public void CheckDistort()
    {
        if (g_current_distortable_target != null && g_current_distortable_target == obj_current_target)
        {
            if(playerLevel >= g_current_distortable_target.GetComponent<Script_Distortable>().GetScriptableItem().i_item_level)
            {
                Script_Collection.Instance.AddItemToCollection(g_current_distortable_target.GetComponent<Script_Distortable>().GetScriptableItem());
                Destroy(g_current_distortable_target);
            }
            else
            {
                Vector3 ejectDirection = transform.position - g_current_distortable_target.transform.position;
                ejectDirection = ejectDirection.normalized;
                Knockback(ejectDirection);
            }
        }

        b_have_use_distort = true; // to stop the animation if you still hold mouse clic
    }

    public void AllowDistort()
    {
        b_can_Distort = true;
    }

    public void DisableDistort()
    {
        b_can_Distort = false;
    }

    #endregion

    #region Alter

    public void StartAlter()
    {
        b_have_use_alter = false;
        DisableMove();
        DisableThrow();
        DisableInteract();
        DisableDistort();

        if (obj_current_target != null && obj_current_target.GetComponent<Script_Alterable>())
        {
            g_current_alterable_target = obj_current_target;
        }
    }

    private void Alter() // call when the left mouse is hold
    {
        a_player_animator.SetBool("Alter", true);
    }

    public void StopAlter() // is use at end of Alter animation
    {
        a_player_animator.SetBool("Alter", false);
        AllowMove();
        AllowThrow();
        AllowInteract();
        AllowDistort();
    }

    public void CheckAlter()
    {
        if (g_current_alterable_target != null && g_current_alterable_target == obj_current_target)
        {
            if (playerLevel >= g_current_alterable_target.GetComponent<Script_Alterable>().GetScriptableItem().i_item_level)
            {
                Script_Game_Manager.Instance.SetTimePause();
                Script_UI_Manager.Instance.OpenTransformationChoice(g_current_alterable_target.GetComponent<Script_Alterable>().GetScriptableItem());
            }
            else
            {
                Vector3 ejectDirection = transform.position - g_current_alterable_target.transform.position;
                ejectDirection = ejectDirection.normalized;
                Knockback(ejectDirection);
            }
        }

        b_have_use_alter = true; // to stop the animation if you still hold mouse clic
    }

    public void AlterTarget(Script_Scriptable_Item transformation_choose)
    {
        GameObject transformation = Instantiate(transformation_choose.g_item_prefab, g_current_alterable_target.transform.position,Quaternion.identity);
        g_last_transformation = transformation;
        Destroy(g_current_alterable_target);
        Script_Game_Manager.Instance.SetTimeResume();
        Script_UI_Manager.Instance.HideAllMenu();
    }

    

    public void AllowAlter()
    {
        b_can_alter = true;
    }

    public void DisableAlter()
    {
        b_can_alter = false;
    }

    #endregion

    #region Use item

    private void UseItem()
    {
        obj_current_object_hold.GetComponent<Script_IObject>().Use(obj_current_target);
    }

    public void AllowUse()
    {
        b_can_use_item = true;
    }

    public void DisableUse()
    {
        b_can_use_item = false;
    }

    #endregion

    #region Debug / other

    public void CheckInputAfterPause() // Check if your still holding or not Alter/Distort or Throw button, if not stop them
    {
        if (!Input.GetButton("Throw") && obj_current_object_hold != null && b_can_throw && f_current_force > 0)
        {
            Throw();
        }

        if (!Input.GetButton("Distort") && b_can_Distort)
        {
            StopDistort();
        }

        if (!Input.GetButton("Alter") && b_can_alter)
        {
            StopAlter();
        }
    }

    public void GoOutOfWall() // Use to set player out of a wall
    {
        if (g_last_transformation.GetComponent<Script_Alter_Wall_Security>())
        {
            transform.position = g_last_transformation.GetComponent<Script_Alter_Wall_Security>().CheckNearPoint();
        }
    }

    public int GetPlayerLevel() // return the player level
    {
        return playerLevel;
    }

    #endregion
}
