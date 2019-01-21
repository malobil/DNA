using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum Language { fr, ang }


public class Script_CVS_Manager : MonoBehaviour
{
    public static Script_CVS_Manager Instance { get; private set; }

    public Language playerLanguage; // Variable to store the player language ( can be ask in the game and change here )

    public TextAsset csv_tutorial; // public variable to stock the csvFile ( who contains loca / quest... )
    public TextAsset csv_dialog; // public variable to stock the csvFile ( who contains loca / quest... )
    public TextAsset csv_notes; // public variable to stock the csvFile ( who contains loca / quest... )

    public List<TutorialExport> tutorial_key_import; // Stock each key ( line ) in a custom class
    public List<NoteExport> notes_key_import; // Stock each key ( line ) in a custom class
    public List<DialogExport> dialog_key_import; // Stock each key ( line ) in a custom class

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        #region Tutorial csv

        string[] data = csv_tutorial.text.Split(new char[] { '\n' }); // Separate each line of a file ( CSV better )

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' }); // Separate each ',' from a CSV file
            TutorialExport importRow = new TutorialExport(); // Create a custom class to stock text / id...
            importRow.keyName = row[0];
            importRow.titleFr = row[1];
            importRow.textFr = row[2];
            importRow.titleEng = row[3];
            importRow.textEng = row[4];
            tutorial_key_import.Add(importRow);
            //Debug.Log(row);
        }

        #endregion

        #region Notes csv

        string[] noteData = csv_notes.text.Split(new char[] { '\n' }); // Separate each line of a file ( CSV better )

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' }); // Separate each ',' from a CSV file
            NoteExport importRow = new NoteExport(); // Create a custom class to stock text / id...
            importRow.keyName = row[0];
            importRow.titleFr = row[1];
            importRow.textFr = row[2];
            importRow.titleEng = row[3];
            importRow.textEng = row[4];
            notes_key_import.Add(importRow);
            //Debug.Log(row);
        }

        #endregion

        #region Dialog csv

        string[] dialogData = csv_dialog.text.Split(new char[] { '\n' }); // Separate each line of a file ( CSV better )

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' }); // Separate each ',' from a CSV file
            DialogExport importRow = new DialogExport(); // Create a custom class to stock text / id...
            importRow.keyName = row[0];
            importRow.titleFr = row[1];
            importRow.textFr = row[2];
            importRow.titleEng = row[3];
            importRow.textEng = row[4];
            dialog_key_import.Add(importRow);
            //Debug.Log(row);
        }

        #endregion

    }

    public string ReturnPlayerLanguage() // Return the player language in string
    {
        return playerLanguage.ToString("");
    }
}

[Serializable]
public class TutorialExport
{
    public string keyName ,titleFr, textFr, titleEng, textEng;
}

[Serializable]
public class NoteExport
{
    public string keyName, titleFr, textFr, titleEng, textEng;
}

[Serializable]
public class DialogExport
{
    public string keyName, titleFr, textFr, titleEng, textEng;
}



