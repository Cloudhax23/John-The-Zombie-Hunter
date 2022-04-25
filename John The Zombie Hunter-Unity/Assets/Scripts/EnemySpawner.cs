/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 22, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 22, 2022
 * 
 * Description: Handles the spawning of multiple enemies
****/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ZombiePrefab; // Take our zombie asset
    public Transform player; // Need to make sure the player is available for distance
    public int counter = 0; // How many were spawned
    public int zombies_per_wave = 50; // How many to spawn
    public int min_distance_to_player = 15; // Give the player some space
    public Vector2 max_zombie_per_floor = new Vector2(2, 8); // Randomize it on a per-floor basis.

    // Update is called once per frame
    // Spawn zombies away from the player in randomly assorted segments
    void Update()
    {
        if (counter != 0) return;
        
        List<GameObject> floorsAtDistance = GameObject.FindGameObjectsWithTag("Floor")
            .Where(x => Vector3.Distance(player.position, x.transform.position) >= min_distance_to_player)
            .OrderBy(x => Random.value).ToList(); // Find floors and randomize them away from player
  
        int remaining_zombies = zombies_per_wave;

        foreach(GameObject floor in floorsAtDistance) // Try to loop valid floors for spawning
        {
            if (remaining_zombies == 0) return;

            int spawn_amount = (int)Mathf.Min(remaining_zombies, Random.Range(max_zombie_per_floor.x, max_zombie_per_floor.y)); // Pick the smaller of two spawn amounts
            for (int i = 0; i < spawn_amount; i++)
            {
                counter++;
                GameObject enemy = Instantiate(ZombiePrefab, floor.transform.position, Quaternion.identity);
                EnemyAI npc = enemy.GetComponent<EnemyAI>();
                npc.playerTransform = player; // Make sure our AI can path to player
                npc.spawner = this;
            }
            remaining_zombies -= spawn_amount; // Remove the amount spawned
        }
    }

    public void UpdateCounter()
    {
        counter--; // Called when a zombie dies to allow eventual respawning
    }
}
