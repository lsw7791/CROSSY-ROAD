using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject player;
    public int cherryCount;
    public Text cherryTxt;
    public Text currentScore;
    public Text highScore;
    private float highScoreValue;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        highScoreValue = PlayerPrefs.GetFloat("HighScore", 0);
        highScore.text = highScoreValue.ToString("F0");
    }

    void Update()
    {
        cherryTxt.text = cherryCount.ToString();
        float score = Mathf.Max(player.transform.position.z / 2, 0);
        currentScore.text = score.ToString("F0");

        if (score > highScoreValue)
        {
            highScoreValue = score;
            highScore.text = highScoreValue.ToString("F0");

            // 하이스코어 저장
            PlayerPrefs.SetFloat("HighScore", highScoreValue);
            PlayerPrefs.Save();
        }
    }
}
