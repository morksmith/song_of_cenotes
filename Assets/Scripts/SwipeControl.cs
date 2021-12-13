using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    public float SwipeTime;
    private float swipeTimer;
    private float dragDistance;  //minimum distance for a swipe to be registered
    public float ScreenPercentage = 0.6f;
    public PlayerMovement Movement;
    void Start()
    {
        dragDistance = Screen.height * 10 / 100; //dragDistance is 15% height of the screen
    }

    void Update()
    {
        var RoomDistance = Screen.width * 30 /100;
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            swipeTimer += Time.deltaTime;
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance && swipeTimer < SwipeTime)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            var swipDir = new Vector3(Screen.width * 0.75f, Screen.height * 0.5f, 0);
                            Movement.Dash(swipDir);
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            var swipDir = new Vector3(Screen.width * 0.25f, Screen.height * 0.5f, 0);
                            Movement.Dash(swipDir);

                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            var swipDir = new Vector3(Screen.width * 0.5f, Screen.height * 0.66f, 0);
                            Movement.Dash(swipDir);

                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                            var swipDir = new Vector3(Screen.width * 0.5f, Screen.height * 0.33f, 0);
                            Movement.Dash(swipDir);

                        }
                    }
                    swipeTimer = 0;
                }
               
            }
        }
    }
}
