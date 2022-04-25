/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 23, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 23, 2022
 * 
 * Description: Handles the appearance of items on the end game screen
****/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndCanvas : MonoBehaviour
{
    /*** VARIABLES ***/

    GameManager gm; //reference to game manager
    public TMP_Text titleUI;
    private void Start()
    {
        gm = GameManager.GM; //find the game manager
        titleUI.text = gm.endMsg;
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
