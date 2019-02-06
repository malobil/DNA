using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Script_Guard_Controller : MonoBehaviour
{
    public enum GuardBaseDirection { Up,Left,Right,Down}

    [Header("Guard direction")]
    public GuardBaseDirection guard_idle_direction;

    [Header("Sign of view")]
    public Transform obj_guard_sign_of_view;
    public Vector3 f_guard_alarm_sign_of_view;
    private Vector3 f_guard_normal_sign_of_view;
    private bool b_see_the_player = false;

    [Header("Behavior")]
    public float f_time_chase_player = 5f;
    private float f_current_time_chase_player;
    public float f_time_to_shot = 1f;
    public float f_current_time_to_shot ;

    [Header("Pathfinder")]
    public AIPath ai_path_component;
    public AIDestinationSetter ai_destination_component;
    public Patrol ai_patrol_component;


    private Animator anim_guard_animator;

    private void Start()
    {
        anim_guard_animator = GetComponent<Animator>();
        anim_guard_animator.SetTrigger(guard_idle_direction.ToString(""));
        f_guard_normal_sign_of_view = obj_guard_sign_of_view.localScale;
        SetGuardDirection();

    }

    private void Update()
    {
        if(f_current_time_chase_player >= 0 && !b_see_the_player)
        {
            f_current_time_chase_player -= Time.deltaTime;
        }
        else if (f_current_time_chase_player < 0 && !b_see_the_player)
        {
            StopChasing();
        }

        float hDistance = Mathf.Abs(ai_path_component.GetNextPosition().x - transform.position.x);
            float vDistance = Mathf.Abs(ai_path_component.GetNextPosition().y - transform.position.y);

            if (vDistance >= hDistance)
            {
                if (transform.position.y > ai_path_component.GetNextPosition().y)
                {
                    guard_idle_direction = GuardBaseDirection.Up;
                    SetGuardDirection();
                    //Debug.Log("GO UP");
                }
                else if (transform.position.y < ai_path_component.GetNextPosition().y)
                {
                    guard_idle_direction = GuardBaseDirection.Down;
                    SetGuardDirection();
                    //Debug.Log("GO DOWN");
                }
            }
            else
            {
                if (transform.position.x > ai_path_component.GetNextPosition().x)
                {
                    guard_idle_direction = GuardBaseDirection.Right;
                    SetGuardDirection();
                   // Debug.Log("GO right");
                }
                else if (transform.position.x < ai_path_component.GetNextPosition().x)
                {
                    guard_idle_direction = GuardBaseDirection.Left;
                    SetGuardDirection();
                    //Debug.Log("GO left");
                }
        }

        
    }

    private void SetGuardDirection()
    {
        switch (guard_idle_direction.ToString(""))
        {
            case "Left":
                obj_guard_sign_of_view.localRotation = Quaternion.Euler(0, 0, -90);
                break;

            case "Right":
                obj_guard_sign_of_view.localRotation = Quaternion.Euler(0, 0, 90);
                break;

            case "Up":
                obj_guard_sign_of_view.localRotation = Quaternion.Euler(0, 0, 180);
                break;

            case "Down":
                obj_guard_sign_of_view.localRotation = Quaternion.Euler(0, 0, 0);
                break;
        }
        
    }

    public void SeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Script_Player.Instance.transform.position - transform.position, 25f);
        Debug.DrawRay(transform.position, Script_Player.Instance.transform.position - transform.position, Color.red, 25f);
        Debug.Log(hit.collider.name);
        

        if(hit.collider.CompareTag("Player"))
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        f_current_time_chase_player = f_time_chase_player;
        ai_path_component.canMove = false;
        obj_guard_sign_of_view.localScale = f_guard_alarm_sign_of_view;
        b_see_the_player = true;
    }

    public void LostPlayer()
    {
        Debug.Log("LOST");
        ai_path_component.canMove = true ;
        ai_patrol_component.enabled = false;
        ai_destination_component.target = Script_Player.Instance.transform;
        ai_destination_component.enabled = true ;
        b_see_the_player = false;

    }

    public void StopChasing()
    {
        ai_destination_component.enabled = false;
        ai_path_component.canMove = true;
        ai_patrol_component.enabled = true ;
        obj_guard_sign_of_view.transform.localScale = f_guard_normal_sign_of_view;
    }
}
