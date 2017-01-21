using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
    
    private PlantModule _core;
    private int _light = 0;
    private int _water = 0;

    [SerializeField]
    private PlantModule[] _moduleTemplates;

	// Use this for initialization
	void Start () {
        this._core = GameObject.Instantiate(_moduleTemplates[0]).GetComponent<PlantModule>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ExperienceEnvironment() {
	}
}
