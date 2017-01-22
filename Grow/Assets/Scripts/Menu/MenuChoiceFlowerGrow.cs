using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceFlowerGrow : MenuChoice
{

    public override void execute()
    {
        AttachmentPoint attachPoint = this.AttachPoint;
        foreach (Transform transform in attachPoint.transform)
        {
            if (!transform.name.ToUpper().Contains("FLOWER")) continue;
            GameObject flower = GameObject.Instantiate(attachPoint.Plant.PlantFlower);
            flower.transform.SetParent(transform);
            flower.transform.localPosition = Vector3.zero;
            flower.transform.localRotation = Quaternion.identity;
            float s = 1f;
            flower.transform.localScale = new Vector3(s, s, s);
        }
        return;
    }
}
