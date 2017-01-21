using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StemModule : PlantModule {

    [SerializeField]
    private AttachmentPoint[] _attachmentPoints;

    public int ForkingAmount
    {
        get { return _attachmentPoints.Length; }
    }
    public AttachmentPoint[] AttachPoints
    {
        get
        {
            return _attachmentPoints;
        }
    }
}
