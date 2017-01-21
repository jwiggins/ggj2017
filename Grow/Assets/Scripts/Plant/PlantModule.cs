﻿using System.Collections;
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
