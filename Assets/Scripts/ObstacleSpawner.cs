using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> Obstacles;
    public GameObject RoomLayout;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnObstacle()
    {
        var willSpawn = Random.Range(0, 10);
        if( willSpawn > 5)
        {
            var pick = Random.Range(0, Obstacles.Count);
            Instantiate(Obstacles[pick], transform.position, transform.rotation, RoomLayout.transform);
        }
        
    }
}
