using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * Script tied to hand object for purposes of resetting positions of any and all projectiles in the scene.
 */
public class ProjectileManager : MonoBehaviour
{
    // Arrays to hold references to all projectile objects, their beginning positions and rotations
    public GameObject[] projectiles;
    private Vector3[] transforms;
    private Quaternion[] rotations;
    // Reference to TMPro object displaying the result of the throw
    //public TextMeshProUGUI distAwayText;
    // Reference to LogManager object in scene
    public GameObject logManager;

    // Start is called before the first frame update
    void Start()
    {
        // Find all projectile objects in the scene
        projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        //Debug.Log(projectiles.Length);
        AdditionalArrays();
    }

    // Sets up additional arrays to mirror projectile array
    public void AdditionalArrays()
    {
        // Establish the position and rotation arrays to fit the number of projectiles
        transforms = new Vector3[projectiles.Length];
        rotations = new Quaternion[projectiles.Length];
        // Populate the arrays with each projectile's position and rotation
        for (int i = 0; i < projectiles.Length; i++)
        {
            transforms[i] = projectiles[i].transform.position;
            rotations[i] = projectiles[i].transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When the player presses the "A" button on the right hand controller
        if (OVRInput.GetUp(OVRInput.RawButton.A))
        {
            Debug.Log("Player reset");
            // Call the ResetPositions() function
            ResetPositions();
        }
        // When the operator presses the space bar
        if (Input.GetKeyUp(KeyCode.Return))
        {
            Debug.Log("Operator reset");
            // Call the ResetPositions() function
            ResetPositions();
        }
    }

    // Function to reset every projectile to its original place
    public void ResetPositions() {
        // Increment through every projectile in the projectiles array
        for (int i = 0; i < projectiles.Length; i++)
        {
            // Reset its position and rotation to match the corresponding arrays
            projectiles[i].transform.position = transforms[i];
            projectiles[i].transform.rotation = rotations[i];
            // Reset its velocity to 0, no momentum
            projectiles[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            projectiles[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            // Tell the projectile to run its HasReset() function to mark it as ready to track again
            //projectiles[i].SendMessage("HasReset");
            projectiles[i].GetComponent<GroundChecker>().tracking = true;
            // Tell the projectile's corresponding target to run its ResetDistText() function to reset 
            // the TMPro GUI object for the next throw
            projectiles[i].GetComponent<GroundChecker>().target.SendMessage("ResetDistText");
        }
        // Add reset data to result text file
        logManager.GetComponent<LogTestResults>().AddText("Reset for next throw.");
    }
}
