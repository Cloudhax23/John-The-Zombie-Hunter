/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 22, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 22, 2022
 * 
 * Description: Handles the generic gun behavior
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem ShootingSystem; // Generate a muzzle flash
    [SerializeField]
    private Transform fireTransform; // Spawning point of our trail renderer
    [SerializeField]
    private ParticleSystem ImpactParticleSystem; // Eventual impact effect
    [SerializeField]
    private ParticleSystem BloodImpactParticleSystem; // If we hit an enemy, apply this impact
    [SerializeField]
    private TrailRenderer BulletTrail; // A trail and not a physical bullet!
    [SerializeField]
    private float ShootDelay = 0.5f; // Prevent spamming the gun

    public int damage_per_hit = 0; // Apply in editor

    public AnimationClip weaponAnimationClip; // Take our animation determined by gun-type set within prefabs
    private float LastShootTime; // Track our shots
    AudioSource shootingSource; // Sounds for our shooting
    
    private void Awake()
    {
        TryGetComponent(out shootingSource); // If a gun does not have a sound, don't bug
    }

    // Main shooting handler
    public void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time) // Prevent spamming
        {
            if (shootingSource)
                shootingSource.Play(); // If we found a sound, play it

            ShootingSystem.Play(); // Do the muzzle flash 
            Vector3 direction = fireTransform.forward * 100f; // Find some point out 100 units for ray casting to

            if (Physics.Raycast(fireTransform.position, direction, out RaycastHit hit)) // Create a ray and find an intersection if possible
            {
                TrailRenderer trail = Instantiate(BulletTrail, fireTransform.position, Quaternion.identity); // Spawn our bullet trail

                StartCoroutine(SpawnTrail(trail, hit)); // Sub routine handles shot (multiple)

                LastShootTime = Time.time; // Prevent spamming
            }
        }
    }

    // Spawn the trail
    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        // Move our trail as a function of time
        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time); // Continue moving the trail
            time += Time.deltaTime / Trail.time;

            yield return null;
        }

        Trail.transform.position = Hit.point; // Determine the final position of the intersection
        EnemyAI enemy = Hit.transform.gameObject.GetComponent<EnemyAI>(); // Check if its an enemy
        if (enemy)
        {
            enemy.ApplyDamage(damage_per_hit); // Apply damage
            Instantiate(BloodImpactParticleSystem, Hit.point, Quaternion.LookRotation(Hit.normal)); // Make a cool blood splat effect
        }
        else
            Instantiate(ImpactParticleSystem, Hit.point, Quaternion.LookRotation(Hit.normal)); // We probably hit a wall, apply this

        Destroy(Trail.gameObject, Trail.time); // Detroy our object in after some time
    }
}