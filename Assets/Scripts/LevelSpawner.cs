using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameManager Manager;
    public GameObject[] Level1Layouts;
    public GameObject[] Level2Layouts;
    public GameObject[] Level3Layouts;
    public GameObject[] Level4Layouts;
    public void SpawnLevel(int i)
    {
        var picker = 0;
        if(i == 1)
        {
            picker = Random.Range(0, Level1Layouts.Length);
            var newLayout = Instantiate(Level1Layouts[picker], transform.position, transform.rotation);
            Manager.RoomLayout = newLayout;
        }
        else if (i == 2)
        {
            picker = Random.Range(0, Level2Layouts.Length);
            var newLayout = Instantiate(Level2Layouts[picker], transform.position, transform.rotation);
            Manager.RoomLayout = newLayout;
        }
        else if (i == 3)
        {
            picker = Random.Range(0, Level3Layouts.Length);
            var newLayout = Instantiate(Level3Layouts[picker], transform.position, transform.rotation);
            Manager.RoomLayout = newLayout;
        }
        else if (i == 4)
        {
            picker = Random.Range(0, Level4Layouts.Length);
            var newLayout = Instantiate(Level4Layouts[picker], transform.position, transform.rotation);
            Manager.RoomLayout = newLayout;
        }
    }
}
