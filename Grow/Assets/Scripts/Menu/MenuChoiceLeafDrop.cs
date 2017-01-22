using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceLeafDrop : MenuChoice
{

    public override void execute()
    {
        AttachmentPoint attachPoint = this.AttachPoint;

        foreach (Transform transform in attachPoint.transform)
        {
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
