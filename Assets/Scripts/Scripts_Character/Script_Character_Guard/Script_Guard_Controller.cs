using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Script_Guard_Controller : MonoBehaviour
{
    public enum GuardBaseDirection { Up,Left,Right,Down}

    public GuardBaseDirection guard_idle_direction;
    public Transform obj_guard_sign_of_view;

    public AIPath ai_path_component;

    private Animator anim_guard_animator;

    private void Start()
    {
        anim_guard_animator = GetComponent<Animator>();
        anim_guard_animator.SetTrigger(guard_idle_direction.ToString(""));
        SetGuardDirection();

    }

    private void Update()
    {
            float hDistance = Mathf.Abs(ai_path_component.GetNextPosition().x - transform.position.x);
            float vDistance = Mathf.Abs(ai_path_component.GetNextPosition().y - transform.position.y);

            if (vDistance >= hDistance)
            {
                if (transform.position.y > ai_path_component.GetNextPosition().y)
                {
                    guard_idle_direction = GuardBaseDirection.Up;
                    SetGuardDirection();
                    Debug.Log("GO UP");
                }
                else if (transform.position.y < ai_path_component.GetNextPosition().y)
                {
                    guard_idle_direction = GuardBaseDirection.Down;
                    SetGuardDirection();
                    Debug.Log("GO DOWN");
                }
            }
            else
            {
                if (transform.position.x > ai_path_component.GetNextPosition().x)
                {
                    guard_idle_direction = GuardBaseDirection.Right;
                    SetGuardDirection();
                    Debug.Log("GO right");
                }
                else if (transform.position.x < ai_path_component.GetNextPosition().x)
                {
                    guard_idle_direction = GuardBaseDirection.Left;
                    SetGuardDirection();
                    Debug.Log("GO left");
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

    public void MoveToTarget(Transform t_target_position)
    {

    }

    public void SeePlayer()
    {
        ai_path_component.canMove = false;
        Debug.Log("SEE IT");
    }
}
