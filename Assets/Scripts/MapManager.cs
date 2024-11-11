using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject[] carPrefabs; 
    public Vector3 startPosition;
    public int minConsecutive;
    public int maxConsecutive;
    public Transform player;

    [SerializeField] private float spawnThreshold;
    private float nextSpawnPositionZ;
    private List<GameObject> spawnedObjects = new List<GameObject>(); // 생성된 오브젝트 저장

    void Start()
    {
        nextSpawnPositionZ = startPosition.z;
        GenerateMap();
    }

    void Update()
    {
        if (nextSpawnPositionZ < player.position.z + spawnThreshold)
        {
            GenerateMap();
        }

        DestroyObjectsBehindPlayer();
    }

    void GenerateMap()
    {
        while (nextSpawnPositionZ < player.position.z + spawnThreshold)
        {
            int consecutiveCount = Random.Range(minConsecutive, maxConsecutive + 1);

            for (int j = 0; j < consecutiveCount; j++)
            {
                GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
                GameObject instance = Instantiate(prefab, new Vector3(startPosition.x, startPosition.y, nextSpawnPositionZ), Quaternion.identity);
                spawnedObjects.Add(instance);
                nextSpawnPositionZ += 2;

                // 태그가 "Ground"인 경우 자동차 생성
                if (prefab.CompareTag("Ground"))
                {
                    GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

                    // x 위치를 20 또는 -20 중 하나로 선택
                    float carXPosition = Random.Range(0, 2) == 0 ? 20 : -20;

                    Vector3 carPosition = new Vector3(carXPosition, instance.transform.position.y + 1.3f, instance.transform.position.z);
                    Instantiate(carPrefab, carPosition, Quaternion.identity);
                }
            }
        }
    }


    void DestroyObjectsBehindPlayer()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] != null && spawnedObjects[i].transform.position.z < player.position.z - 6)
            {
                Destroy(spawnedObjects[i]);
                spawnedObjects.RemoveAt(i); // 리스트에서 제거
            }
        }
    }
}
