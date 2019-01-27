using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHP = 100;
    public Slider hPSlider;

    public delegate void HappinessExpireEvent();
    public static event HappinessExpireEvent HPExpired;

    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
        //hPSlider.value = currentHP;
    }

    public void DealDamage(int i)
    {
        currentHP -= i;
        hPSlider.value = currentHP;

        if (currentHP <= 0)
        {
            Debug.Log("YOU DIE OF UNHAPPINESS");
            HPExpired();
        }
    }

    public void HealDamage(int i)
    {
        currentHP += i;
    }
}
