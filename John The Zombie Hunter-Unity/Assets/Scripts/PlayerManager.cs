/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 21, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 22, 2022
 * 
 * Description: Handles the health associated with the player
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int health = 100; // How much health we have
    public HealthBarPlayer healthBar; // Reference to our health bar

    // Take damage and update our health bar as well as the game state if needed
    public void ApplyDamage(int points)
    {
        health -= points;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            GameManager.GM.gameState = GameState.LostLevel;
        }
    }
}
