using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceFlowerDrop : MenuChoice
{

    public override void execute()
    {
        AttachmentPoint attachPoint = this.AttachPoint;

        foreach (Transform transform in attachPoint.transform)
        {
            if (!transform.name.ToUpper().Contains("FLOWER")) continue;
            while (transform.childCount > 0)
            {
                Transform child = transform.GetChild(0);
                child.parent = null;
                Destroy(child.gameObject);
            }
        }
        return;
    }
}
