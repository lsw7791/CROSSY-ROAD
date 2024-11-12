using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBtn : MonoBehaviour
{
    public void OnClickedStartBtn()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickedExitBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("IntroScene");
    }

    public void OnClickedRetryBtn()
    {
        SceneManager.LoadScene("GameScene");
    }

}
