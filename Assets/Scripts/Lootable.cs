using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Lootable : MonoBehaviour
{
    Transform _transform;
    void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        
    }

        
    private void OnMouseUpAsButton() {
        List<GameObject> workers = GameObject.FindGameObjectsWithTag("Worker").Where(worker => Vector3.Distance(_transform.position, worker.transform.position) < 14f).OrderBy(worker => Vector3.Distance(_transform.position, worker.transform.position)).ToList();
        Debug.Log("trying to loot");
        if(workers.Count >= 1){
            Debug.Log("Looting by chest click");
            workers[0].GetComponent<NPC>().DirectLoot(gameObject);
        }
    }
}
