using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Script_UI_Manager : MonoBehaviour
{
    public static Script_UI_Manager Instance { get; private set; }

    public EventSystem event_system;

    #region Pause

    [Header("Pause menu")]
    public GameObject obj_pause_menu;
    public Transform obj_menu_parent;
    public GameObject obj_base_button_selected;

    public void ShowPauseMenu()
    {
        obj_pause_menu.SetActive(true);
        event_system.SetSelectedGameObject(obj_base_button_selected);
        obj_base_button_selected.GetComponent<Button>().OnSelect(null);
    }

    #endregion

    #region Notes

    [Header ("Note UI")]
    public TextMeshProUGUI t_note_title;
    public TextMeshProUGUI t_note_description;
    public GameObject g_note;

    public void ShowNote(string loca_key_title, string local_key_description)
    {
        t_note_title.text = Script_Localization_Manager.Instance.GetLocalisedText(loca_key_title);
        t_note_description.text = Script_Localization_Manager.Instance.GetLocalisedText(local_key_description);
        Script_Game_Manager.Instance.SetTimePause();
        g_note.SetActive(true);
    }

    public void UnShowNote()
    {
        g_note.SetActive(false);
    }

    #endregion

    #region Collection

    [Header("Collection UI")]
    public GameObject obj_item_tile_prefab;
    public Transform t_collection_layout;

    #endregion

    #region Alter

    public GameObject obj_transformation_tile;
    public GameObject obj_transformation_choice;
    public GameObject obj_no_transformation_text;
    public Transform t_transformation_layout;

    private List<GameObject> b_list_transformation = new List<GameObject>() ;
    #endregion

    #region Distort

    [Header("New items UI")]
    public GameObject obj_new_item_ui;
    public TextMeshProUGUI t_obj_name ;
    public TextMeshProUGUI t_obj_description ;
    public Image img_obj_sprite ;

    #endregion


    public GameObject obj_interaction_ui_canvas;
    public GameObject obj_interaction_disable_ui_canvas;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LauchGame()
    {
        SceneManager.LoadScene("Scene_Kevin");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Scene_Arthur");
    }

    #region GameOver

    [Header("Game Over")]

    public GameObject g_game_over;

    public void ShowUIGameOver()
    {
        g_game_over.SetActive(true);
    }

    public void UnshowUIGameOver()
    {
        g_game_over.SetActive(false);
    }

    #endregion

    #region Dialogue

    [Header("Dialogue")]

    public GameObject g_dialogue_menu;

    public void ShowDialogue()
    {
        g_dialogue_menu.SetActive(true);
    }

    public void UnshowDialogue()
    {
        g_dialogue_menu.SetActive(false);
    }

    #endregion

    #region Item

    public GameObject obj_item_overview;
    public TextMeshProUGUI t_item_name ;
    public TextMeshProUGUI t_item_level ;

    public Color under_level_color;
    public Color over_level_color;
    

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

    public void NewObjectHold(Sprite new_object)
    {
        if(new_object == null)
        {
            hold_object.enabled = false;
        }
        else
        {
            hold_object.enabled = true;
        }
       
        hold_object.sprite = new_object;
    }

    public void AddTileToCollection(Script_Scriptable_Item item_to_add)
    {
        GameObject obj_new_tile = Instantiate(obj_item_tile_prefab, t_collection_layout);
        obj_new_tile.GetComponent<Script_Item_Collection_Tile>().SetupTile(item_to_add);
    }

    public void OpenTransformationChoice(Script_Scriptable_Item item_to_transform)
    {
        foreach (Transform child in t_transformation_layout)
        {
            Destroy(child.gameObject);
        }
        b_list_transformation.Clear();
        obj_transformation_choice.SetActive(true);

        foreach(Script_Scriptable_Item item in Script_Collection.Instance.l_item_in_collection)
        {
            if(item.i_item_level <= item_to_transform.i_item_level && item != item_to_transform)
            {
                GameObject obj_new_tile = Instantiate(obj_transformation_tile, t_transformation_layout);
                obj_new_tile.GetComponent<Script_Tile_Transformation>().SetupTile(item);
                b_list_transformation.Add(obj_new_tile);
                obj_no_transformation_text.SetActive(false);
            }
        }

        if (b_list_transformation.Count > 0)
        {
            event_system.SetSelectedGameObject(b_list_transformation[0]);
            b_list_transformation[0].GetComponent<Button>().OnSelect(null);
        }
    }

    public void CloseTransformationChoice(Script_Scriptable_Item item_to_transform)
    {
        obj_transformation_choice.SetActive(false);
    }

    public void HideAllMenu()
    {
        foreach(Transform child in obj_menu_parent)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void LeavePause()
    {
        Script_Game_Manager.Instance.TogglePause();
    }

    public void LoadAScene(string s_scene_to_load)
    {
        Script_Game_Manager.Instance.LoadAScene(s_scene_to_load);
    }

    #region ItemHeader

    public void ShowItemHeader(Script_Scriptable_Item info_to_show)
    {
        if(info_to_show != null)
        {
            t_item_name.text = Script_Localization_Manager.Instance.GetLocalisedText(info_to_show.s_item_CSV_name_key);
            t_item_level.text = info_to_show.i_item_level.ToString("");

            if (info_to_show.i_item_level > Script_Player.Instance.GetPlayerLevel())
            {
                t_item_level.color = over_level_color;
            }
            else if (info_to_show.i_item_level <= Script_Player.Instance.GetPlayerLevel())
            {
                t_item_level.color = under_level_color;
            }

        }

        obj_item_overview.SetActive(true);
    }

    public void HideItemHeader()
    {
        obj_item_overview.SetActive(false);
    }

    #endregion

    #region NewItem

    public void ShowNewItemUI(Script_Scriptable_Item object_data)
    {
        t_obj_name.text = Script_Localization_Manager.Instance.GetLocalisedText(object_data.s_item_CSV_name_key);
        t_obj_description.text = Script_Localization_Manager.Instance.GetLocalisedText(object_data.s_item_CSV_description_key);
        img_obj_sprite.sprite = object_data.s_item_sprite;
        obj_new_item_ui.SetActive(true);
        Script_Game_Manager.Instance.SetTimePause();
    }

    public void HideItemUI()
    {
        obj_new_item_ui.SetActive(false);
    }

    #endregion

    public void ShowInteractionUI(Vector3 new_position)
    {
        obj_interaction_ui_canvas.transform.position = new Vector3(new_position.x, new_position.y + 0.6f, 0f);
        obj_interaction_ui_canvas.SetActive(true);
    }

    public void ShowInteractionDisableUI(Vector3 new_position)
    {
        obj_interaction_disable_ui_canvas.transform.position = new Vector3(new_position.x, new_position.y + 0.6f, 0f);
        obj_interaction_disable_ui_canvas.SetActive(true);
    }

    public void HideInteractionUI()
    {
        obj_interaction_ui_canvas.SetActive(false);
    }

    public void HideInteractionDisableUI()
    {
        obj_interaction_disable_ui_canvas.SetActive(false);
    }
}
