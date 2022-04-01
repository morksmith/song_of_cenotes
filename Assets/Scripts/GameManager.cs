using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public int CurrentLevel = 1;
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
    public GameObject RoomLayout;
    public ObstacleSpawner[] ObstacleSpawners;
    public GameObject BlackSolid;
    public Vector3 NewPlayerPosition;
    private float step;
    private float colourStep;
    private bool levelCleared = false;
    public LevelSpawner LevelSpawn;
    void Start()
    {

        
        LoadNewLevel();
        


    }

    // Update is called once per frame
    void Update()
    {
        if(State == GameState.Paused)
        {
            return;
        }
        if (!DoorsOpened)
        {
            enemyTimer += Time.deltaTime;
            if (enemyTimer > EnemyCheckTime)
            {
                var enemiesInScene = GameObject.FindObjectsOfType<Enemy>();
                var pickupsInScene = GameObject.FindObjectsOfType<Pickup>();
                if (enemiesInScene.Length <= 0 && pickupsInScene.Length <= 0)
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
                colourStep += Time.deltaTime * 2;
                if(colourStep < 1.1f)
                {
                    BlackSolid.GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0, 0, colourStep));
                }
                else
                {
                    if (!levelCleared)
                    {
                        ClearLevel();
                    }
                    step += Time.deltaTime;
                    Player.transform.localPosition = Vector3.Lerp(Player.transform.localPosition, NewPlayerPosition, step * 0.04f);
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
                colourStep -= Time.deltaTime * 2;
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
                    levelCleared = false;

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
        LevelSpawn.SpawnLevel(CurrentLevel);
        Portals = GameObject.FindObjectsOfType<Portal>();
        EnemySpawners = GameObject.FindObjectsOfType<EnemySpawner>();
        ObstacleSpawners = GameObject.FindObjectsOfType<ObstacleSpawner>();
        if(Portals.Length > 0)
        {
            for (var i = 0; i < Portals.Length; i++)
            {
                Portals[i].ClosePortal();
            }
        }
        if(EnemySpawners.Length > 0)
        {
            for (var i = 0; i < EnemySpawners.Length; i++)
            {
                EnemySpawners[i].SpawnEnemy();
            }
        }
        if(ObstacleSpawners.Length > 0)
        {
            for (var i = 0; i < ObstacleSpawners.Length; i++)
            {
                ObstacleSpawners[i].SpawnObstacle();
            }
        }
        
    }

    public void ClearLevel()
    {
        Destroy(RoomLayout);
        var projectiles = GameObject.FindObjectsOfType<Projectile>();
        if(projectiles.Length > 0)
        {
            for(var x = 0; x < projectiles.Length; x++)
            {
                Destroy(projectiles[x].transform);
            }
        }
        Array.Clear(ObstacleSpawners, 0, ObstacleSpawners.Length);
        Array.Clear(EnemySpawners, 0, EnemySpawners.Length);
        Array.Clear(Portals, 0, Portals.Length);
        levelCleared = true;

    }


}
