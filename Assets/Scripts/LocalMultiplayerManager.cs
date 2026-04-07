using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiplayerManager : MonoBehaviour
{
    public List<Sprite> playerSprites;
    public List<PlayerInput> players;
    
    public void OnPlayerJoined(PlayerInput player)
    {
        players.Add(player);

        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        sr.sprite = playerSprites[player.playerIndex];

        LocalMultipler controller = player.GetComponent<LocalMultipler>();
        controller.manager = this;
    }

    public void PlayerAttacking(PlayerInput attackPlayer)
    {
        for(int i=0;i<players.Count;i++)
        {
            if (attackPlayer == players[i]) continue;
            if(Vector2.Distance(attackPlayer.transform.position, players[i].transform.position)<0.5f) {
                //Debug.Log("Player "+attackPlayer.playerIndex+" hit player "+ players[i].playerIndex);
                LocalMultipler damPlayer = players[i].GetComponent<LocalMultipler>();
                damPlayer.health -= 2;
            }
        }
    }

    public void WorldHealing()
    {
        for (int j = 0; j < players.Count; j++)
        {
            LocalMultipler damPlayer = players[j].GetComponent<LocalMultipler>();
            damPlayer.heal();
        }
    }
}
