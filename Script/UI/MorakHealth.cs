using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorakHealth : MonoBehaviour
{

    public Slider healthSlider;
    public Slider expSlider;
    public Text healthText;
    public Text expText;
    public Text Level;
    public MorakStats morakStats;

    public void Start()
    {
        healthText = GetComponent<Text>();
        expText = GetComponent<Text>();
        morakStats = GetComponent<MorakStats>();
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
}
