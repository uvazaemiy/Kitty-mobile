using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Change(string SceneName)
    { 
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName);
    }
}
