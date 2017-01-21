using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttachmentPoint", menuName = "ScriptableObjects/AttachmentPoint")]
[System.Serializable]
public class AttachmentPoint{

    #region private variables
    [SerializeField]
    private string _name;
    [SerializeField]
    private PlantModule _module;
    [SerializeField]
    private Vector3 _position;
    [SerializeField]
    private Vector3 _orientation;
    [SerializeField]
    private float _thickness;
    #endregion

    #region properties

    #endregion
}
