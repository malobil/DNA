using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartLocalization;

public enum PlayerLanguage { french,english}

public class Script_Localization_Manager : MonoBehaviour
{
    public static Script_Localization_Manager Instance { get; private set; }

    public PlayerLanguage language;

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

    public string GetLocalisedText(string key)
    {
        switch(language.ToString(""))
        {
            case "french":
                LanguageManager.Instance.ChangeLanguage("fr");
                break;

            case "english":
                LanguageManager.Instance.ChangeLanguage("eng");
                break;
        }

        return LanguageManager.Instance.GetTextValue(key);
    }
}
