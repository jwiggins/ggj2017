using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceBranchSplit : MenuChoice
{

    public override void execute()
    {
        PlantModule plantModule = this.AttachPoint.RootModule;
        if (plantModule.RootPoint == null) return;
        if (!(plantModule is StemModule)) return;
        StemModule stemModule = (StemModule)plantModule;
        AttachmentPoint[] oldPoints = stemModule.AttachPoints;
        StemModule stemModuleDouble = GameObject.Instantiate(plantModule.Plant.StemModuleDouble);
        stemModuleDouble.Plant = plantModule.Plant;
        stemModuleDouble.RootPoint = plantModule.RootPoint;
        stemModuleDouble.transform.SetParent(plantModule.Plant.transform);
        stemModuleDouble.transform.position = stemModule.transform.position;
        stemModuleDouble.transform.rotation = stemModule.transform.rotation;
        for (int i = 0; i < oldPoints.Length; i++)
        {
            stemModuleDouble.AttachPoints[i].AttachedModule = oldPoints[i].AttachedModule;
        }
        stemModule.RootPoint.AttachedModule = stemModuleDouble;
        GameObject.Destroy(stemModule.gameObject);
    }
}
