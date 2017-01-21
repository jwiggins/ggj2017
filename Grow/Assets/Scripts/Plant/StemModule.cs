using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]
public class StemModule : PlantModule {

    [SerializeField]
    [Range(0, 3)]
    private int _thicknessLevel;
    [SerializeField]
    private AttachmentPoint[] _attachmentPoints;
    [SerializeField]
    private LeafModule _leafModule;
    [SerializeField]
    private FlowerModule _flowerModule;
    [SerializeField]
    private Transform _menuAnchor;

    public LeafModule LeafModule
    {
        get { return this._leafModule; }
        set {
            value.transform.SetParent(this.transform);
            value.transform.position = this.transform.position;
            this._leafModule = value;
        }
    }

    public FlowerModule FlowerModule
    {
        get { return this._flowerModule; }
        set { this._flowerModule = value; }
    }

    public bool AssignMenuChoices(BranchMenu menu)
    {
        List<MenuChoice> result = new List<MenuChoice>();
        {
            MenuChoiceBranchDrop choice = GameObject.Instantiate(menu._choiceBranchDrop);
            choice.StemModule = this;
            result.Add(choice);
        }
        if (this._thicknessLevel < 3)
        {
            MenuChoiceBranchReinforce choice = GameObject.Instantiate(menu._choiceBranchReinforce);
            choice.StemModule = this;
            result.Add(choice);
        }
        foreach(AttachmentPoint attachmentPoint in this._attachmentPoints)
        {
            if (attachmentPoint.Module == null)
            {
                MenuChoiceBranchGrow choice = GameObject.Instantiate(menu._choiceBranchGrow);
                choice.AttachPoint = attachmentPoint;
                choice.Plant = this.Plant;
                result.Add(choice);
            }
            else
            {
                MenuChoiceBranchDrop choice = GameObject.Instantiate(menu._choiceBranchDrop);
                choice.StemModule = attachmentPoint.Module;
                result.Add(choice);
            }
        }
        menu.setMenuChoices(result.ToArray());
        return (result.Count != 0) ;
    }

    void OnMouseDown()
    {
        BranchMenu.create(this); //create a BranchMenu for this StemModule
    }

    public Transform MenuAnchor
    {
        get { return this._menuAnchor; }
    }
}
