using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script for checking whether a projectile has hit the ground. Handles whether the projectile
 * has hit the target or has missed the target.
 */
public class GroundChecker : MonoBehaviour
{
    // Reference to target-area object this projectile is meant to be thrown at
    public GameObject target;
    // Private boolean value, true when projectile has yet to be thrown and hit ground, false once it has hit the ground.
    // Turns true upon reset. Exists to prevent it from bouncing or rolling into the goal, or from both hitting and missing in the
    // same toss by traveling through the target-area trigger into the ground and qualifying as a miss.
    //private bool tracking;
    public bool tracking;

    // Start is called before the first frame update
    void Start()
    {
        // On startup, the projectile will be tracked
        tracking = true;
        // Finds target in scene
        target = GameObject.FindGameObjectWithTag("Target");
    }

    // When the projectile comes in contact with another collider
    private void OnCollisionEnter(Collision collision)
    {
        // If the projectile was tracking and hits the ground
        if (tracking && collision.gameObject.CompareTag("Ground"))
        {
            // Tell the target to run its Miss() function with the parameter of the projectile's current position
            //target.SendMessage("Miss", this.transform.position);
            target.GetComponent<AccuracyChecker>().Miss(this.transform.position, this.gameObject);
            // Run the projectile's landed() function
            //Landed();
            tracking = false;
        }
    }

    // Function to let other scripts see if the projectile is still being tracked
    public bool GetTracking()
    {
        return tracking;
    }

    // Marks the projectile as no longer being tracked, has hit the ground
    private void Landed()
    {
        tracking = false;
    }

    // When projectile is reset, this function marks it as ready to be tracked again
    private void HasReset()
    {
        tracking = true;
    }
}
