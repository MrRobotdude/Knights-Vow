using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public int exp;
    public float[] position;
    public  int potion;

    public PlayerData(CharacterStats player)
    {
        level = player.Level;
        Transform transform = PlayerManager.instance.player.GetComponent<Transform>();
        health = player.maxHealth;
        exp = player.currentExp;
        position = new float[3];
        position[0] = transform.position.x;
        position[1] = transform.position.y;
        position[2] = transform.position.z;
        potion = player.potion;
    }
}
