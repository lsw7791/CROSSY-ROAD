using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public GameObject player;
    public GameObject playerPrefab;  
    public int cherryCount;
    public int highScoreValue;
    public int score;

    public static DataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        highScoreValue = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        // 하이스코어 갱신
        if (score > highScoreValue)
        {
            highScoreValue = score;
            PlayerPrefs.SetFloat("HighScore", highScoreValue);
            PlayerPrefs.Save();
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            score = (int)Mathf.Max(player.transform.position.z / 2, 0);
        }
    }
}
