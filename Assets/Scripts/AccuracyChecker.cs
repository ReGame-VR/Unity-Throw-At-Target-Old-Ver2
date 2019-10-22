using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * Script tied to Target objects for tracking how many times a projectile
 * hits a target, and if it missed, how much it missed by.
 */
public class AccuracyChecker : MonoBehaviour
{
    // int values to track how many times a projectile designed for this target has hit this target or has missed
    private int numHit;
    private int numMiss;
    // Float value to track how far away the projectile landed from the target, in the event of a miss
    private float distAway;
    // References to TextMeshPro objects used to display above the target how many times it's been hit, missed, 
    // and whether the most recent shot hit or how much it missed by
    public TextMeshProUGUI hitCounter;
    //private String hitCounterText;
    public TextMeshProUGUI missCounter;
    //private String missCounterText;
    public TextMeshProUGUI distanceFromTarget;
    // String to allow easier modification of the distanceFromTarget TMPro's text field, since it will flip between
    // having hit and having missed
    private String distanceFromTargetText;
    // Reference to LogManager object in scene
    public GameObject logManager;

    // Start is called before the first frame update
    void Start()
    {
        // ints all start at 0
        numHit = 0;
        numMiss = 0;
        distAway = 0;
        // The distanceFromTarget TMPro will read the following before a projectile has been thrown
        distanceFromTargetText = "Please throw the projectile to begin.";
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the TMPros' text fields to mirror the current numbers of hits and misses, and whether
        // the last shot hit or missed
        hitCounter.text = "Hit: " + numHit;
        missCounter.text = "Missed: " + numMiss;
        distanceFromTarget.text = distanceFromTargetText;
    }

    // Called when another object enters the trigger area of this target object.
    private void OnTriggerEnter(Collider other)
    {
        // If a projectile-tagged object hits the trigger while it is being tracked
        //if (other.gameObject.GetComponent<GroundChecker>().GetTracking() && other.gameObject.CompareTag("Projectile"))
        if (other.gameObject.GetComponent<GroundChecker>().tracking && other.gameObject.CompareTag("Projectile"))
        {
            // Update the hit counter
            numHit += 1;
            // Tell the projectile to invoke its Landed() function
            other.gameObject.SendMessage("Landed");
            // Update the distanceFromTargetText to reflect the successful hit
            distanceFromTargetText = "Target successfully hit!";
            // Add hit data to result text file
            logManager.GetComponent<LogTestResults>().AddText(other.gameObject.name + " successfully hit " + this.name + ".");
        }
    }

    // Function to handle when this target's projectile misses and hits the ground.
    public void Miss(Vector3 pos, GameObject projectile)
    {
        // Updates the miss counter
        numMiss += 1;
        // Calculates how far away the projectile landed from the target
        distAway = Math.Abs(Vector3.Distance(pos, this.transform.position));
        //  Updates the text string to reflect this
        distanceFromTargetText = "You missed by: " + distAway;
        // Add miss data to result text file
        logManager.GetComponent<LogTestResults>().AddText(projectile.name + " missed " + this.name + " by " + distAway + ".");
    }

    // Function to reset the text string when called
    private void ResetDistText()
    {
        distanceFromTargetText = "Please throw the projectile to begin.";
    }
}
