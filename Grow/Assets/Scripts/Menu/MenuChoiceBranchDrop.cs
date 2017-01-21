using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceBranchDrop : MenuChoice {

    public override void execute()
    {
        this.AttachPoint.drop();
        return;
    }
}
