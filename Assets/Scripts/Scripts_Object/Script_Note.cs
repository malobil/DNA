using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Data
{
    #region Note Variables

    public string s_note_title;
    public string s_note_description;

    #endregion
}

public class Script_Note : Script_IObject
{
    Note_Data c_note;

    public override void Interact()
    {
        Script_UI_Manager.Instance.ShowNote(c_note);
    }
}
