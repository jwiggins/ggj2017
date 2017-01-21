using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuChoice : MonoBehaviour {
    [SerializeField]
    private Image _image;

    private BranchMenu _branchMenu;

    public BranchMenu BranchMenu
    {
        get { return this._branchMenu; }
        set { this._branchMenu = value; }
    }

	public void choose()
    {
        this._branchMenu.ActiveChoice = this;
    }

    public abstract void execute();
}
