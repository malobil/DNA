using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Trigger_Door_Manager : MonoBehaviour
{

    #region Door
    public enum Doortype { Nocard, Tutocard, Bluecard, Redcard, Greencard, Yellowcard }
    public Doortype d_door_type;

    private Animator a_door_animator;
    private bool b_door_is_open = false;
    public List<GameObject> g_character_in_trigger;
    #endregion

    private void Start()
    {
        a_door_animator = GetComponent<Animator>();
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
            a_door_animator.SetTrigger("Open");
            b_door_is_open = true;
        }
    }

    public void CloseDoor()
    {
        if (b_door_is_open)
        {
            a_door_animator.SetTrigger("Close");
            b_door_is_open = false;
        }
    }
}
