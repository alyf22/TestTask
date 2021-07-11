using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void onStart(){
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void onExit(){
        Application.Quit();
    }
}
