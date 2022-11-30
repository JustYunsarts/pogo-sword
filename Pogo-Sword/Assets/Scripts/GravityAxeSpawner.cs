using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAxeSpawner : MonoBehaviour
{
    /*
     *A prefab that spawns a gravity axe at set intervals
     *It sends the direction data to the axe, so that the axe knows which direction to go to
     */
    [SerializeField]
    private GameObject gravityAxe;

    [SerializeField]
    private directions direction;
    public directions Direction { get { return direction; } private set { direction = value; } }

    [SerializeField]
    private float spawnInterval;

    private float spawnTimer;
    public enum directions
    {
        up,
        down,
        left,
        right
    };

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0)
        {
            var newAxe = Instantiate(gravityAxe, transform.position, transform.rotation);
            newAxe.transform.parent = gameObject.transform;
            spawnTimer = spawnInterval;
        }

        spawnTimer -= Time.deltaTime;
    }
}
