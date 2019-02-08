using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class Script_Player : MonoBehaviour
{
    public static Script_Player Instance { get; private set; }

    #region Movement variables

    [Header("Movement")]
    public float f_move_speed_horizontal = 10f ;
    public float f_move_speed_vertical = 10f ;

    private bool b_can_move = true;

    [Header("Knockback")]
    public float f_knockback_force = 10f;
    public float f_knockback_duration = 10f;
   
    private bool b_is_knockback = false;
    private float f_current_knockback_duration = 0f;

    private Rigidbody2D player_rb;

    #endregion

    #region PLayer Thought

    [Header("Player Thought")]

    public GameObject obj_player_Thought;
    public TextMeshProUGUI text_player_Thought;
    public float f_time_text_display = 3f;
    private float f_current_time_text_display;

    #endregion

    #region Interaction variables

    [Header("Interaction")]
    public Transform t_interaction_holder_trigger;

    private List<GameObject> list_interactible_objects = new List<GameObject>() ;
    private GameObject obj_current_target;
    private bool b_can_interact = true ;
    private GameObject obj_current_object_hold;
    private int i_current_object_index = 0;

    #endregion

    #region Animation variable

    private Animator a_player_animator;

    #endregion

    #region Distort variable
    [Header("Distort")]
    public bool b_can_use_powers = true ;
    public int playerLevel = 0;
    public string s_loca_key_distort_level_too_low;
    private bool b_can_Distort = true ;
    private bool b_have_use_distort = false;
    private GameObject g_current_distortable_target;

    #endregion

    #region Alter variable
    [Header("Alter")]
    public string s_loca_key_alter_level_too_low;
    public string s_loca_key_target_not_distort ;
    private bool b_can_alter = true;
    private bool b_have_use_alter = false ;
    private GameObject g_current_alterable_target;
    private GameObject g_last_transformation;

    #endregion

    #region Cards variables

    private bool b_have_bluecard = false;

    #endregion

    #region Dialog variables

    private bool b_is_talking = false;
    private Script_Interactable s_current_dialog;

    #endregion

    private bool b_can_use_item = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        a_player_animator = GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Update()
    {
        Move();

        if(f_current_time_text_display > 0)
        {
            f_current_time_text_display -= Time.deltaTime;
        }
        else if(f_current_time_text_display < 0)
        {
           obj_player_Thought.SetActive(false);
            f_current_time_text_display = 0;
        }

        if(Input.GetKeyDown(KeyCode.R) && Script_Game_Manager.Instance.GetGameOver())
        {
            Script_Game_Manager.Instance.Retry();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && Script_Game_Manager.Instance.GetGameOver())
        {
            Script_Game_Manager.Instance.LoadAScene("Scene_Main_Menu");
        }

        if (Input.GetButtonDown("Pause") && !b_is_talking && !Script_Game_Manager.Instance.GetGameOver() && !Script_Game_Manager.Instance.GetCinematicState())
        {
            Script_Game_Manager.Instance.TogglePause();
        }

        if(b_is_knockback && !Script_Game_Manager.Instance.GetGameState())
        {
            KnockbackEffect();
        }

        if (Input.GetButtonDown("Interact") && b_is_talking)
        {
            s_current_dialog.Talk() ;
            return;
        }

        if (!Script_Game_Manager.Instance.GetGameState() && !b_is_knockback)
        {
            if (Input.GetButtonDown("Interact") && obj_current_target != null && b_can_interact)
            {
                Interact();
            }

            if (Input.GetButtonDown("Drop") && obj_current_object_hold != null)
            {
                Drop();
            }

            if (Input.GetButtonDown("SwitchTarget") && list_interactible_objects.Count > 1)
            {
                SwitchTarget();
            }

            if(Input.GetButtonDown("Use") && obj_current_object_hold != null && b_can_use_item)
            {
                UseItem();
            }

            if(Input.GetButton("Distort") && b_can_Distort && !b_have_use_distort && b_can_use_powers)
            {
                Distort();
            }

            if (Input.GetButtonDown("Distort") && b_can_Distort && b_can_use_powers)
            {
                StartDistort();
            }

            if (Input.GetButtonUp("Distort") && b_can_Distort && b_can_use_powers)
            {
                StopDistort();
            }

            if (Input.GetButton("Alter") && b_can_alter && !b_have_use_alter && b_can_use_powers)
            {
                Alter();
            }

            if (Input.GetButtonDown("Alter") && b_can_alter && b_can_use_powers)
            {
                StartAlter();
            }

            if (Input.GetButtonUp("Alter") && b_can_alter && b_can_use_powers)
            {
                StopAlter();
            }
        }
    }
   
    #region Interact

    public void Interact()
    {
        if (obj_current_target.GetComponent<Script_ObjectRelatedInteraction>() && obj_current_object_hold != null)
        {
            obj_current_target.GetComponent<Script_ObjectRelatedInteraction>().SpecialInteraction(obj_current_object_hold.GetComponent<Script_ItemInfo>().item_info);
            CheckUI();
            return;
        }

        if (obj_current_target.GetComponent<Script_Interactable>())
        {
            obj_current_target.GetComponent<Script_Interactable>().Interact(this);
            CheckUI();
        }
    }

    public void AddInteractibleObject(GameObject obj_interactible_object)
    {
        list_interactible_objects.Add(obj_interactible_object);

        if (obj_current_target == null)
        {
            SelectTarget(obj_interactible_object);
            i_current_object_index = 0;
        }
    }

    public void SwitchTarget()
    {
        i_current_object_index++;

        if(i_current_object_index > list_interactible_objects.Count -1)
        {
            i_current_object_index = 0;
        }

        SelectTarget(list_interactible_objects[i_current_object_index]);
    }

    public void SelectTarget(GameObject target)
    {
        if (obj_current_target != null)
        {
            obj_current_target.GetComponent<Outline>().DisableOutline();
        }

        obj_current_target = target;
        CheckUI();
       
    }

    void CheckUI()
    {
        Script_UI_Manager.Instance.HideInteractionUI();

        if (obj_current_target != null)
        {
            obj_current_target.GetComponent<Outline>().EnableOutline();

            if (obj_current_target.GetComponent<Script_ItemInfo>())
            {
                Script_UI_Manager.Instance.ShowItemHeader(obj_current_target.GetComponent<Script_ItemInfo>().GetItemInfo());
            }

            if (obj_current_target.GetComponent<Script_Interactable>() && !obj_current_target.GetComponent<Script_ObjectRelatedInteraction>())
            {
                Script_UI_Manager.Instance.ShowInteractionUI(obj_current_target.transform, UIIndicationButton.A, true);
            }

            if (obj_current_target.GetComponent<Script_ObjectRelatedInteraction>())
            {
                if (obj_current_object_hold != null && obj_current_target.GetComponent<Script_ObjectRelatedInteraction>().CheckIfPlayerHaveCorrectItem(obj_current_object_hold.GetComponent<Script_ItemInfo>().item_info))
                {
                    Script_UI_Manager.Instance.ShowInteractionUI(obj_current_target.transform, UIIndicationButton.A, true);
                }
                else
                {
                    Script_UI_Manager.Instance.ShowInteractionUI(obj_current_target.transform, UIIndicationButton.A, false);
                }
            }
        }
        else
        {
            Script_UI_Manager.Instance.HideItemHeader();
        }
    }

    public void RemoveInteractibleObject(GameObject obj_interactible_object)
    {
        
        if(obj_interactible_object != null)
        {
            list_interactible_objects.Remove(obj_interactible_object);

            if (obj_interactible_object.GetComponent<Outline>())
            {
                obj_interactible_object.GetComponent<Outline>().DisableOutline();
            }

            if (obj_interactible_object == obj_current_target)
            {
                SelectTarget(null);

                if (list_interactible_objects.Count > 0)
                {
                    SelectTarget(list_interactible_objects[0]);
                }
            }
        }
        
    }

    private void AllowInteract()
    {
        b_can_interact = true;
    }

    private void DisableInteract()
    {
        b_can_interact = false;
    }

    #endregion

    #region Movement
    public  void Move()
    {
        if(!b_can_move || b_is_knockback || Script_Game_Manager.Instance.GetGameState())
        {
            return;
        }

        float f_horizontal_move = Input.GetAxis("Horizontal") * f_move_speed_horizontal;
        float f_vertical_move = Input.GetAxis("Vertical") * f_move_speed_vertical;

        float f_horizontal_move_raw = Input.GetAxisRaw("Horizontal");
        float f_vertical_move_raw = Input.GetAxisRaw("Vertical");

        player_rb.velocity = new Vector2(f_horizontal_move, f_vertical_move);

        if(f_horizontal_move == 0 && f_vertical_move == 0)
        {
            a_player_animator.SetBool("Idle", true);
        }
        else
        {
            a_player_animator.SetBool("Idle", false);
        }

        #region Interaction rotation

        if (f_vertical_move_raw > 0)
        {
            t_interaction_holder_trigger.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(f_vertical_move_raw < 0)
        {
            t_interaction_holder_trigger.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if(f_vertical_move_raw < 1 || f_vertical_move_raw > -1)
        {
            if(f_horizontal_move_raw > 0)
            {
                t_interaction_holder_trigger.localRotation = Quaternion.Euler(0, 0, -90);
            }
            else if (f_horizontal_move_raw < 0)
            {
                t_interaction_holder_trigger.localRotation = Quaternion.Euler(0, 0, 90);
            }

        }

        #endregion Interaction 

        a_player_animator.SetFloat("horizontal_movement", f_horizontal_move_raw);
        a_player_animator.SetFloat("vertical_movement", f_vertical_move_raw);
    }

    public void Knockback()
    {
        Vector2 test = player_rb.velocity;
        player_rb.velocity = Vector2.zero;

        if (test != Vector2.zero)
        {
            player_rb.AddForce(-test.normalized * f_knockback_force, ForceMode2D.Impulse);
        }
        else
        {
            player_rb.AddForce(-t_interaction_holder_trigger.transform.up * f_knockback_force, ForceMode2D.Impulse);
        }
       
        b_is_knockback = true;
    }

    void KnockbackEffect() // call if the player is knockback in update function
    {
        if (f_current_knockback_duration < f_knockback_duration)
        {
            f_current_knockback_duration += Time.deltaTime;
        }
        else
        {
            b_is_knockback = false;
            f_current_knockback_duration = 0f;
        }
    }

    public void Teleport(Vector2 t_new_position)
    {
        transform.position = t_new_position;
    }

    private void AllowMove()
    {
        b_can_move = true;
    }

    private void DisableMove()
    {
        player_rb.velocity = Vector3.zero;
        b_can_move = false;
    }
    #endregion

    #region Hold & Drop

    public void Hold(GameObject obj_to_hold)
    {
        if (obj_current_object_hold != null)
        {
            Drop();
        }
            obj_current_object_hold = obj_to_hold;
            obj_current_object_hold.transform.SetParent(transform);
            obj_current_object_hold.gameObject.SetActive(false);
            obj_current_object_hold.transform.localPosition = new Vector2(0, 0);
            Script_UI_Manager.Instance.NewObjectHold(obj_current_object_hold.GetComponent<SpriteRenderer>().sprite); // UI
            RemoveInteractibleObject(obj_current_target); // Remove object from the list
            SelectTarget(null); // Remove the target        
    }


    public void DestroyHoldObject()
    {
        Destroy(obj_current_object_hold);
        Script_UI_Manager.Instance.NewObjectHold(null);
    }

    public void Drop()
    {
        obj_current_object_hold.transform.SetParent(null);
        obj_current_object_hold.gameObject.SetActive(true);
        obj_current_object_hold.GetComponent<Rigidbody2D>().AddForce(t_interaction_holder_trigger.up * 2f, ForceMode2D.Impulse);
        Script_UI_Manager.Instance.NewObjectHold(null);
        obj_current_object_hold = null;
        CheckUI();
    }

    #endregion

    #region Distort

    public void StartDistort()
    {
        b_have_use_distort = false;
        DisableMove();
        DisableInteract();
        DisableAlter();
        if (obj_current_target != null && obj_current_target.GetComponent<Script_Distortable>())
        {
            g_current_distortable_target = obj_current_target ;
        }
    }

    private void Distort() // call when the right mouse is hold
    {
        a_player_animator.SetBool("Distort", true);
    }

    public void StopDistort() // is use at end of Distort animation
    {
        a_player_animator.SetBool("Distort", false);
        AllowMove();
        AllowInteract();
        AllowAlter();
        
    }

    public void CheckDistort()
    {
        if (g_current_distortable_target != null && g_current_distortable_target == obj_current_target)
        {
            if(playerLevel >= g_current_distortable_target.GetComponent<Script_Distortable>().GetScriptableItem().i_item_level)
            {
                Script_Collection.Instance.AddItemToCollection(g_current_distortable_target.GetComponent<Script_Distortable>().GetScriptableItem());
            }
            else
            {
                text_player_Thought.text = Script_Localization_Manager.Instance.GetLocalisedText(s_loca_key_distort_level_too_low);
                obj_player_Thought.SetActive(true);
                f_current_time_text_display = f_time_text_display;
            }
        }

        b_have_use_distort = true; // to stop the animation if you still hold mouse clic
    }

    public void AllowDistort()
    {
        b_can_Distort = true;
    }

    public void DisableDistort()
    {
        b_can_Distort = false;
    }

    #endregion

    #region Alter

    public void StartAlter()
    {
        b_have_use_alter = false;
        DisableMove();
        DisableInteract();
        DisableDistort();

        if (obj_current_target != null && obj_current_target.GetComponent<Script_Alterable>())
        {
            g_current_alterable_target = obj_current_target;
        }
    }

    private void Alter() // call when the left mouse is hold
    {
        a_player_animator.SetBool("Alter", true);
    }

    public void StopAlter() // is use at end of Alter animation
    {
        a_player_animator.SetBool("Alter", false);
        AllowMove();
        AllowInteract();
        AllowDistort();
    }

    public void CheckAlter()
    {
        if (g_current_alterable_target != null && g_current_alterable_target == obj_current_target)
        {
            if (playerLevel >= g_current_alterable_target.GetComponent<Script_Alterable>().GetScriptableItem().i_item_level && Script_Collection.Instance.l_item_in_collection.Contains(g_current_alterable_target.GetComponent<Script_Alterable>().GetScriptableItem()))
            {
                Script_Game_Manager.Instance.SetTimePause();
                Script_UI_Manager.Instance.OpenTransformationChoice(g_current_alterable_target.GetComponent<Script_Alterable>().GetScriptableItem());
            }
            else if(playerLevel < g_current_alterable_target.GetComponent<Script_Alterable>().GetScriptableItem().i_item_level)
            {
                text_player_Thought.text = Script_Localization_Manager.Instance.GetLocalisedText(s_loca_key_alter_level_too_low);
                obj_player_Thought.SetActive(true);
                f_current_time_text_display = f_time_text_display;
                Debug.Log("No level");
            }
            else if(!Script_Collection.Instance.l_item_in_collection.Contains(g_current_alterable_target.GetComponent<Script_Alterable>().GetScriptableItem()))
            {
                text_player_Thought.text = Script_Localization_Manager.Instance.GetLocalisedText(s_loca_key_target_not_distort);
                obj_player_Thought.SetActive(true);
                f_current_time_text_display = f_time_text_display;
                Debug.Log("Not in collection");
            }
        }

        b_have_use_alter = true; // to stop the animation if you still hold mouse clic
    }

    public void AlterTarget(Script_Scriptable_Item transformation_choose)
    {
        GameObject transformation = Instantiate(transformation_choose.g_item_prefab, g_current_alterable_target.transform.position,Quaternion.identity);
        g_last_transformation = transformation;
        Destroy(g_current_alterable_target);
        Script_Game_Manager.Instance.SetTimeResume();
        Script_UI_Manager.Instance.HideAllMenu();
    }

    public void AllowAlter()
    {
        b_can_alter = true;
    }

    public void DisableAlter()
    {
        b_can_alter = false;
    }

    #endregion

    #region Use item

    private void UseItem()
    {
        if (obj_current_object_hold.GetComponent<Script_IObject>())
        {
            obj_current_object_hold.GetComponent<Script_IObject>().Use(obj_current_target);
        }
        
    }

    public void AllowUse()
    {
        b_can_use_item = true;
    }

    public void DisableUse()
    {
        b_can_use_item = false;
    }

    #endregion

    #region Debug / other

    public void CheckInputAfterPause() // Check if your still holding or not Alter/Distort or Throw button, if not stop them
    {
        if (!Input.GetButton("Distort") && b_can_Distort)
        {
            StopDistort();
        }

        if (!Input.GetButton("Alter") && b_can_alter)
        {
            StopAlter();
        }
    }

    public void GoOutOfWall() // Use to set player out of a wall
    {
        if (g_last_transformation != null && g_last_transformation.GetComponent<Script_Alter_Wall_Security>())
        {
            transform.position = g_last_transformation.GetComponent<Script_Alter_Wall_Security>().CheckNearPoint();
        }
    }

    public int GetPlayerLevel() // return the player level
    {
        return playerLevel;
    }

    public void CallSave()
    {
        Script_Game_Manager.Instance.Save(transform.position.x , transform.position.y , GetPlayerLevel());
    }

    public void LoadData(float x_position, float y_position, int player_level)
    {
        transform.position = new Vector2 (x_position,y_position);
        playerLevel = player_level;
    }

    #endregion

    #region Talk 

    public void EnterADialog(Script_Interactable dialog)
    {
        b_is_talking = true;
        s_current_dialog = dialog;
    }

    public void LeaveADialog()
    {
        b_is_talking = false;
        s_current_dialog = null;
    }

    #endregion

    #region Cards

    public bool CheckBlueCard()
    {
        return b_have_bluecard;
    }

    public void ObtainACard(string card)
    {
        switch(card)
        {
            case "blue":
                b_have_bluecard = true;
                break;
        }
    }

    #endregion

    #region Level

    public void LevelUp()
    {
        playerLevel++;
    }

    #endregion
}
