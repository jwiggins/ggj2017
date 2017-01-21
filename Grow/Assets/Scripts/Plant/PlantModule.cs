using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantModule : MonoBehaviour {
    private enum PlantType
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
    [SerializeField]
    [Range(0, 3)]
    private int _thicknessLevel;
    [SerializeField]
    private AttachmentPoint[] _attachmentPoints;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
