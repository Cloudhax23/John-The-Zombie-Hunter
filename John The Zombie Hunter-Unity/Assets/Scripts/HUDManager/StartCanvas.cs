/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 23, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 23, 2022
 * 
 * Description: Handles the appearance of items on the start game screen
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //libraries for UI components

public class StartCanvas : MonoBehaviour
{
    /*** VARIABLES ***/

    GameManager gm; //reference to game manager

    private void Start()
    {
        gm = GameManager.GM; //find the game manager
    }

    public void GameStart()
    {
         gm.StartGame(); //refenece the StartGame method on the game manager
    }

    public void GameExit()
    {
        gm.ExitGame(); //refenece the ExitGame method on the game manager
    }
}