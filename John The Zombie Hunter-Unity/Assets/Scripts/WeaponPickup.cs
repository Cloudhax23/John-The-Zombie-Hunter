/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 21, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 22, 2022
 * 
 * Description: Handles the spheres with guns in them
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weaponPrefab; // Whatever gun we want in the sphere set by spawner

    private void Start()
    {
        Weapon weapon_preview = Instantiate(weaponPrefab); // Generate the object
        weapon_preview.transform.parent = transform; // Make the parent the sphere
        weapon_preview.transform.localPosition = Vector3.zero; // Reset its position relative to sphere
    }

    // Handle the effects of walking into the sphere for a player
    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if(activeWeapon)
        {
            activeWeapon.Equip(weaponPrefab); // Equip the gun if we are a player
            Destroy(gameObject); // Destroy our sphere
        }
    }
}
