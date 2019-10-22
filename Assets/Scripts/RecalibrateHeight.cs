using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Script to help calibrate the game to better accomodate players of different
 * heights, based on the HMD height as well as the distance from the HMD to
 * the handheld contrllers.
 */
public class RecalibrateHeight : MonoBehaviour
{
    // gameObject reference to CenterEyeAnchor, part of OVRPlayerController
    public GameObject centerEyeAnchor;
    // gameObject references to left hand and right hand controllers
    public GameObject leftHand, rightHand;
    // gameObject reference to dominant hand
    private GameObject handController;
    // floats to track HMD height, as well as dominant arm length.
    private float height, armLength;
    // Reference to text objects to display height and arm length
    public TextMeshProUGUI heightDisp, armLengthDisp;
    // Gameobject reference to LevelHeightScaler object
    public GameObject levelScaler;
    // bool to prevent multiple projectile spawns
    private bool projectileSpawned;
    // Boolean to stop accepting new data for height and arm length
    private bool calibrationComplete; 

    // Start is called before the first frame update
    void Start()
    {
        // Turns VR back on from title scene
        UnityEngine.XR.XRSettings.enabled = true;
        // Setting up values for calibration scene
        if (GlobalControl.Instance.isRightHanded)
        {
            handController = rightHand;
        }
        else
        {
            handController = leftHand;
        }
        calibrationComplete = false;
        projectileSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        heightDisp.text = "HMD Height = " + height;
        if (GlobalControl.Instance.isRightHanded)
        {
            armLengthDisp.text = "Right Arm Length = " + armLength;
        }
        else
        {
            armLengthDisp.text = "Left Arm Length = " + armLength;
        }
        // Taking values while calibration is not yet complete
        if (!calibrationComplete)
        {
            height = centerEyeAnchor.transform.position.y;
            armLength = handController.transform.position.y;
            GlobalControl.Instance.armLength = armLength;
            GlobalControl.Instance.height = height;
            levelScaler.GetComponent<LevelHeightScale>().AdjustTarget();
            levelScaler.GetComponent<LevelHeightScale>().AdjustPlatform();
        }
        // If operator presses RightShift, or if player presses Y, the calibration completes and values are stored
        if (OVRInput.GetUp(OVRInput.RawButton.Y) || (Input.GetKeyUp(KeyCode.RightShift)) && !calibrationComplete)
        {
            calibrationComplete = true;
            GlobalControl.Instance.armLength = armLength;
            GlobalControl.Instance.height = height;
        }
        // Spawns a projectile once
        if (calibrationComplete && !projectileSpawned)
        {
            levelScaler.GetComponent<LevelHeightScale>().SpawnProjectile();
            projectileSpawned = true;
            Debug.Log("Calibration complete");
        }
        // Loads Classroom level
        if ((OVRInput.GetUp(OVRInput.RawButton.X) || Input.GetKeyUp(KeyCode.KeypadEnter)) && calibrationComplete)
        {
            //SceneManager.LoadScene("Classroom");
            levelScaler.GetComponent<LevelHeightScale>().LoadNextScene();
        }
    }
}
