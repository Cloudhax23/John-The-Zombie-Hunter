/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 23, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 23, 2022
 * 
 * Description: Manages the appearance of items on the in game screen
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuHandler : MonoBehaviour
{

    GameManager gm; //reference to game manager

    [Header("Canvas Settings")]
    public TMP_Text killScoreUI;
    public TMP_Text bestScoreUI;
    public TMP_Text objectivesRemainingUI;

    private void Start()
    {
        gm = GameManager.GM;
    }

    // Update is called once per frame
    // Update our scores/objectives
    void Update()
    {
        killScoreUI.text = "Kills: " + gm.Score;
        bestScoreUI.text = "Best: " + gm.HighScore;
        objectivesRemainingUI.text = "Chests Remaining: " + (gm.totalObjectives - gm.objectivesCaptured);
    }
}
