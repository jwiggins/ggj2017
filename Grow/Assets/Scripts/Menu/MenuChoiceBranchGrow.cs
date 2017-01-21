using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceBranchGrow : MenuChoice
{
    private Plant _plant;
    public Plant Plant
    {
        get { return this._plant; }
        set { this._plant = value; }
    }
    public override void execute()
    {
        this.AttachPoint.grow(this.Plant, this.Plant.StemModuleSingle);
        return;
    }
}
