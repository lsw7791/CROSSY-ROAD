using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] prefabs;  
    public GameObject[] treePrefabs;  
    public GameObject cherryPrefab; 
    public Vector3 startPosition;
    public int minConsecutive;
    public int maxConsecutive;

    [SerializeField] private float spawnThreshold;
    private float nextSpawnPositionZ;
    private List<GameObject> spawnedObjects = new List<GameObject>(); // 생성된 오브젝트 저장

    // 나무의 생성 위치를 추적
    private HashSet<Vector3> treePositions = new HashSet<Vector3>();

    void Start()
    {
        nextSpawnPositionZ = startPosition.z;
        GenerateMap();
    }

    void Update()
    {
        if (nextSpawnPositionZ < player.transform.position.z + spawnThreshold)
        {
            GenerateMap();
        }

        DestroyObjectsBehindPlayer();
    }

    void GenerateMap()
    {
        while (nextSpawnPositionZ < player.transform.position.z + spawnThreshold)
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
                        if (!treePositions.Contains(treePosition) && IsEnoughSpaceForTree(treePosition))
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

                    // 체리 생성
                    GenerateCherry(instance.transform.position);
                }
            }
        }
    }

    void GenerateCherry(Vector3 treePosition)
    {
        // 체리 생성 위치를 나무 근처로 설정 (X는 -5 ~ 5 범위, Z는 나무와 동일한 Z 위치)
        float cherryXPosition = Random.Range(-5, 6);
        Vector3 cherryPosition = new Vector3(treePosition.x + cherryXPosition, treePosition.y + 1.0f, treePosition.z);

        // 체리 위치가 겹치지 않도록 체크
        if (!treePositions.Contains(cherryPosition))
        {
            GameObject cherryInstance = Instantiate(cherryPrefab, cherryPosition, Quaternion.identity);
            spawnedObjects.Add(cherryInstance);
            treePositions.Add(cherryPosition); // 체리의 위치도 추적
        }
    }

    // 나무가 생성될 때 주변에 충분한 공간이 있는지 체크
    bool IsEnoughSpaceForTree(Vector3 position)
    {
        float spaceThreshold = 2f; 
        foreach (var existingPosition in treePositions)
        {
            if (Vector3.Distance(position, existingPosition) < spaceThreshold)
            {
                return false; 
            }
        }
        return true;
    }

    void DestroyObjectsBehindPlayer()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] == null || !spawnedObjects[i].activeInHierarchy || spawnedObjects[i].transform.position.z < player.transform.position.z - 6)
            {
                // 나무나 체리일 경우 위치도 제거
                if (spawnedObjects[i] != null && (spawnedObjects[i].CompareTag("Tree") || spawnedObjects[i].CompareTag("Cherry")))
                {
                    treePositions.Remove(spawnedObjects[i].transform.position);
                }

                if (spawnedObjects[i] != null)
                {
                    Destroy(spawnedObjects[i]);
                }
                spawnedObjects.RemoveAt(i); // 리스트에서 제거
            }
        }
    }


}
