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
    public TMP_Dropdown chooseMode;
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

    GlobalControl.ProgressionType ProgressionConvert(int value)
    {
        if (value == 0) {
            return GlobalControl.ProgressionType.Performance;
        }
        if (value == 1) {
            return GlobalControl.ProgressionType.Random;
        }
        else { 
            return GlobalControl.ProgressionType.Choice;
        }
    }

    // Progresses to next scene, setting values in GlobalControl
    public void NextScene()
    {
        GlobalControl.Instance.progression = ProgressionConvert(chooseMode.value);
        GlobalControl.Instance.isRightHanded = rightHandToggle.enabled;
        SceneManager.LoadScene("Calibration");
    }
}
