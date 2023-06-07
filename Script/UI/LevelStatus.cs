using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStatus : MonoBehaviour
{
    public CharacterStats character;
    public Text LevelText;

    private void Start()
    {
        character = PlayerManager.instance.player.GetComponent<CharacterStats>();
    }

    void Update()
    {
        LevelText.text = "Level\n" + character.Level.ToString();
    }
}
