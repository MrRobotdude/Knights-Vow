using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BanditStatus : MonoBehaviour
{
    public Text text;
    public MorakStats stat;
    public Slider healthSlider;

    public Text Level;

    // Start is called before the first frame update
    void Start()
    {
        stat = GetComponentInParent<MorakStats>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Bandit\nLevel " + stat.Level.ToString();
        maxHealth(stat.maxHealth);
        currHealth(stat.currentHealth);
    }

    public void maxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
    }

    public void currHealth(int currHealth)
    {
        healthSlider.value = currHealth;
    }

}
