using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public string levelToLoad;

    private void Awake()
    {
        Screen.SetResolution(1280, 720, true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void OnLoadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
    }
}
