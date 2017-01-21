using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    private SeedModule _core;
    private int _energy = 0;

    [SerializeField]
    private BranchMenu _branchMenu;

    [SerializeField]
    private SeedModule _seed;
    [SerializeField]
    private StemModule _stem;
    [SerializeField]
    private StemModule _stemDouble;
    [SerializeField]
    private StemModule _stemTriple;
    [SerializeField]
    private LeafModule _leaf;
    [SerializeField]
    private FlowerModule _flower;

    public PlantModule[] children
    {
        get
        {
            List<PlantModule> result = new List<PlantModule>();
            result.Add(this.RootModule);
            result.AddRange(this.RootModule.children);
            return result.ToArray();
        }
    }

    public SeedModule SeedModule
    {
        get { return this._seed; }
    }

    public StemModule StemModuleSingle
    {
        get { return this._stem; }
    }

    public StemModule StemModuleDouble
    {
        get { return this._stemDouble; }
    }

    public StemModule StemModuleTriple
    {
        get { return this._stemTriple; }
    }

    public LeafModule LeafModule
    {
        get { return this._leaf; }
    }

    public FlowerModule FlowerModule
    {
        get { return this._flower; }
    }

    public SeedModule RootModule
    {
        get { return this._core; }
    }

    public BranchMenu BranchMenu { get { return this._branchMenu; } }

    // Use this for initialization
    void Start () {
        this._core = GameObject.Instantiate(this._seed);
        this._core.Plant = this;
        this._core.transform.SetParent(this.transform);
    }

    // Update is called once per frame
    void Update () {
    }

    public void ExperienceEnvironment(float temperature, float moisture, float light) {
        _energy += _calcEnergy(_core, temperature, moisture, light);
    }

    private int _calcEnergy(PlantModule current, float temperature, float moisture, float light) {
        int localEnergy = 0;

        if (current != null) {
            foreach (PlantModule mod in current.children) {
                localEnergy += _calcEnergy(mod, temperature, moisture, light);
            }

            switch (current.type) {
                case PlantModule.PlantType.SEED:
                    localEnergy += 0;
                    break;
                case PlantModule.PlantType.ROOT:
                    localEnergy += 5;
                    break;
                case PlantModule.PlantType.STEM:
                    localEnergy += 0;
                    break;
                case PlantModule.PlantType.LEAF:
                    localEnergy += (int)light * 25;
                    break;
                case PlantModule.PlantType.FLOWER:
                    localEnergy += 0;
                    break;
            }
        }
        return localEnergy;
    }

}
