using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameManager GameManager;
    public enum LockedState
    {
        Open,
        Closed,
        Locked,
        Hidden
    }
    public GameObject ClosedObject;
    public GameObject OpenObject;
    public GameObject LockedObject;
    public Vector3 NewPlayerPosition;

    public LockedState State = LockedState.Closed;

    public void Start()
    {
        GameManager = GameObject.FindObjectOfType<GameManager>();
        if (State == LockedState.Locked)
        {
            ClosedObject.SetActive(false);
            OpenObject.SetActive(false);
            LockedObject.SetActive(true);
        }
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerControl>() != null)
        {
            if(State == LockedState.Open)
            {
                Debug.Log("Player collided with open portal");
                GameManager.ChangeLevel(NewPlayerPosition);
            }
        }
    }

    public void OpenPortal()
    {
        State = LockedState.Open;
        OpenObject.SetActive(true);
        ClosedObject.SetActive(false);
    }
}
