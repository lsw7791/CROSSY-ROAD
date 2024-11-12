using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            UIManager.Instance.uiGameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
