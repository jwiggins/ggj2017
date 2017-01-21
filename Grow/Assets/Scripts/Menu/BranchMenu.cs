﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BranchMenu : MonoBehaviour {
    private static BranchMenu _currentMenu;

    [SerializeField]
    private RectTransform _choices;
    [SerializeField]
    private Button _accept;
    [SerializeField]
    private Button _cancel;
    [SerializeField]
    private Image _infoPanel;
    [SerializeField]
    public MenuChoiceBranchReinforce _choiceBranchReinforce;
    [SerializeField]
    public MenuChoiceBranchDrop _choiceBranchDrop;
    [SerializeField]
    public MenuChoiceBranchGrow _choiceBranchGrow;
    [SerializeField]
    public MenuChoiceBranchSplit _choiceBranchSplit;
    [SerializeField]
    public MenuChoiceBranchDoubleSplit _choiceBranchDoubleSplit;
    [SerializeField]
    public MenuChoiceFlowerDrop _choiceFlowerDrop;
    [SerializeField]
    public MenuChoiceFlowerGrow _choiceFlowerGrow;
    [SerializeField]
    public MenuChoiceFruitDrop _choiceFruitDrop;
    [SerializeField]
    public MenuChoiceFruitGrow _choiceFruitGrow;
    [SerializeField]
    public MenuChoiceLeafDrop _choiceLeafDrop;
    [SerializeField]
    public MenuChoiceLeafGrow _choiceLeafGrow;

    private AttachmentPoint _attachPoint;
    private MenuChoice _activeChoice;

    private Camera _camera;

    public MenuChoice ActiveChoice
    {
        get { return this._activeChoice; }
        set {
            this._activeChoice = value;
            this._infoPanel.color = value.GetComponent<Image>().color;
        }
    }

    public AttachmentPoint AttachPoint
    {
        get
        {
            return this._attachPoint;
        }
        set
        {
            this._attachPoint = value;
        }
    }

    private Vector3 ViewAllVector
    {
        get
        {
            Plant plant = this.AttachPoint.Plant;
            PlantModule[] modules = plant.children;
            float minY = 0;
            float maxY = 0;
            foreach(PlantModule plantModule in modules)
            {
                minY = Mathf.Min(minY, plantModule.transform.position.y);
                maxY = Mathf.Max(maxY, plantModule.transform.position.y);
            }
            float frustumHeight = maxY+1 - minY;
            float middle = frustumHeight / 2f;
            float distance = (frustumHeight+3) * 0.5f / Mathf.Tan(GameObject.Find("Camera").GetComponent<Camera>().fieldOfView * 0.5f * Mathf.Deg2Rad);
            return new Vector3(0, middle+.5f, distance);
        }
    }

    void Start()
    {
        this._camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 position = _camera.WorldToScreenPoint(this.AttachPoint.transform.position);
        int menuWidth = 300;
        if (position.x < menuWidth / 2) position.x = menuWidth / 2;
        if (position.x > Screen.width - menuWidth / 2) position.x = Screen.width - menuWidth / 2;
        int menuHeight = 200;
        if (position.y < menuHeight / 2) position.y = menuHeight / 2;
        if (position.y > Screen.height - menuHeight / 2) position.y = Screen.height - menuHeight / 2;
        this.transform.position = position;
    }

    public void accept()
    {
        if (this.ActiveChoice != null)
        {
            this._activeChoice.execute();
        }
        this.zoomOut();
        this.end();
    }
    public void cancel()
    {
        this.zoomOut();
        this.end();
    }
    private void zoomOut()
    {
        this.AttachPoint.Plant.StartCoroutine(moveCameraTo(ViewAllVector));
    }
    private void end()
    {
        GameObject.Destroy(this.gameObject);
    }

    public static BranchMenu create(AttachmentPoint attachmentPoint)
    {
        if (_currentMenu != null)
        {
            _currentMenu.end();
        }
        BranchMenu result = GameObject.Instantiate(attachmentPoint.Plant.BranchMenu);
        result._attachPoint = attachmentPoint;
        result.transform.SetParent(GameObject.Find("Canvas").transform);
        _currentMenu = result;
        bool assignedChoices = attachmentPoint.AssignMenuChoices(result);
        if (!assignedChoices)
        {
            Debug.Log("No actions found.");
            GameObject.Destroy(result.gameObject);
            return null;
        }
        result._accept.onClick.AddListener(() => { result.accept(); });
        result._cancel.onClick.AddListener(() => { result.cancel(); });
        attachmentPoint.StartCoroutine(BranchMenu.moveCameraTo(attachmentPoint.transform.position + new Vector3(0, 0, 1.75f)));
        return result;
    }

    public void setMenuChoices(MenuChoice[] choices)
    {
        for(int i = 0; i < choices.Length; i++)
        {
            MenuChoice choice = choices[i];
            choice.BranchMenu = this;
            RectTransform rectTransform = choice.GetComponent<RectTransform>();
            rectTransform.SetParent(this._choices);
            rectTransform.anchorMin = new Vector2(0.1f + i * 0.2f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.1f + i * 0.2f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
    private static IEnumerator moveCameraTo(Vector3 targetPosition)
    {
        Transform cameraTransform = GameObject.Find("Camera").transform;
        Vector3 startPosition = cameraTransform.position;
        float t = 0;
        float totalTime = 1;
        while (t < totalTime)
        {
            t += Time.deltaTime;
            cameraTransform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0,1,t));
            yield return null;
        }
    }
}
