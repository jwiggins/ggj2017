using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceBranchReinforce : MenuChoice
{
    public override void execute()
    {
        this.AttachPoint.ThicknessLevel+=25;
        return;
    }
}
