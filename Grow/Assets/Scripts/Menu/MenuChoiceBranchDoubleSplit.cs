using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceBranchDoubleSplit : MenuChoice {

    public override void execute()
    {
        PlantModule plantModule = this.AttachPoint.RootModule;
        if (plantModule.RootPoint == null) return;
        if (!(plantModule is StemModule)) return;
        StemModule stemModule = (StemModule)plantModule;
        AttachmentPoint[] oldPoints = stemModule.AttachPoints;
        StemModule stemModuleTriple = GameObject.Instantiate(plantModule.Plant.StemModuleTriple);
        stemModuleTriple.Plant = plantModule.Plant;
        stemModuleTriple.RootPoint = plantModule.RootPoint;
        stemModuleTriple.transform.SetParent(plantModule.Plant.transform);
        stemModuleTriple.transform.position = stemModule.transform.position;
        stemModuleTriple.transform.rotation = stemModule.transform.rotation;
        for(int i = 0; i < oldPoints.Length; i++)
        {
            stemModuleTriple.AttachPoints[i].AttachedModule = oldPoints[i].AttachedModule;
        }
        stemModule.RootPoint.AttachedModule = stemModuleTriple;
        GameObject.Destroy(stemModule.gameObject);
    }
}
