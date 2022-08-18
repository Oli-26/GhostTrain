using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public List<GameObject> TreePrefabs;
    public List<GameObject> GlassDetailPrefabs;
    public List<GameObject> StonePrefabs;
    public GameObject coreGroundPrefab;

    public List<GameObject> spawnedObjects = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GenerateTerrain(Vector3 center){
        GameObject terrainHolder = new GameObject();
        
        GameObject ground = Instantiate(coreGroundPrefab, center, Quaternion.identity);
        ground.transform.parent = terrainHolder.transform;
        spawnedObjects.Add(ground);

        for(int i = 0; i<45; i++){
            int index = Random.Range(0,TreePrefabs.Count-1);

            GameObject tree = Instantiate(TreePrefabs[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(1f, 4.5f), 0), Quaternion.identity);
            tree.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(tree);
        }

         for(int i = 0; i<22; i++){
            int index = Random.Range(0,StonePrefabs.Count-1);

            GameObject stone = Instantiate(StonePrefabs[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(1.5f, 4.5f), 0), Quaternion.identity);
            stone.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(stone);
        }

        for(int i = 0; i<150; i++){
            int index = Random.Range(0,TreePrefabs.Count-1);

            GameObject glassDetail = Instantiate(GlassDetailPrefabs[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(0.7f, 4.5f), 0), Quaternion.identity);
            glassDetail.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(glassDetail);
        }

        return terrainHolder;

    }

    private float generateRandomNumber(float minimum, float maximum){
        float randomNumber = Random.Range(minimum, maximum);

        return Random.Range(0,2) == 0 ? randomNumber : -randomNumber;
    }
}
