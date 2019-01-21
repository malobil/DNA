using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Script_UI_Manager : MonoBehaviour
{
    public static Script_UI_Manager Instance { get; private set; }

    #region Pause

    public GameObject obj_pause_menu;
    public Transform obj_menu_parent;

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

    #region Alter

    public GameObject obj_transformation_tile;
    public GameObject obj_transformation_choice;
    public GameObject obj_no_transformation_text;
    public Transform t_transformation_layout;
    #endregion

    #region Menu Manager

    [Header("Menu Manager")]

    public GameObject[] g_all_menu;
    private int i_current_menu;

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

    public void ChangeMenu(int i_menu_to_activate)
    {
        g_all_menu[i_current_menu].SetActive(false);
        g_all_menu[i_menu_to_activate].SetActive(true);
        i_current_menu = i_menu_to_activate;
    }


    #endregion

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

    private void Start()
    {
        i_current_menu = 0;
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

    public void OpenTransformationChoice(Script_Scriptable_Item item_to_transform)
    {
        foreach (Transform child in t_transformation_layout)
        {
            Destroy(child.gameObject);
        }

        obj_transformation_choice.SetActive(true);

        foreach(Script_Scriptable_Item item in Script_Collection.Instance.l_item_in_collection)
        {
            if(item.i_item_level <= item_to_transform.i_item_level && item != item_to_transform)
            {
                GameObject obj_new_tile = Instantiate(obj_transformation_tile, t_transformation_layout);
                obj_new_tile.GetComponent<Script_Tile_Transformation>().SetupTile(item);
                obj_no_transformation_text.SetActive(false);
            }
        }
    }

    public void CloseTransformationChoice(Script_Scriptable_Item item_to_transform)
    {
        obj_transformation_choice.SetActive(false);
    }

    public void ShowPauseMenu()
    {
           obj_pause_menu.SetActive(true);
    }

    public void HideAllMenu()
    {
        foreach(Transform child in obj_menu_parent)
        {
            child.gameObject.SetActive(false);
        }
    }
}
