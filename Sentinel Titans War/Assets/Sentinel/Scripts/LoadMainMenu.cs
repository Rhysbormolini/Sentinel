using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gamekit3D;

public class LoadMainMenu : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene(0);
        ProjectileTrigger.ballUnlocked = false;
        ProjectileTrigger.stormUnlocked = false;
        ProjectileTrigger.tornadoUnlocked = false;
    }
}
