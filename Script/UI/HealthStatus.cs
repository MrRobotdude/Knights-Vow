using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatus : MonoBehaviour
{
    public CharacterStats character;
    public Text HealthText;

    private void Start()
    {
        character = PlayerManager.instance.player.GetComponent<CharacterStats>();
    }

    void Update()
    {
        HealthText.text = CharacterStats.currentHealth.ToString() + "/" + character.maxHealth.ToString();
    }
}
