using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;  

    private void Update()
    {
        if (transform.position.y < 0)
        {
            UIManager.Instance.uiGameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            UIManager.Instance.uiGameOver.SetActive(true);
            Time.timeScale = 0;
        }

        if (other.CompareTag("Cherry"))
        {
            DataManager.Instance.cherryCount++;
            Destroy(other.gameObject);
        }
    }
}
