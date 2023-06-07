using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionStatus : MonoBehaviour
{
    public CharacterStats character;
    public Text PotionText;

    private void Start()
    {
        character = PlayerManager.instance.player.GetComponent<CharacterStats>();
    }

    void Update()
    {
        PotionText.text = character.potion.ToString();
    }
}
