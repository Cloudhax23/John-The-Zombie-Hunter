/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 22, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 22, 2022
 * 
 * Description: Handles the visualization of elements by spinning them around
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    // Rotate the attached object by 20 degrees every game tick
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime);
    }
}
