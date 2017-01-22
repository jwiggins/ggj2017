using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceBranchSplit : MenuChoice
{

    public override void execute()
    {
        PlantModule plantModule = this.AttachPoint.RootModule;
        StemModule stemModule = (StemModule)plantModule;
        AttachmentPoint[] oldPoints = stemModule.AttachPoints;
        StemModule stemModuleDouble = GameObject.Instantiate(plantModule.Plant.StemModuleDouble);
        stemModuleDouble.Plant = plantModule.Plant;

        AttachmentPoint rootPoint = stemModule.RootPoint;
        stemModule.RootPoint = null;
        rootPoint.AttachedModule = stemModuleDouble;
        stemModuleDouble.RootPoint = rootPoint;

        stemModuleDouble.transform.SetParent(plantModule.Plant.transform);
        stemModuleDouble.transform.position = stemModule.transform.position;
        stemModuleDouble.transform.rotation = stemModule.transform.rotation;
        for (int i = 0; i < oldPoints.Length; i++)
        {
            PlantModule attachedModule = oldPoints[i].AttachedModule;
            if (attachedModule == null) continue;
            attachedModule.RootPoint = stemModuleDouble.AttachPoints[i];
            stemModuleDouble.AttachPoints[i].AttachedModule = attachedModule;
            stemModuleDouble.AttachPoints[i].Relocate();
        }
        GameObject.Destroy(stemModule.gameObject);
    }
}
