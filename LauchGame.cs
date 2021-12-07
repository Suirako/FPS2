using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LauchGame : MonoBehaviour
{
    void Update(){
        if (Input.GetKeyDown(KeyCode.P)){
            Application.Quit();
        }
    }
    
    public void LaunchGame(){
        SceneManager.LoadScene(1);
    }

    public void ToMenu(){
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
    }
}
