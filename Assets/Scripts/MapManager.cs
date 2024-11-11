using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject[] treePrefabs;
    public Vector3 startPosition;
    public int minConsecutive;
    public int maxConsecutive;
    public Transform player;

    [SerializeField] private float spawnThreshold;
    private float nextSpawnPositionZ;
    private List<GameObject> spawnedObjects = new List<GameObject>(); // 생성된 오브젝트 저장

    // 나무의 생성 위치를 추적하는 Set
    private HashSet<Vector3> treePositions = new HashSet<Vector3>();

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

                // 태그가 "Grass"인 경우 10개의 나무 생성
                if (prefab.CompareTag("Grass"))
                {
                    for (int k = 0; k < 10; k++)
                    {
                        GameObject treePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];

                        // 나무의 x 위치를 -20부터 20까지 랜덤으로 설정
                        float treeXPosition = Random.Range(-20, 21);

                        // 나무가 겹치지 않도록 확인
                        Vector3 treePosition = new Vector3(treeXPosition, instance.transform.position.y + 1.0f, instance.transform.position.z);

                        // 나무가 이미 해당 위치에 존재하면 다시 생성 시도
                        if (!treePositions.Contains(treePosition))
                        {
                            GameObject treeInstance = Instantiate(treePrefab, treePosition, Quaternion.identity);
                            spawnedObjects.Add(treeInstance);
                            treePositions.Add(treePosition);
                        }
                        else
                        {
                            k--;
                        }
                    }
                }
            }
        }
    }

    void DestroyObjectsBehindPlayer()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (!spawnedObjects[i].activeInHierarchy)
            {
                Destroy(spawnedObjects[i]);
                spawnedObjects.RemoveAt(i);
            }
            else if (spawnedObjects[i] != null && spawnedObjects[i].transform.position.z < player.position.z - 6)
            {
                // 나무가 사라지면 해당 위치도 제거
                if (spawnedObjects[i].CompareTag("Tree"))
                {
                    treePositions.Remove(spawnedObjects[i].transform.position);
                }

                Destroy(spawnedObjects[i]);
                spawnedObjects.RemoveAt(i); // 리스트에서 제거
            }
        }
    }
}
