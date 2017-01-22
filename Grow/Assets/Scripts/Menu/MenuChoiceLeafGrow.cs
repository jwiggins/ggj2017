using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceLeafGrow : MenuChoice
{

    public override void execute()
    {
        AttachmentPoint attachPoint = this.AttachPoint;
        foreach(Transform transform in attachPoint.transform)
        {
            if (!transform.name.ToUpper().Contains("LEAF")) continue;
            GameObject leaf = GameObject.Instantiate(attachPoint.Plant.PlantLeaf);
            leaf.transform.SetParent(transform);
            leaf.transform.localPosition = Vector3.zero;
            leaf.transform.localRotation = Quaternion.identity;
            float s = 100f;
            leaf.transform.localScale = new Vector3(s, s, s);
        }
        return;
    }
}
