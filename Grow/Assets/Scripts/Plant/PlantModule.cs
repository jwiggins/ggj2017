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
    private PlantModule[] _children;

    public PlantModule[] children
    {
        get { return _children; }
    }

    public PlantType type
    {
        get { return _type; }
    }

    public Plant Plant
    {
        get { return this._plant; }
        set { this._plant = value;}
    }
}
