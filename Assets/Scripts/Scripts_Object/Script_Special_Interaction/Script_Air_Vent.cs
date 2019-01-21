using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Air_Vent : Script_ISpecialInteraction
{
    public override void SpecialInteraction()
    {
        Destroy(gameObject);
    }
}
