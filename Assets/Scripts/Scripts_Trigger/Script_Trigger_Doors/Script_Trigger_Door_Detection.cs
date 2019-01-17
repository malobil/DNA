using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Trigger_Door_Detection : Script_ITrigger
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ennemy"))
        {
            Script_Trigger_Door_Manager.Instance.VerifyCard(other.gameObject);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ennemy"))
        {
            Script_Trigger_Door_Manager.Instance.RemoveCharacterInList(other.gameObject);
        }
    }
}
