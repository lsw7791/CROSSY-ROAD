using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; 
    public float spawnInterval;

    private List<Transform> groundObjects = new List<Transform>(); 
    private float timeSinceLastSpawn = 0f; 

    private Dictionary<Transform, float> lastSpawnedPositions = new Dictionary<Transform, float>(); // 각 Ground 위치에서 마지막 자동차 생성 위치 추적

    void Update()
    {
        // "Ground" 태그를 가진 모든 오브젝트들 업데이트
        UpdateGroundObjects();

        // 일정 시간이 지난 후 자동차 생성
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnCars();
            timeSinceLastSpawn = 0f; // 주기 초기화
        }
    }

    // "Ground" 태그를 가진 오브젝트들을 찾아서 리스트에 갱신
    void UpdateGroundObjects()
    {
        GameObject[] newGroundObjects = GameObject.FindGameObjectsWithTag("Ground");
        foreach (var ground in newGroundObjects)
        {
            if (!groundObjects.Contains(ground.transform))
            {
                groundObjects.Add(ground.transform);

                // 새로 추가된 Ground 오브젝트의 X 좌표를 -20 또는 20으로 지정
                float initialCarXPosition = (ground.transform.position.x == -20 || ground.transform.position.x == 20)
                    ? ground.transform.position.x
                    : Random.Range(0, 2) == 0 ? -20 : 20;

                lastSpawnedPositions[ground.transform] = initialCarXPosition; // 초기 위치 설정
            }
        }

        for (int i = groundObjects.Count - 1; i >= 0; i--)
        {
            if (groundObjects[i] == null)
            {
                lastSpawnedPositions.Remove(groundObjects[i]);
                groundObjects.RemoveAt(i);
            }
        }
    }

    void SpawnCars()
    {
        foreach (Transform ground in groundObjects)
        {
            // 해당 Ground에서 마지막으로 생성된 자동차 위치
            float lastCarPositionX = lastSpawnedPositions[ground];

            // 새로운 자동차를 마지막으로 생성된 위치에서만 생성하도록 함
            Vector3 carPosition = new Vector3(lastCarPositionX, ground.position.y + 1.3f, ground.position.z); 

            GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
            Instantiate(carPrefab, carPosition, Quaternion.identity);

            lastSpawnedPositions[ground] = lastCarPositionX; // (계속 같은 위치에서만 생성)
        }
    }
}
