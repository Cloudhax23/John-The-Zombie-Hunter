/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 24, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 24, 2022
 * 
 * Description: Enemies have sounds and this is how they can be invoked
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource[] attackSounds; // Take an array of sounds
    public AudioSource[] searchSounds; // Take an array of locating sounds
    public AudioSource damagedSound; // Damage sound for applied damage
    public float lastSoundTime; // Prevent spam

    // Randomly choose a search sound to play
    public void ApplySearchSound()
    {
        if (lastSoundTime > Time.time) return;
        searchSounds[Random.Range(0, searchSounds.Length)].Play();
        lastSoundTime = Time.time + Random.Range(4, 10);
    }

    // If the AI hits us, play this
    public void ApplyAttackSound()
    {
        attackSounds[Random.Range(0, attackSounds.Length)].Play();
    }
    
    // If the AI is hit, play this sound
    public void ApplyDamageSound()
    {
        damagedSound.Play();
    }
}
