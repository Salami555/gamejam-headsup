using UnityEngine;
using System.Collections;
using Entities;
using System.Linq;

public class WinManager : MonoBehaviour
{
    private Player[] players;
    public UnityEngine.UI.Text winText;
    public float winTimeOut = 3.0f;

    private void Awake()
    {
        players = GetComponentsInChildren<Player>();
    }

    public void CheckLives()
    {
        var livingPlayers = players.Where(p => p.health == 0);
        if (livingPlayers.Count() == 1)
        {
            StartCoroutine(PlayerWon( livingPlayers.First()));
        }
    }

    private IEnumerator PlayerWon(Player winner)
    {
        winText.text = "Player " + winner.playerName + " won!";
        winText.enabled = true;
        InputController.Reset();
        yield return new WaitForSeconds(winTimeOut);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
