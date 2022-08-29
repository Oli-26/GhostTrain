using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public List<GameObject> TreePrefabs;
    public List<GameObject> GlassDetailPrefabs;
    public List<GameObject> StonePrefabs;
    public GameObject coreGroundPrefab;
    public List<GameObject> GrassLandLoot;
    public List<GameObject> Gardens;
    public GameObject giantTree;


    public List<GameObject> DesertTreePrefabs;
    public List<GameObject> DesertDetailPrefabs;
    public List<GameObject> DesertStonePrefabs;
    public GameObject DesertGroundPrefab;
    public List<GameObject> DesertLoot;
    public List<GameObject> DesertOasis;

    public GameObject SnowGroundPrefab;
    public List<GameObject> SnowLoot;
    public List<GameObject> SnowTrees;

    public GameObject WaterGroundPrefab;
    public GameObject WaterTrackSupport;
    public GameObject Boat;
    public List<GameObject> Fishes;
    public GameObject SmallConcreteIsland;

    public GameObject SpookyGroundPrefab;
    public List<GameObject> GraveStones;
    public List<GameObject> GraveYards;
    public GameObject SpookyTree;
    
    public List<GameObject> spawnedObjects = new List<GameObject>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public GameObject GenerateRandomTerrain(Vector3 center){
        int numberOfBiomes = BiomeType.GetNames(typeof(BiomeType)).Length;
        int random = Random.Range(0, numberOfBiomes);
        BiomeType type = (BiomeType) random;

        switch(type){
            case BiomeType.Grass:
                return GenerateGrassTerrain(center);
            case BiomeType.Desert:
                return GenerateDesertTerrain(center);
            case BiomeType.Snow:
                return GenerateSnowTerrain(center);
            case BiomeType.Water:
                return GenerateGrassTerrain(center);
            case BiomeType.Spooky:
                return GenerateSpookyTerrain(center);
            default:
                return null;
        }
    }

    public GameObject GenerateGrassTerrain(Vector3 center){
        GameObject terrainHolder = new GameObject();
        terrainHolder.name = "GrassBiome";
        
        GameObject ground = Instantiate(coreGroundPrefab, center, Quaternion.identity);
        ground.transform.parent = terrainHolder.transform;
        spawnedObjects.Add(ground);

        for(int i = 0; i<90; i++){
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

            GameObject grassDetail = Instantiate(GlassDetailPrefabs[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(0.7f, 4.5f), 0), Quaternion.identity);
            grassDetail.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(grassDetail);
        }

        for(int i = 0; i<Random.Range(0, 3); i++){
            int index = Random.Range(0,GrassLandLoot.Count-1);

            GameObject loot = Instantiate(GrassLandLoot[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(0.7f, 4.5f), 0), Quaternion.identity);
            loot.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(loot);
        }

        for(int i = 0; i<Random.Range(0, 2); i++){
            int index = Random.Range(0,Gardens.Count-1);

            GameObject garden = Instantiate(Gardens[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(0.7f, 4.5f), 0), Quaternion.identity);
            garden.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(garden);
        }

        GameObject gaintT  = Instantiate(giantTree, center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(0.7f, 4.5f), 0), Quaternion.identity);
        gaintT.transform.parent = terrainHolder.transform;
        spawnedObjects.Add(gaintT);

        

        return terrainHolder;

    }


    public GameObject GenerateDesertTerrain(Vector3 center){
        GameObject terrainHolder = new GameObject();
        terrainHolder.name = "DesertBiome";

        GameObject ground = Instantiate(DesertGroundPrefab, center, Quaternion.identity);
        ground.transform.parent = terrainHolder.transform;
        spawnedObjects.Add(ground);

        for(int i = 0; i<20; i++){
            int index = Random.Range(0,DesertTreePrefabs.Count-1);

            GameObject tree = Instantiate(DesertTreePrefabs[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(1f, 4.5f), 0), Quaternion.identity);
            tree.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(tree);
        }

         for(int i = 0; i<65; i++){
            int index = Random.Range(0,DesertStonePrefabs.Count-1);

            GameObject stone = Instantiate(StonePrefabs[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(1.5f, 4.5f), 0), Quaternion.identity);
            stone.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(stone);
        }

        for(int i = 0; i<70; i++){
            int index = Random.Range(0,DesertDetailPrefabs.Count-1);

            GameObject glassDetail = Instantiate(DesertDetailPrefabs[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(0.7f, 4.5f), 0), Quaternion.identity);
            glassDetail.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(glassDetail);
        }

        for(int i = 0; i<Random.Range(0, 3); i++){
            int index = Random.Range(0,DesertLoot.Count-1);

            GameObject loot = Instantiate(DesertLoot[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(0.7f, 4.5f), 0), Quaternion.identity);
            loot.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(loot);
        }

        for(int i = 0; i<Random.Range(0, 2); i++){
            int index = Random.Range(0,DesertOasis.Count-1);

            GameObject oasis = Instantiate(DesertOasis[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(0.7f, 4.5f), 0), Quaternion.identity);
            oasis.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(oasis);
        }
        

        return terrainHolder;
    }

    public GameObject GenerateSnowTerrain(Vector3 center){
        GameObject terrainHolder = new GameObject();
        terrainHolder.name = "SnowTerrain";

        GameObject ground = Instantiate(SnowGroundPrefab, center, Quaternion.identity);
        ground.transform.parent = terrainHolder.transform;
        spawnedObjects.Add(ground);

        
        for(int i = 0; i<20; i++){
            int index = Random.Range(0,SnowTrees.Count-1);

            GameObject tree = Instantiate(SnowTrees[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(1f, 4.5f), 0), Quaternion.identity);
            tree.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(tree);
        }


        for(int i = 0; i<45; i++){
            int index = Random.Range(0,DesertStonePrefabs.Count-1);

            GameObject stone = Instantiate(StonePrefabs[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(1.5f, 4.5f), 0), Quaternion.identity);
            stone.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(stone);
        }

        return terrainHolder;
    }

    public GameObject GenerateWaterTerrain(Vector3 center){
        GameObject terrainHolder = new GameObject();
        terrainHolder.name = "WaterTerrain";
        
        GameObject ground = Instantiate(WaterGroundPrefab, center, Quaternion.identity);
        ground.transform.parent = terrainHolder.transform;
        spawnedObjects.Add(ground);

        GameObject path = Instantiate(WaterTrackSupport, center, Quaternion.identity);
        path.transform.parent = terrainHolder.transform;
        spawnedObjects.Add(path);

        GameObject boat = Instantiate(Boat, center + new Vector3(generateRandomNumber(0f,35f), generateRandomNumber(2f, 4f), 0f), Quaternion.identity);


        
        for(int i = 0; i<20; i++){
            int index = Random.Range(0,Fishes.Count-1);

            GameObject fish = Instantiate(Fishes[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(1f, 4.5f), 0), Quaternion.identity);
            fish.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(fish);
        }

        for(int i = 0; i < Random.Range(0, 2); i++){
            GameObject island = Instantiate(SmallConcreteIsland, center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(2f, 4.5f), 0), Quaternion.identity);
            island.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(island);
            
        }


        return terrainHolder;
    }

    public GameObject GenerateSpookyTerrain(Vector3 center){
        GameObject terrainHolder = new GameObject();
        terrainHolder.name = "SpookyTerrain";
        
        GameObject ground = Instantiate(SpookyGroundPrefab, center, Quaternion.identity);
        ground.transform.parent = terrainHolder.transform;
        spawnedObjects.Add(ground);


        for(int i = 0; i<10; i++){
            int index = Random.Range(0,GraveStones.Count-1);

            GameObject grave = Instantiate(GraveStones[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(1f, 4.5f), 0), Quaternion.identity);
            grave.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(grave);
        }

        for(int i = 0; i < 2; i++){
            int index = Random.Range(0,GraveYards.Count-1);

            GameObject graveYard = Instantiate(GraveYards[index], center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(2.5f, 3.5f), 0), Quaternion.identity);
            graveYard.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(graveYard);
        }

        for(int i = 0; i < 15; i++){
            GameObject tree = Instantiate(SpookyTree, center + new Vector3(generateRandomNumber(0, 49f), generateRandomNumber(2.5f, 3.5f), 0), Quaternion.identity);
            tree.transform.parent = terrainHolder.transform;
            spawnedObjects.Add(tree);
            
        }


        return terrainHolder;
    }

    private float generateRandomNumber(float minimum, float maximum){
        float randomNumber = Random.Range(minimum, maximum);

        return Random.Range(0,2) == 0 ? randomNumber : -randomNumber;
    }
}

public enum BiomeType {Grass, Desert, Snow, Water, Spooky}
