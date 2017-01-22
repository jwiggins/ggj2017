using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    private SeedModule _core;
    private int _energy = 0;

    [SerializeField]
    private BranchMenu _branchMenu;

    [SerializeField]
    private SeedModule[] _seeds;
    [SerializeField]
    private StemModule[] _stems;
    [SerializeField]
    private StemModule[] _stemDoubles;
    [SerializeField]
    private StemModule[] _stemTriples;
    [SerializeField]
    private LeafModule[] _leafs;
    [SerializeField]
    private FlowerModule[] _flowers;
    [SerializeField]
    private RootModule[] _roots;

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
        get { return this._seeds[Random.Range(0, this._seeds.Length)]; }
    }

    public StemModule StemModuleSingle
    {
        get { return this._stems[Random.Range(0, this._stems.Length)]; }
    }

    public StemModule StemModuleDouble
    {
        get { return this._stemDoubles[Random.Range(0, this._stemDoubles.Length)]; }
    }

    public StemModule StemModuleTriple
    {
        get { return this._stemTriples[Random.Range(0, this._stemTriples.Length)]; }
    }

    public LeafModule LeafModule
    {
        get { return this._leafs[Random.Range(0, this._leafs.Length)]; }
    }

    public FlowerModule FlowerModule
    {
        get { return this._flowers[Random.Range(0, this._flowers.Length)]; }
    }

    public SeedModule RootModule
    {
        get { return this._core; }
    }

    public BranchMenu BranchMenu { get { return this._branchMenu; } }

    public int energy
    {
        get { return this._energy; }
    }

    // Use this for initialization
    void Start () {
        this._core = GameObject.Instantiate(this._seeds[Random.Range(0, this._seeds.Length)]);
        this._core.Plant = this;
        this._core.transform.SetParent(this.transform);
    }

    // Update is called once per frame
    void Update () {
    }

    public void ExperienceEnvironment(float temperature, float moisture, float light) {
        foreach (PlantModule mod in this.children) {
            _energy += _positiveEnergy(mod, light);
            if (temperature < 0.0f) {
                _energy -= _negativeEnergy(mod);
            }
        }
    }

    private int _negativeEnergy(PlantModule current) {
        switch (current.type) {
            case PlantModule.PlantType.FLOWER:
                return 40;
            case PlantModule.PlantType.LEAF:
                return 25;
            case PlantModule.PlantType.SEED:
                return 15;
            default:
                break;
        }
        return 0;
    }

    private int _positiveEnergy(PlantModule current, float light) {
        switch (current.type) {
            case PlantModule.PlantType.LEAF:
                return (int)light * 25;
            case PlantModule.PlantType.ROOT:
                return 5;
            case PlantModule.PlantType.STEM:
                return 1;
            default:
                break;
        }
        return 0;
    }


}
