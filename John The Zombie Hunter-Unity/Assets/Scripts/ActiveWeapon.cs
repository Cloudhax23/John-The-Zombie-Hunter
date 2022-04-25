/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 23, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 23, 2022
 * 
 * Description: Handles the functionality of the actively equiped weapon
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Animations; Used for generating animation rig constants

public class ActiveWeapon : MonoBehaviour
{
    public UnityEngine.Animations.Rigging.Rig handIK; //Ref for the IK hand layer
    private Weapon currentWeapon; //Ref to the weapon we have equiped 
    public Transform weaponLeftGrip; //Left grip transform
    public Transform weaponRightGrip; //Right grip transform
    public Transform weaponParent; //WeaponHolder associated with the weapons available
    private Animator animator; //Our animations
    private AnimatorOverrideController animatorOverride; //Override if we hold a weapon
    public GameObject player; //Ref to our player

    // Start is called before the first frame update
    // Gather animator components
    void Awake()
    {
        animator = GetComponent<Animator>();
        animatorOverride = animator.runtimeAnimatorController as AnimatorOverrideController;
    }
     
    // Update is called once per frame
    // Make our animations weighted if holding a weapon
    // TODO: implement weapon switching, backbone in place
    void FixedUpdate()
    {
        if (currentWeapon)
        {
            handIK.weight = 1f;
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            handIK.weight = 0f;
            animator.SetLayerWeight(1, 0);
        }

        if (currentWeapon && Input.GetButton("Fire1"))
            currentWeapon.Shoot();
    }

    // Called when player interacts with the trigger of weapon
    public void Equip(Weapon newWeapon)
    {
        if(currentWeapon)
            Destroy(currentWeapon.gameObject);
        
        currentWeapon = Instantiate(newWeapon, weaponParent);
        Invoke(nameof(SetOverrideAnim), .001f);
    }

    // Prevent a crash related to unity animation switching
    void SetOverrideAnim()
    {
        animatorOverride["weapon_empty"] = currentWeapon.weaponAnimationClip;
    }

    // How we can save our animation positions for a variety of weapons
    #region Animation saving
    /***
    [ContextMenu("Save pose")]
    void SaveWeaponPos()
    {
        GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
        recorder.BindComponentsOfType<Transform>(weaponParent.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponLeftGrip.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponRightGrip.gameObject, false);
        recorder.TakeSnapshot(0f);
        recorder.SaveToClip(currentWeapon.weaponAnimationClip);
        UnityEditor.AssetDatabase.SaveAssets();
    }
    ***/
    #endregion
}
