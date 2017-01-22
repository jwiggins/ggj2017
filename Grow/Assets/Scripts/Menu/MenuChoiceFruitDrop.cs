using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceFruitDrop : MenuChoice
{

    public override void execute()
    {
        AttachmentPoint attachPoint = this.AttachPoint;

        foreach (Transform transform in attachPoint.transform)
        {
            if (!transform.name.ToUpper().Contains("FLOWER")) continue;
            if (transform.childCount < 2) continue;
            Transform child = transform.GetChild(1);
            child.parent = null;
            Destroy(child.gameObject);
            Transform flower = transform.GetChild(0);
            flower.parent = null;
            Destroy(flower.gameObject);
        }
        return;
    }
}
