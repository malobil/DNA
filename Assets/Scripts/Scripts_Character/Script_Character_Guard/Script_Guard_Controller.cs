using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Guard_Controller : MonoBehaviour
{
    public enum GuardBaseDirection { Up,Left,Right,Down}

    public GuardBaseDirection guard_idle_direction;
    public Transform obj_guard_sign_of_view;

    private Animator anim_guard_animator;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Objet") && Script_Player.Instance.ItemIsThrowing())
        {
            MoveToTarget(other.transform);
        }
    }*/

    private void Start()
    {
        anim_guard_animator = GetComponent<Animator>();
        SetGuardDirection(guard_idle_direction.ToString(""));
    }

    private void SetGuardDirection(string direction)
    {
       /* anim_guard_animator.SetTrigger(direction);

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
        }*/
        
    }

    public void MoveToTarget(Transform t_target_position)
    {

    }
}
