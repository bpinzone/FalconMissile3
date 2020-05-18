using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// House the stats and controller for the ship. Players can board.
public class ShipCore : MonoBehaviour {

    [SerializeField]
    private Stats stats = new Stats();
    private AShipController controller;

    public delegate void OnPlayerBoarded(PlayerIdentity player, AShipController controller);
    public OnPlayerBoarded on_player_boarded;

    void Awake(){
        ship_systems.AddRange(GetComponents<ShipSystem>());
        foreach(ShipSystem system in ship_systems){
            Assert.IsTrue(!system.enabled);
        }
    }

    public void BoardPlayer(PlayerIdentity player){
        Assert.IsNotNull(player);
        controller = player.GetControlDevice().GenerateAShipController();
        Assert.IsNotNull(controller);
        if(on_player_boarded != null){
            on_player_boarded(player, controller);
        }
        foreach(ShipSystem system in ship_systems){
            system.enabled = true;
        }
    }

    public Stats GetStats(){
        return stats;
    }

    public AShipController GetShipController(){
        return controller;
    }

    private List<ShipSystem> ship_systems = new List<ShipSystem>();
}
