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
    private float _thickness;
    [SerializeField]
    private int _blendShapeIndex;

    public List<Transform> Leafs
    {
        get
        {
            List<Transform> result = new List<Transform>();
            foreach (Transform child in this.transform)
            {
                if (child.name.ToUpper().Contains("LEAF")&&child.transform.childCount>0)
                {
                    result.Add(child.GetChild(0));
                }
            }
            return result;
        }
    }

    public List<Transform> Flowers
    {
        get
        {
            List<Transform> result = new List<Transform>();
            foreach (Transform child in this.transform)
            {
                if (child.name.ToUpper().Contains("FLOWER") && child.transform.childCount > 0)
                {
                    result.Add(child.GetChild(0));
                }
            }
            return result;
        }
    }

    public List<Transform> Fruit
    {
        get
        {
            List<Transform> result = new List<Transform>();
            foreach (Transform child in this.transform)
            {
                if (child.name.ToUpper().Contains("FLOWER") && child.transform.childCount > 1)
                {
                    result.Add(child.GetChild(1));
                }
            }
            return result;
        }
    }

    bool mouseOver = false;
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
    public float ThicknessLevel
    {
        get { return this._thickness; }
        set
        {
            float newThickness = Mathf.RoundToInt(Mathf.Clamp((float)value, 0f, 100f));
            this._thickness = newThickness;
            SkinnedMeshRenderer skinnedRoot = this.RootModule.SkinnedMeshRenderer;
            if (skinnedRoot != null)
            {
                if (this.RootModule.AttachPoints.Length > 1)
                {
                    skinnedRoot.SetBlendShapeWeight(this._blendShapeIndex, newThickness+100);
                }
                else
                {
                    skinnedRoot.SetBlendShapeWeight(this._blendShapeIndex, newThickness);
                }
                if (this.RootModule.RootPoint == null)
                {
                    skinnedRoot.SetBlendShapeWeight(this.RootModule.RootBlendShapeIndex, newThickness);
                }
            }
            if (this.AttachedModule != null)
            {
                SkinnedMeshRenderer skinnedAttached = this.AttachedModule.SkinnedMeshRenderer;
                if (skinnedAttached != null)
                    skinnedAttached.SetBlendShapeWeight(this.AttachedModule.RootBlendShapeIndex, newThickness);
            }
        }
    }
    #endregion

    #region public functions
    
    void OnMouseDown()
    {
        if (BranchMenu.CurrentMenu != null) return;
        this.mouseOver = true;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (this.mouseOver)
            {
                this.mouseOver = false;
                if (BranchMenu.CurrentCoroutine != null) return;
                BranchMenu.create(this); //create a BranchMenu for this StemModule
            }
        }
    }

    public void Relocate()
    {
        if (this.AttachedModule != null)
        {
            this.AttachedModule.transform.position = this.transform.position;
            this.AttachedModule.transform.rotation = this.transform.rotation;
            if(this.AttachedModule is StemModule)
            {
                AttachmentPoint[] attachPoints = ((StemModule)this.AttachedModule).AttachPoints;
                for(int i = 0; i < attachPoints.Length; i++)
                {
                    attachPoints[i].Relocate();
                }
            }
        }
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
        }
        else
        {
            List<Transform> leafs = this.Leafs;
            List<Transform> flowers = this.Flowers;
            List<Transform> fruit = this.Fruit;
            if (leafs.Count > 0 && flowers.Count == 0)
            {
                MenuChoiceLeafDrop dropChoice = GameObject.Instantiate(menu._choiceLeafDrop);
                dropChoice.AttachPoint = this;
                result.Add(dropChoice);
                MenuChoiceFlowerGrow growChoice = GameObject.Instantiate(menu._choiceFlowerGrow);
                growChoice.AttachPoint = this;
                result.Add(growChoice);
            }
            else if (flowers.Count > 0 && fruit.Count==0)
            {
                MenuChoiceFlowerDrop dropChoice = GameObject.Instantiate(menu._choiceFlowerDrop);
                dropChoice.AttachPoint = this;
                result.Add(dropChoice);
                MenuChoiceFruitGrow growChoice = GameObject.Instantiate(menu._choiceFruitGrow);
                growChoice.AttachPoint = this;
                result.Add(growChoice);
            }
            else if (fruit.Count>0)
            {
                MenuChoiceFruitDrop dropChoice = GameObject.Instantiate(menu._choiceFruitDrop);
                dropChoice.AttachPoint = this;
                result.Add(dropChoice);
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

        }
        bool mayGrow = true;
        if (this.RootModule.RootPoint != null)
        {
            if (this.RootModule.RootPoint.ThicknessLevel <= this.ThicknessLevel)
            {
                mayGrow = false;
            }
        }
        if (this._thickness < 100 && mayGrow && this.AttachedModule!=null)
        {
            MenuChoiceBranchReinforce choice = GameObject.Instantiate(menu._choiceBranchReinforce);
            choice.AttachPoint = this;
            result.Add(choice);
        }
        if (this.RootModule.ForkingAmount < 2 && this.RootModule.RootPoint!=null)
        {
            MenuChoiceBranchSplit splitChoice = GameObject.Instantiate(menu._choiceBranchSplit);
            splitChoice.AttachPoint = this;
            result.Add(splitChoice);
        }
        if (this.RootModule.ForkingAmount< 3 && this.RootModule.RootPoint != null)
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
