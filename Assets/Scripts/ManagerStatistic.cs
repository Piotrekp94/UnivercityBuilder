using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStatistic : MonoBehaviour
{
    public static ManagerStatistic Instance { get; private set; }

    public int Students;
    public int Money;
    public int maxStudnetCup;
    public int moneyPerSecond;

    public void Start()
    {
        InvokeRepeating("updateMoney", 10, 10);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool canBuy(int value)
    {
        return Money >= value;
    }
    public void buy(int value)
    {
        Money = Money - value;
    }
    private void updateMoney()
    {
        Money += moneyPerSecond * 10;
    }
    public void updateMoneyPerSecond(int amount)
    {
        moneyPerSecond += amount;
    }

}
