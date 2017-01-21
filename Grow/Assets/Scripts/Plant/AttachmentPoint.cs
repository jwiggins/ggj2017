using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[System.Serializable]
public class AttachmentPoint : MonoBehaviour{

    #region private variables
    [SerializeField]
    private StemModule _rootModule;
    [SerializeField]
    private PlantModule _attachedModule;
    [SerializeField]
    [Range(0, 3)]
    private int _thicknessLevel;
    [SerializeField]
    private int _blendShapeIndex;
    #endregion

    #region properties
    public StemModule RootModule
    {
        get { return this._rootModule; }
    }
    public PlantModule AttachedModule
    {
        get { return this._attachedModule; }
        set {
            this._attachedModule = value;
            if (value != null)
            {
                this._attachedModule.transform.position = this.transform.position;
                this._attachedModule.transform.rotation = this.transform.rotation;
                this.ThicknessLevel = this.ThicknessLevel;
            }
        }
    }
    public Plant Plant
    {
        get { return this.RootModule.Plant; }
    }
    public int ThicknessLevel
    {
        get { return this._thicknessLevel; }
        set
        {
            int maxThickness = 4;
            int newThickness = Mathf.Clamp(value, 0, maxThickness);
            if (newThickness > this.ThicknessLevel)
                this._thicknessLevel = newThickness;
            float thicknessValue = newThickness / (float)(Mathf.Max(1, maxThickness));
            SkinnedMeshRenderer skinnedRoot = this.RootModule.SkinnedMeshRenderer;
            if (skinnedRoot != null)
            {
                skinnedRoot.SetBlendShapeWeight(this._blendShapeIndex, thicknessValue * 100);
                if (this.RootModule.RootPoint == null)
                {
                    skinnedRoot.SetBlendShapeWeight(this.RootModule.RootBlendShapeIndex, thicknessValue * 100);
                }
            }
            if (this.AttachedModule != null)
            {
                SkinnedMeshRenderer skinnedAttached = this.AttachedModule.SkinnedMeshRenderer;
                if (skinnedAttached != null)
                    skinnedAttached.SetBlendShapeWeight(this.AttachedModule.RootBlendShapeIndex, thicknessValue * 100);
            }
        }
    }
    #endregion

    #region public functions

    void OnMouseDown()
    {
        BranchMenu.create(this); //create a BranchMenu for this StemModule
    }

    public PlantModule grow(Plant plant, PlantModule template)
    {
        this._attachedModule = GameObject.Instantiate(template);
        this._attachedModule.Plant = plant;
        this._attachedModule.RootPoint = this;
        this._attachedModule.transform.SetParent(plant.transform);
        this._attachedModule.transform.position = this.transform.position;
        this._attachedModule.transform.rotation = this.transform.rotation;
        this.ThicknessLevel = this.ThicknessLevel;
        return this._attachedModule;
    }
    public void drop()
    {
        if (this.AttachedModule != null)
        {
            if (this.AttachedModule != null)
            {
                if (this.AttachedModule is StemModule)
                {
                    StemModule stemModule = (StemModule)this.AttachedModule;
                    for(int i = 0; i < stemModule.AttachPoints.Length; i++)
                    {
                        AttachmentPoint point = stemModule.AttachPoints[i];
                        point.drop();
                    }
            }
            }
            GameObject.Destroy(this.AttachedModule.gameObject);
            this._attachedModule = null;
        }
    }
    public bool AssignMenuChoices(BranchMenu menu)
    {
        List<MenuChoice> result = new List<MenuChoice>();
        if (this.AttachedModule != null) {
            if(this.AttachedModule is StemModule)
            {
                MenuChoiceBranchDrop dropChoice = GameObject.Instantiate(menu._choiceBranchDrop);
                dropChoice.AttachPoint = this;
                result.Add(dropChoice);
            }
            else if(this.AttachedModule is LeafModule)
            {
                MenuChoiceLeafDrop dropChoice = GameObject.Instantiate(menu._choiceLeafDrop);
                dropChoice.AttachPoint = this;
                result.Add(dropChoice);
                MenuChoiceFlowerGrow growChoice = GameObject.Instantiate(menu._choiceFlowerGrow);
                growChoice.AttachPoint = this;
                result.Add(growChoice);
            }
            else if(this.AttachedModule is FlowerModule)
            {
                MenuChoiceFlowerDrop dropChoice = GameObject.Instantiate(menu._choiceFlowerDrop);
                dropChoice.AttachPoint = this;
                result.Add(dropChoice);
                MenuChoiceFruitGrow growChoice = GameObject.Instantiate(menu._choiceFruitGrow);
                growChoice.AttachPoint = this;
                result.Add(growChoice);
            }
            else if(this.AttachedModule is FruitModule)
            {
                MenuChoiceFruitDrop dropChoice = GameObject.Instantiate(menu._choiceFruitDrop);
                dropChoice.AttachPoint = this;
                result.Add(dropChoice);
            }
        }
        else
        {
            MenuChoiceBranchGrow branchChoice = GameObject.Instantiate(menu._choiceBranchGrow);
            branchChoice.AttachPoint = this;
            branchChoice.Plant = this.Plant;
            result.Add(branchChoice);

            MenuChoiceLeafGrow leafChoice = GameObject.Instantiate(menu._choiceLeafGrow);
            leafChoice.AttachPoint = this;
            result.Add(leafChoice);
        }
        if (this._thicknessLevel < 3)
        {
            MenuChoiceBranchReinforce choice = GameObject.Instantiate(menu._choiceBranchReinforce);
            choice.AttachPoint = this;
            result.Add(choice);
        }
        if (this.RootModule.ForkingAmount <3)
        {
            MenuChoiceBranchSplit splitChoice = GameObject.Instantiate(menu._choiceBranchSplit);
            splitChoice.AttachPoint = this;
            result.Add(splitChoice);
        }
        if (this.RootModule.ForkingAmount < 2)
        {
            MenuChoiceBranchDoubleSplit splitChoice = GameObject.Instantiate(menu._choiceBranchDoubleSplit);
            splitChoice.AttachPoint = this;
            result.Add(splitChoice);
        }
        menu.setMenuChoices(result.ToArray());
        return (result.Count != 0);
    }
    #endregion
}
