using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttachmentPoint{

    #region private variables
    [SerializeField]
    private string _name;
    [SerializeField]
    private StemModule _module;
    [SerializeField]
    private GameObject _connector;
    #endregion

    #region properties
    public StemModule Module
    {
        get { return this._module; }
    }
    #endregion

    #region public functions
    public StemModule grow(Plant plant)
    {
        this._module = GameObject.Instantiate(plant.StemModule);
        this._module.Plant = plant;
        this._module.transform.SetParent(plant.transform);
        this._module.transform.position = this._connector.transform.position;
        this._module.transform.rotation = this._connector.transform.rotation;
        return this._module;
    }
    #endregion
}
