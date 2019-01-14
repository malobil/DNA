using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Note_Data
{
    #region Note Variables

    public string s_note_title = "Test title";
    public string s_note_description = "Test description" ;

    #endregion
}

public class Script_Note : Script_IObject
{
    public Note_Data c_note;

    public override void Interact()
    {
        Script_UI_Manager.Instance.ShowNote(c_note);
    }
}
