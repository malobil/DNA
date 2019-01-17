﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Trigger_Door_Manager : MonoBehaviour
{
    public static Script_Trigger_Door_Manager Instance { get; private set; }

    #region Door
    public enum Doortype { Nocard, Tutocard, Bluecard, Redcard, Greencard, Yellowcard }
    public Doortype d_door_type;

    public GameObject g_door_to_activate;
    public float f_speed_of_animation = 0.5f;
    private bool b_door_is_open = false;
    public List<GameObject> g_character_in_trigger;
    #endregion

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

    public void VerifyCard(GameObject g_character_detected)
    {
        switch (d_door_type.ToString(""))
        {
            case "Nocard":
                AddCharacterInList(g_character_detected);
                break;

            case "Tutocard":
                Debug.Log("Tuto");
                break;

            case "Bluecard":
                Debug.Log("Blue");
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
        OpenDoor();
    }

    public void RemoveCharacterInList(GameObject g_character_exit)
    {
        g_character_in_trigger.Remove(g_character_exit);
        if(g_character_in_trigger.Count == 0)
        {
            b_door_is_open = true;
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        if (!b_door_is_open)
        {
            g_door_to_activate.GetComponent<Animator>().Play("Open_Door", 0, f_speed_of_animation);
            b_door_is_open = true;
        }
    }

    public void CloseDoor()
    {
        if (b_door_is_open)
        {
            g_door_to_activate.GetComponent<Animator>().Play("Close_Door", 0, f_speed_of_animation);
            b_door_is_open = false;
        }
    }
}
