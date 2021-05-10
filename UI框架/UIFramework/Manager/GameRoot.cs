using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameRoot : MonoBehaviour {

    void Start () {
        UIPanelManager panelManager = UIPanelManager.Instance;
        if (SceneManager.GetActiveScene().name == "WriteScene")
        {
            panelManager.PushPanel(UIPanelType.DetectionBody);
        }
        else if (SceneManager.GetActiveScene().name == "StartScene")
        {
            panelManager.PushPanel(UIPanelType.Start);
        }
    }
}
