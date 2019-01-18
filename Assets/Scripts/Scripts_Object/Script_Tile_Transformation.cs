using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Tile_Transformation : MonoBehaviour
{
    public Image img_item_image;
    private Script_Scriptable_Item item_scriptable;

    public void SetupTile(Script_Scriptable_Item item_to_setup)
    {
        item_scriptable = item_to_setup;
        img_item_image.sprite = item_scriptable.s_item_sprite;
    }

    public void ConfirmTransformation()
    {
        Script_Player.Instance.AlterTarget(item_scriptable);
    }
}
