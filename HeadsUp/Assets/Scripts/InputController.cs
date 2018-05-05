using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private int playerNumber;

    private static List<InputController> controllers;
    private static int playerCounter;

    private const string horizontalAxis = "Horizontal";
    private const string verticalAxis = "Vertical";
    private const string gravityTurnRightButton = "GravityTurnRight";
    private const string gravityTurnLeftButton = "GravityTurnLeft";
    private const string jumpButton = "Jump";

    private void Awake()
    {
        this.playerNumber = playerCounter++;
        if (controllers == null)
            controllers = new List<InputController>();
        controllers.Add(this);
    }

    public float Horizontal { get { return Input.GetAxis(horizontalAxis + playerNumber); } }
    public float Vertical { get { return Input.GetAxis(verticalAxis + playerNumber); } }
    public bool GravityTurnLeft { get { return Input.GetButtonDown(gravityTurnLeftButton + playerNumber); } }
    public bool GravityTurnRight { get { return Input.GetButtonDown(gravityTurnRightButton + playerNumber); } }
}
