using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceBranchReinforce : MenuChoice
{
    private StemModule _stemModule;

    public StemModule StemModule
    {
        get { return this._stemModule; }
        set { this._stemModule = value; }
    }
    public override void execute()
    {
        return;
    }
}
