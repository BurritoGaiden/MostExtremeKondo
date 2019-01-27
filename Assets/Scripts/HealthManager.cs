using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHP = 100;
    //public Slider hPSlider;
    public Image hpBar;

    public delegate void HappinessExpireEvent();
    public static event HappinessExpireEvent HPExpired;

    private int currentHP;


    void Start()
    {
        currentHP = maxHP;
        hpBar.fillAmount = currentHP *.01f;
        //hpBar.fillAmount = .5f;
        //hPSlider.value = currentHP;
    }

    private void Update()
    {
        
    }

    public void DealDamage(int i)
    {
        currentHP -= i;
        Debug.Log("current health is: " + currentHP);
        hpBar.fillAmount = currentHP *.01f;
        //hPSlider.value = currentHP;

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

    public bool isAlive()
    {
        if (currentHP > 0)
        {
            return true;
        }

        return false;
    }
}
