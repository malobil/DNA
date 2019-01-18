using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Script_UI_Manager : MonoBehaviour
{
    public static Script_UI_Manager Instance { get; private set; }

    #region Pause

    public GameObject obj_pause_menu;

    #endregion

    #region Notes

    [Header ("Note UI")]
    public TextMeshProUGUI t_note_title;
    public TextMeshProUGUI t_note_description;
    public GameObject g_note;

    #endregion

    #region Collection

    [Header("Collection UI")]
    public GameObject obj_item_tile_prefab;
    public Transform t_collection_layout;

    #endregion

    [Header("General UI")]
    public Image hold_object;

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

    public void UnShowNote()
    {
        g_note.SetActive(false);
    }

    public void NewObjectHold(Sprite new_object)
    {
        hold_object.sprite = new_object;
    }

    public void AddTileToCollection(Script_Scriptable_Item item_to_add)
    {
        GameObject obj_new_tile = Instantiate(obj_item_tile_prefab, t_collection_layout);
        obj_new_tile.GetComponent<Script_Item_Collection_Tile>().SetupTile(item_to_add);
    }

    public void TogglePauseMenu()
    {
        if(!obj_pause_menu.activeSelf)
        {
            obj_pause_menu.SetActive(true);
            Script_Game_Manager.Instance.SetTimePause();
        }
        else
        {
            obj_pause_menu.SetActive(false);
            Script_Game_Manager.Instance.SetTimeResume();
        }
       
    }
}
