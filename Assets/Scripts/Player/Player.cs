using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        if(player.position.y < 0)
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
    }
}
