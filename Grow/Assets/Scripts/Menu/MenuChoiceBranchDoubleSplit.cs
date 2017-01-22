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
        StemModule stemModuleTriple = GameObject.Instantiate(stemModule.Plant.StemModuleTriple);
        stemModuleTriple.Plant = stemModule.Plant;

        AttachmentPoint rootPoint = stemModule.RootPoint;
        stemModule.RootPoint = null;
        rootPoint.AttachedModule = stemModuleTriple;
        stemModuleTriple.RootPoint = rootPoint;

        stemModuleTriple.transform.SetParent(stemModule.Plant.transform);
        stemModuleTriple.transform.position = stemModule.transform.position;
        stemModuleTriple.transform.rotation = stemModule.transform.rotation;
        for(int i = 0; i < oldPoints.Length; i++)
        {
            PlantModule attachedModule = oldPoints[i].AttachedModule;
            if (attachedModule == null) continue;
            attachedModule.RootPoint = stemModuleTriple.AttachPoints[i];
            stemModuleTriple.AttachPoints[i].AttachedModule = attachedModule;
            stemModuleTriple.AttachPoints[i].Relocate();
        }
        GameObject.Destroy(stemModule.gameObject);
    }
}
