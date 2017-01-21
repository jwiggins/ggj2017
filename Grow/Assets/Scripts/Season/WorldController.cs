using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    [SerializeField]
    [Range(1,12)]
    private int _month; //this is the current month (1 month = 1 round)
    [SerializeField]
    private Season[] _seasons; //these are all the seasons

    private Season _season; //this is the current season

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
