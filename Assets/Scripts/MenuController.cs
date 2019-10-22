 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Script for Menu scene, when player info is being entered
public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Toggle rightHandToggle;
    // Start is called before the first frame update
    void Start()
    {
        // disable VR settings for menu scene
        UnityEngine.XR.XRSettings.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeText()
    {
        text.text = "Success!";
    }

    public void NextScene()
    {
        GlobalControl.Instance.isRightHanded = rightHandToggle.enabled;
        SceneManager.LoadScene("Calibration");
    }
}
