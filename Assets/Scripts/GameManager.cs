using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public Animator PlayerAnimator;
    public float EnemyCheckTime = 5;
    private float enemyTimer;
    public bool DoorsOpened = false;
    public bool NewLevelLoaded = true;
    public enum GameState
    {
        Paused,
        Playing,
        Transition
    }
    public GameState State = GameState.Playing;
    public Portal[] Portals;
    public EnemySpawner[] EnemySpawners;
    public ObstacleSpawner[] ObstacleSpawners;
    public GameObject BlackSolid;
    public Vector3 NewPlayerPosition;
    private float step;
    private float colourStep;
    void Start()
    {

        Portals = GameObject.FindObjectsOfType<Portal>();
        EnemySpawners = GameObject.FindObjectsOfType<EnemySpawner>();
        ObstacleSpawners = GameObject.FindObjectsOfType<ObstacleSpawner>();
        LoadNewLevel();
        


    }

    // Update is called once per frame
    void Update()
    {
        if (!DoorsOpened)
        {
            enemyTimer += Time.deltaTime;
            if (enemyTimer > EnemyCheckTime)
            {
                var enemiesInScene = GameObject.FindObjectsOfType<Enemy>();
                if (enemiesInScene.Length <= 0)
                {
                    Debug.Log("No Enemies");
                    OpenDoors();
                    DoorsOpened = true;
                }
                enemyTimer = 0;
            }
        }

        if(State == GameState.Transition)
        {
            var playerDist = Vector3.Distance(Player.transform.position, NewPlayerPosition);
            if(playerDist > 0.2f)
            {
                colourStep += Time.deltaTime;
                if(colourStep < 1.1f)
                {
                    BlackSolid.GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0, 0, colourStep));
                }
                else
                {
                    step += Time.deltaTime;
                    Player.transform.localPosition = Vector3.Lerp(Player.transform.localPosition, NewPlayerPosition, step * 0.02f);
                }
                
            }
            else
            {
                if(NewLevelLoaded == false)
                {
                    LoadNewLevel();
                    DoorsOpened = false;
                    NewLevelLoaded = true;
                }
                colourStep -= Time.deltaTime;
                BlackSolid.GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0, 0, colourStep));
                if (colourStep<= 0)
                {
                    Player.GetComponent<PlayerControl>().enabled = true;
                    step = 0;
                    colourStep = 0;
                    PlayerAnimator.speed = 1;
                    State = GameState.Playing;
                    BlackSolid.GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0, 0, 0));
                    NewLevelLoaded = false;

                }
            }
            


        }

    }

    public void OpenDoors()
    {
        for(var i = 0; i<Portals.Length; i++)
        {
            if(Portals[i].State == Portal.LockedState.Closed)
            {
                Portals[i].OpenPortal();
            }
        }
    }

    public void ChangeLevel(Vector3 newPosition)
    {
        Player.GetComponent<PlayerControl>().enabled = false;
        step = 0;
        State = GameState.Transition;
        PlayerAnimator.speed = 0;
        NewPlayerPosition = newPosition;
    }

    public void LoadNewLevel()
    {
        for (var i = 0; i < Portals.Length; i++)
        {
            Portals[i].ClosePortal();
        }
        for (var i = 0; i < EnemySpawners.Length; i++)
        {
            EnemySpawners[i].SpawnEnemy();
        }
        for (var i = 0; i < ObstacleSpawners.Length; i++)
        {
            ObstacleSpawners[i].SpawnObstacle();
        }


    }

}
