/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 22, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 22, 2022
 * 
 * Description: Handles the appearance of 3D health bar on any entity
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // A fille based slider
    [SerializeField] private GameObject healthBarUI; // Ref to our UI element

    // Set or health visually
    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    // Update the view of the health bar 
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }

    // Disable render if dead/any other reason
    public void Disable()
    {
        healthBarUI.SetActive(false);
    }
}
