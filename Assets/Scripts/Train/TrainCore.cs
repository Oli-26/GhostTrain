using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCore : TimeEffected
{
    private const int MAX_EXTENSIONS = 5;

    public GameObject extensionPrefab;

    Transform _transform;
    public GameObject trainFront;
    float speed = 3f;
    bool boostActive = false;
    float boostAmount = 1.5f;

    public List<Extention> Extentions { get; } = new List<Extention>();

    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveForward();
    }

    public void boost()
    {
        boostActive = true;
    }

    public void moveForward()
    {
        _transform.position += new Vector3(getActiveSpeed() * getTimePassed(), 0, 0);
        boostActive = false;
    }

    public void moveBackward()
    {
        _transform.position += new Vector3(-getActiveSpeed() * getTimePassed(), 0, 0);
    }

    private float getActiveSpeed()
    {
        return speed + (boostActive ? boostAmount : 0f);
    }

    public void AddExtension()
    {
        if (Extentions.Count < MAX_EXTENSIONS)
        {
            Extention extension = Instantiate(extensionPrefab, trainFront.transform.position, Quaternion.identity).GetComponent<Extention>();
            extension.transform.position -= new Vector3((extension.GetComponent<Extention>().baseObject.GetComponent<SpriteRenderer>().bounds.size.x - 0.2f) * Extentions.Count, 0f, 0f);
            extension.transform.position -= new Vector3(1.88f, 0.82f, 0f);
            extension.transform.parent = gameObject.transform;
            
            Extentions.Add(extension);
            extension.SetSlotExtensionId(Extentions.Count);
        }
    }
}