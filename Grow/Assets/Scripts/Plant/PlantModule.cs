using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantModule : MonoBehaviour {
    public enum PlantType
    {
        SEED,
        ROOT,
        STEM,
        LEAF,
        FLOWER,
    }
    [SerializeField]
    private string _name;
    [SerializeField]
    private AttachmentPoint _rootPoint;
    [SerializeField]
    private SkinnedMeshRenderer _renderer;
    [SerializeField]
    private int _health;
    [SerializeField]
    private int _temperatureResistance;
    [SerializeField]
    private int _gravityResistance;
    [SerializeField]
    private int _animalResistance;
    [SerializeField]
    private PlantType _type;
    [SerializeField]
    private int _lightGathering;
    [SerializeField]
    private int _waterGathering;

    private Plant _plant;

    public PlantModule[] children
    {
        get {
            List<PlantModule> result = new List<PlantModule>();
            if(this is StemModule)
            {
                StemModule stemModule = (StemModule)this;
                foreach(AttachmentPoint attachPoint in stemModule.AttachPoints)
                {
                    PlantModule attachedModule = attachPoint.AttachedModule;
                    if (attachedModule == null) continue;
                    result.Add(attachedModule);
                    foreach(PlantModule plantModule in attachedModule.children)
                    {
                        result.Add(plantModule);
                    }
                }
            }
            return result.ToArray();
        }
    }

    public PlantType type
    {
        get { return _type; }
    }

    public SkinnedMeshRenderer SkinnedMeshRenderer
    {
        get { return this._renderer; }
    }

    public Plant Plant
    {
        get { return this._plant; }
        set { this._plant = value;}
    }

    public AttachmentPoint RootPoint
    {
        get { return this._rootPoint; }
        set { this._rootPoint = value; }
    }
}
