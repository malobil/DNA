using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Script_CVS_Manager : MonoBehaviour
{
    public static Script_CVS_Manager Instance { get; private set; }

    public enum Language { fr, ang }

    public Language playerLanguage; // Variable to store the player language ( can be ask in the game and change here )

    public TextAsset csvFile; // public variable to stock the csvFile ( who contains loca / quest... )

    public List<TextImportation> keysImport; // Stock each key ( line ) in a custom class

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

        string[] data = csvFile.text.Split(new char[] { '\n' }); // Separate each line of a file ( CSV better )


        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' }); // Separate each ',' from a CSV file
            TextImportation importRow = new TextImportation(); // Create a custom class to stock text / id...
            importRow.keyName = row[0];
            importRow.textFr = row[1];
            importRow.textAng = row[2];
            keysImport.Add(importRow);
            Debug.Log(row);
        }
    }

    public string ReturnPlayerLanguage() // Return the player language in string
    {
        return playerLanguage.ToString("");
    }
}

[Serializable]
public class TextImportation
{
    public string keyName, textFr, textAng;
}

