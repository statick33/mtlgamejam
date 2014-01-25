using UnityEngine;
using System.Collections;

public class LeftAnalogInterface : MonoBehaviour
{
    public enum LeftAnalogDirection
    {
        None, Up, Down, Left, Right
    };
    private LeftAnalogDirection inputDirection = LeftAnalogDirection.None;
    private LeftAnalogDirection lastInputDirection = LeftAnalogDirection.None;

    bool init;

    private string UpDownAxis;
    private string LeftRightAxis;

    public delegate void LeftAnalogHandler(LeftAnalogDirection pDirection);
    public event LeftAnalogHandler OnDirectionChange;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (init)
        {
            float lookX = Input.GetAxis(LeftRightAxis);
            float lookY = -Input.GetAxis(UpDownAxis);

            if (Mathf.Abs(lookY) <= 0.1)
            {
                lookY = 0;
            }

            if (Mathf.Abs(lookX) <= 0.1)
            {
                lookX = 0;
            }

            if (lookY <= -0.8)
            {
                inputDirection = LeftAnalogDirection.Down;
            }
            else if (lookY >= 0.8)
            {
                inputDirection = LeftAnalogDirection.Up;
            }
            else if (lookX <= -0.8)
            {
                inputDirection = LeftAnalogDirection.Left;
            }
            else if (lookX >= 0.8)
            {
                inputDirection = LeftAnalogDirection.Right;
            }
            else if (lookY == 0 && lookX == 0)
            {
                inputDirection = LeftAnalogDirection.None;
            }

            if (lastInputDirection == LeftAnalogDirection.None && inputDirection != LeftAnalogDirection.None)
            {
                OnDirectionChange(inputDirection);
            }

            lastInputDirection = inputDirection;
        }
	}

    public void Init(string pUpDownAxisName, string pLeftRightAxisName)
    {
        UpDownAxis = pUpDownAxisName;
        LeftRightAxis = pLeftRightAxisName;

        init = true;

    }
}
