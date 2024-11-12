using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameObject player;
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
        player = FindObjectOfType<Player>()?.gameObject;

        if (score > highScoreValue)
        {
            highScoreValue = score;

            // 하이스코어 저장
            PlayerPrefs.SetFloat("HighScore", highScoreValue);
            PlayerPrefs.Save();
        }
    }
    private void FixedUpdate()
    {
        score = (int)Mathf.Max(player.transform.position.z / 2, 0);
    }
}
