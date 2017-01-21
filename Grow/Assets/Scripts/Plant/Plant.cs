using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
    
    private PlantModule _core;
    private int _light = 0;
    private int _water = 0;

    [SerializeField]
    private BranchMenu _branchMenu;

    [SerializeField]
    private SeedModule _seed;
    [SerializeField]
    private StemModule _stem;
    [SerializeField]
    private LeafModule _leaf;
    [SerializeField]
    private FlowerModule _flower;
    [SerializeField]
    private RootModule _root;

    public SeedModule SeedModule
    {
        get { return this._seed; }
    }

    public StemModule StemModule
    {
        get { return this._seed; }
    }

    public LeafModule LeafModule
    {
        get { return this._leaf; }
    }

    public FlowerModule FlowerModule
    {
        get { return this._flower; }
    }

    public RootModule RootModule
    {
        get { return this._root; }
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
    }

}
