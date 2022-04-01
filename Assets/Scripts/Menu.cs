using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public bool Active;
    public float TransitionSpeed = 8;
    public Vector3 InactivePosition;
    public Vector3 ActivePosition;
    private RectTransform rect;
    private GameManager gm;
    public GameObject FirstSelected;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        gm = GameObject.FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, ActivePosition, Time.deltaTime * TransitionSpeed);
        }
        else
        {
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, InactivePosition, Time.deltaTime * TransitionSpeed);

        }
    }
    public void Toggle()
    {
        gm = GameObject.FindObjectOfType<GameManager>();

        if(Active)
        {
            Active = false;
            gm.State = GameManager.GameState.Playing;

        }
        else
        {
            Active = true;
            gm.State = GameManager.GameState.Paused;
            EventSystem.current.SetSelectedGameObject(FirstSelected);


        }
    }

    public void Disable()
    {
        Active = false;
        gm = GameObject.FindObjectOfType<GameManager>();
        gm.State = GameManager.GameState.Playing;



    }
    public void Enable()
    {
        Active = true;
        gm = GameObject.FindObjectOfType<GameManager>();
        gm.State = GameManager.GameState.Paused;
        EventSystem.current.SetSelectedGameObject(FirstSelected);




    }
}
