using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChoiceFruitGrow : MenuChoice
{

    public override void execute()
    {
        AttachmentPoint attachPoint = this.AttachPoint;
        foreach (Transform transform in attachPoint.transform)
        {
            if (!transform.name.ToUpper().Contains("FLOWER")) continue;
            if (transform.childCount == 0) continue;
            transform.GetChild(0).gameObject.SetActive(false);
            GameObject fruit = GameObject.Instantiate(attachPoint.Plant.PlantFruit);
            fruit.transform.SetParent(transform);
            fruit.transform.localPosition = Vector3.zero;
            fruit.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            float s = 100f;
            fruit.transform.localScale = new Vector3(s, s, s);
        }
        return;
    }
}
