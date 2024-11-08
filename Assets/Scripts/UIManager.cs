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

    }

    void Update()
    {
        cherryTxt.text = cherryCount.ToString();
        float score = Mathf.Max(player.transform.position.z / 2, 0);
        currentScore.text = score.ToString("F0");
    }
}
