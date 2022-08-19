using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public GameObject railBlockPrefab;
    Transform trainTransform;
    Transform _transform;
    public Vector3 nextRailSpawn = new Vector3(0f, 0f, 0f);
    public Vector3 nextTerrainSpawn = new Vector3(0f, 0f, 0f);

    List<GameObject> allRails = new List<GameObject>();
    List<GameObject> allTerrains = new List<GameObject>();

    int spawnCheckTick = 0;

    TerrainGenerator terrainGenerator;

    List<GameObject> targetedResources = new List<GameObject>();

    void Start()
    {
        trainTransform = GameObject.Find("Train").transform;
        terrainGenerator = GetComponent<TerrainGenerator>();
        _transform = transform;

        spawnNextRail();
        spawnNextTerrain();
    }
    void Update()
    {
        
        if(spawnCheckTick == 30){
            if(checkNextRailSpawn()){
                spawnNextRail();
            }

            if(checkNextTerrainSpawn()){
                spawnNextTerrain();
            }

            spawnCheckTick = 0;
        }else{
            spawnCheckTick++;
        }
        
    }

    bool checkNextRailSpawn(){
        if(Vector3.Distance(trainTransform.position, nextRailSpawn) < 15f){
            return true;
        }

        return false;
    }

    void spawnNextRail(){
        allRails.Add(Instantiate(railBlockPrefab, nextRailSpawn, Quaternion.identity));

        nextRailSpawn += new Vector3(23f, 0f, 0f);

        if(allRails.Count > 4){
            Destroy(allRails[0]);
            allRails.RemoveAt(0);
        }
    }

     bool checkNextTerrainSpawn(){
        if(Vector3.Distance(trainTransform.position, nextTerrainSpawn) < 80f){
            return true;
        }

        return false;
    }

    void spawnNextTerrain(){
        allTerrains.Add(terrainGenerator.GenerateRandomTerrain(nextTerrainSpawn));

        nextTerrainSpawn += new Vector3(100f, 0f, 0f);

        if(allTerrains.Count > 2){
            Destroy(allTerrains[0]);
            allTerrains.RemoveAt(0);
        }
    }

    public void TargetResource(GameObject resource){
        targetedResources.Add(resource);
    }

    public void DeTargetResource(GameObject resource){
        targetedResources.Remove(resource);
    }

    public bool IsResourceTargeted(GameObject resource){
        return targetedResources.Contains(resource);
    }
}
