using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class playername_hook : LobbyHook {

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        //LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        //hp_hud NamePlayer = gamePlayer.GetComponent<hp_hud>();
            
      
        //NamePlayer.playerName = lobby.playerName;
        
    }
}

