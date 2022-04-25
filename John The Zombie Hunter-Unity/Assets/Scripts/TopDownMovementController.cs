/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 20, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 20, 2022
 * 
 * Description: Movement controller and trigger handler
****/

using System.Collections.Generic;
using UnityEngine;

public class TopDownMovementController : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 4; // Speed of our player
    [SerializeField] private Animator m_animator = null; // animation controller for our player

    private float m_currentV = 0; // Vertical axis entry
    private float m_currentH = 0; // Horizontal axis entry

    private readonly float m_interpolation = 5; // Smoothen our movement

    private Plane playerMovementPlane; // Used for calculating rotation of our player
    private Vector3 playerToMouse; // Used for finding our mouse relative to player

    // Treat the spawn plane as our movement area for mouse positioning
    private void Awake()
    {
        playerMovementPlane = new Plane(transform.up, transform.position + transform.up);
    }

    // Main logic for the movemnet
    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical"); // Gather inputs V
        float h = Input.GetAxis("Horizontal"); // Gather inputs X
        
        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation); // Make sure smooth movement
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation); // Same, but on different axis

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime; // Take our frontal movements relative to player
        transform.position += transform.right * m_currentH * m_moveSpeed * Time.deltaTime * .75f; // Our right/left movements dampened to player

        Vector3 cursorScreenPosition = Input.mousePosition; // Find our mouse

        Vector3 cursorWorldPosition = ScreenPointToWorldPointOnPlane(cursorScreenPosition, playerMovementPlane, Camera.main); // Apply camera transform to find the world positon

        playerToMouse = cursorWorldPosition - transform.position; // Relative to player find the position

        playerToMouse.y = 0f;

        playerToMouse.Normalize();

        transform.rotation = Quaternion.LookRotation(playerToMouse); // Rotate our player in the mouse direction
        m_animator.SetFloat("InputX", m_currentH); // Apply our horizontal movement animations
        m_animator.SetFloat("InputY", m_currentV); // Apply our vertical movement animations
    }

    // Taken from Unity public repo, used for determining the plane intersection (mouse world)
    Vector3 PlaneRayIntersection(Plane plane, Ray ray)
    {
        float dist = 0.0f;
        plane.Raycast(ray, out dist);
        return ray.GetPoint(dist);
    }

    // Take our mouse and find the correct position on the base plane
    Vector3 ScreenPointToWorldPointOnPlane(Vector3 screenPoint, Plane plane, Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(screenPoint);
        return PlaneRayIntersection(plane, ray);
    }

    // Handle the behavior of collecting chests
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Chest"))
        {
            GameManager.GM.ObjectiveCaptured();
            if (GameManager.GM.totalObjectives - GameManager.GM.objectivesCaptured == 0)
                GameManager.GM.gameState = GameState.BeatLevel;

            Destroy(other.gameObject);
        }
    }
}
