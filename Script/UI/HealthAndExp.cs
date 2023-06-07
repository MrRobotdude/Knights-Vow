using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndExp : MonoBehaviour
{

    public Slider healthSlider;
    public Slider expSlider;
    public Text healthText;
    public Text expText;
    public Text Level;
    public CharacterStats playerStats;

    public void Start()
    {
        healthText = GetComponent<Text>();
        expText = GetComponent<Text>();
        playerStats = PlayerManager.instance.player.GetComponent<CharacterStats>();
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    public void SetMaxExp(int exp)
    {
        expSlider.maxValue = exp;
        expSlider.value = 0;
    }

    public void SetExp(int exp)
    {
        expSlider.value = exp;
    }
}
