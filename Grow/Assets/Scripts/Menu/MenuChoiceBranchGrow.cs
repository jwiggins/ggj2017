using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceBranchGrow : MenuChoice
{
    private AttachmentPoint _attachPoint;
    private Plant _plant;

    public AttachmentPoint AttachPoint
    {
        get { return this._attachPoint; }
        set { this._attachPoint = value; }
    }
    public Plant Plant
    {
        get { return this._plant; }
        set { this._plant = value; }
    }
    public override void execute()
    {
        AttachPoint.grow(this.Plant);
        return;
    }
}
