using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    ClickPower, PerSecondIncrease
};

public class StoreItem : MonoBehaviour
{
    [Tooltip("이 업그레이드의 가격이 얼마인지")]
    public int cost;

    public ItemType itemType;

    [Tooltip("구매한다면 얼마나 증가될것인지")]
    public float increaseAmount;

    private int qty;

    public Text costText;
    public Text qtyText;

    private GameController controller;
    private Button button;

    // Use this for initialization
    void Start()
    {
        qty = 0;
        qtyText.text = "$" + cost.ToString();

        button = transform.GetComponent<Button>();
        button.onClick.AddListener(this.ButtonClicked);
        controller = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = (controller.Cash >= cost);
    }

    public void ButtonClicked()
    {
        controller.Cash -= cost;
        switch (itemType)
        {
            case ItemType.ClickPower:
                controller.cashPerClick += increaseAmount;
                break;
            case ItemType.PerSecondIncrease:
                controller.CashPerSecond += increaseAmount;
                break;
        }
        qty++;
        qtyText.text = qty.ToString();
    }
}
