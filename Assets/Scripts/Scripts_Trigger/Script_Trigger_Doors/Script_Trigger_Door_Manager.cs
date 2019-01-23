﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Trigger_Door_Manager : MonoBehaviour
{

    #region Door
    public enum Doortype { Nocard, BlockedDoor, Bluecard, Redcard, Greencard, Yellowcard }

    public Doortype d_door_type;
    private Animator a_door_animator;
    private bool b_door_is_open = false;
    public List<GameObject> g_character_in_trigger;
    #endregion

    private void Start()
    {
        a_door_animator = GetComponent<Animator>();
    }

    public void VerifyCard()
    {
        switch (d_door_type.ToString(""))
        {
            case "Nocard":
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                OpenDoor();
                break;

            case "BlockedDoor":
                Debug.Log("BLOCKED DOOR");
                break;

            case "Bluecard":
                BlueCard();
                break;

            case "Redcard":
                Debug.Log("Red");
                break;

            case "Greencard":
                Debug.Log("Green");
                break;

            case "Yellowcard":
                Debug.Log("Yellow");
                break;
        }
    }

    public void AddCharacterInList(GameObject g_character_enter)
    {
        g_character_in_trigger.Add(g_character_enter);

        if(g_character_enter.CompareTag("Player"))
        {
            VerifyCard();
        }
    }

    public void RemoveCharacterInList(GameObject g_character_exit)
    {
        g_character_in_trigger.Remove(g_character_exit);

        if(g_character_in_trigger.Count == 0 && b_door_is_open)
        {
            CloseDoor();
        }
    }

    private void BlueCard()
    {
        if(Script_Player.Instance.CheckBlueCard())
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (!b_door_is_open)
        {
            a_door_animator.SetTrigger("Open");
            b_door_is_open = true;
        }
    }

    public void CloseDoor()
    {
            a_door_animator.SetTrigger("Close");
            b_door_is_open = false;
    }

    public void ActivateDoor()
    {
        d_door_type = Doortype.Nocard;
        VerifyCard();
    }
}
