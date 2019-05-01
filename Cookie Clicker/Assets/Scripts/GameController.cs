using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private float _cash;
    public float Cash
    {
        get { return _cash; }
        set
        {
            _cash = value;
            cashText.text = "You Have: $" + _cash.ToString("0.00");
        }
    }

    private float _cashPerSecond;
    public float CashPerSecond
    {
        get { return _cashPerSecond; }
        set
        {
            _cashPerSecond = value;
            rateText.text = "per Second: " + _cashPerSecond.ToString("0.0");
        }
    }
    [Tooltip("버튼을 누를 때마다 플레이어가 벌어들이는 돈.")]
    public float cashPerClick = 1;

    [Header("Object References")]
    public Text cashText;
    public Text rateText;

    private void Start()
    {
        _cash = 0;
        _cashPerSecond = 0;
    }

    private void Update()
    {
        Cash += CashPerSecond * Time.deltaTime;
    }

    public void ClickButton()
    {
        Cash += cashPerClick;
    }
}
	

