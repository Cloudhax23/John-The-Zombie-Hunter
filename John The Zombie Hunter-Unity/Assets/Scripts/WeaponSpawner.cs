/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 21, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 22, 2022
 * 
 * Description: Handles the randomly spawned weapons
****/

using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public Weapon[] weaponsPrefabs; // Whatever weapons we want a drop chance to be
    public GameObject spherePrefab; // The spawn object
    public Transform player; // Player for distances
    public int counter = 0; // How many we spawned
    public int weapons_spawn_amount = 10; // Total to spawn
    public int max_distance_to_player = 25; // How far we want them to be
    public Vector2 max_weapon_per_floor = new Vector2(1, 1); // Multiple floor drops

    // Find the closest sphere or infinity if none
    float GetClosestSphereDistance(Vector3 position)
    {
        return GameObject.FindGameObjectsWithTag("WeaponPicker").Length > 0 ? GameObject.FindGameObjectsWithTag("WeaponPicker").Select(x => Vector3.Distance(position, x.transform.position)).Min() : Mathf.Infinity;
    }

    // Make sure its not close to an objective
    float GetClosestChestDistance(Vector3 position)
    {
        return GameObject.FindGameObjectsWithTag("Chest").Select(x => Vector3.Distance(position, x.transform.position)).Min();
    }

    // Handle weapon drop
    private void Update()
    {
        if (counter != 0) return;
        
        // Find valid floors with relavent distance
        List<GameObject> floorsAtDistance = GameObject.FindGameObjectsWithTag("Floor")
        .Where(x => Vector3.Distance(player.position, x.transform.position) <= max_distance_to_player && GetClosestChestDistance(x.transform.position) > 5).OrderBy(x => Random.value).ToList();

        int remaining_weapons = weapons_spawn_amount;
        foreach (GameObject floor in floorsAtDistance) // Eval floors available
        {
            if (remaining_weapons == 0) return;
            if (GetClosestSphereDistance(floor.transform.position) < 10) continue; // Make sure the last drop is far

            counter++; // Increment the spawn
            GameObject weapon_holder = Instantiate(spherePrefab, new Vector3(floor.transform.position.x, 1f, floor.transform.position.z), Quaternion.identity);
            weapon_holder.GetComponent<WeaponPickup>().weaponPrefab = weaponsPrefabs[Random.Range(0, weaponsPrefabs.Length)]; // Random prefab for the spawn
        
            remaining_weapons--; // Decrement available guns
        }
    }
}
