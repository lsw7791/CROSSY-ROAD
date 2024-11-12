using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public Text cherryTxt;
    public Text currentScore;
    public Text highScore;
    public GameObject uiGameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 새롭게 로드된 씬에서 필요한 UI 오브젝트를 찾기
        cherryTxt = GameObject.Find("CherryTxt")?.GetComponent<Text>();
        currentScore = GameObject.Find("ScoreTxt")?.GetComponent<Text>();
        highScore = GameObject.Find("HighScoreTxt")?.GetComponent<Text>();
        uiGameOver = GameObject.Find("UIGameOver");

        //초기화 
        DataManager.Instance.score = 0;
        Time.timeScale = 1;

        if (uiGameOver != null)
        {
            uiGameOver.SetActive(false);
        }

        if (highScore != null)
        {
            highScore.text = DataManager.Instance.highScoreValue.ToString();
        }
    }

    void Update()
    {
        if (cherryTxt != null)
            cherryTxt.text = DataManager.Instance.cherryCount.ToString();

        if (currentScore != null)
            currentScore.text = DataManager.Instance.score.ToString();

        if (highScore != null)
            highScore.text = DataManager.Instance.highScoreValue.ToString();
    }
}
