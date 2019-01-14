using System;

public class Script_Note : Script_IObject
{
    public Note_Data c_note;

    public override void Interact()
    {
        Script_UI_Manager.Instance.ShowNote(c_note);
        Script_UI_Manager.Instance.PauseGame();
    }

    public override void UnInteract()
    {
        Script_UI_Manager.Instance.ResumeGame();
        Script_UI_Manager.Instance.UnShowNote();
        Destroy(gameObject);
    }
}

[Serializable]
public class Note_Data
{
    #region Note Variables

    public string s_note_title = "Test title";
    public string s_note_description = "Test description";

    #endregion
}