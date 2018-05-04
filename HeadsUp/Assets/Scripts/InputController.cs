using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private int playerNumber;

    private static List<InputController> controllers;
    private static int playerCounter;

    private const string horizontalAxis = "Horizontal";
    private const string gravityTurnAxis = "GravityTurn";
    private const string jumpButton = "Jump";

    private void Awake()
    {
        this.playerNumber = playerCounter++;
        if (controllers == null)
            controllers = new List<InputController>();
        controllers.Add(this);
    }

    public float Horizontal { get { return Input.GetAxis(horizontalAxis + playerNumber); } }
    public float GravityTurn { get { return Input.GetAxis(gravityTurnAxis + playerNumber); } }
    public bool Jump { get { return Input.GetButtonDown(jumpButton + playerNumber); } }
}
