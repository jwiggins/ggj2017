using System.Collections;
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

    public void accept()
    {
        if (this.ActiveChoice != null)
        {
            this._activeChoice.execute();
            this.end();
        }
    }
    public void cancel()
    {
        this.end();
    }
    private void end()
    {
        GameObject.Destroy(this.gameObject);
    }

    public static BranchMenu create(AttachmentPoint attachmentPoint)
    {
        if (_currentMenu != null)
            _currentMenu.cancel();
        Camera camera = GameObject.Find("Camera").GetComponent<Camera>();
        BranchMenu result = GameObject.Instantiate(attachmentPoint.Plant.BranchMenu);
        result._attachPoint = attachmentPoint;
        result.transform.SetParent(GameObject.Find("Canvas").transform);
        Vector3 position = camera.WorldToScreenPoint(attachmentPoint.transform.position);
        int menuWidth = 300;
        if (position.x < menuWidth/2) position.x = menuWidth/2;
        if(position.x > Screen.width- menuWidth/2) position.x = Screen.width- menuWidth/2;
        int menuHeight = 200;
        if (position.y < menuHeight / 2) position.y = menuHeight / 2;
        if (position.y > Screen.height - menuHeight / 2) position.y = Screen.height - menuHeight / 2;
        result.transform.position = position;
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
}
