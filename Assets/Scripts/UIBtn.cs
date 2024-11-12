using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBtn : MonoBehaviour
{
    public void OnClickedStartBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickedExitBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("IntroScene");
    }

    public void OnClickedRetryBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
}
