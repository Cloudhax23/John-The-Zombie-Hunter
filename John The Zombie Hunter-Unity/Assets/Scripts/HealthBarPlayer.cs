/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 23, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 23, 2022
 * 
 * Description: Handles the appearance of our 2D health bar on the screen
****/

using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
    public Slider healthSlider; // Health bar 0-100 slider
    public Gradient healthGradient; // From Green -> Yellow -> Red
    public Image Fill; // For changing colors of the slider

    private void Start()
    {
        Fill.color = healthGradient.Evaluate(1f); // Initally the maximal gradient value
    }

    // Updates the visualization of the 2D health bar
    public void SetHealth(int health)
    {
        healthSlider.value = health;
        Fill.color = healthGradient.Evaluate(healthSlider.normalizedValue); // Change color based on health changes
    }
}
