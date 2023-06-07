using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpStatus : MonoBehaviour
{
    public CharacterStats character;
    public Text ExpText;

    public ExpStatus()
    {
    }

    private void Start()
    {
        character = PlayerManager.instance.player.GetComponent<CharacterStats>();
    }

    void Update()
    {
        ExpText.text = character.currentExp.ToString() + "/" + character.Exp.ToString();
    }
}
