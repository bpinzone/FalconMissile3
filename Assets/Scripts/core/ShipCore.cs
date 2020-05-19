using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/*
A master coordination point for the ship.

Keeps track of:
    Stats
    Player Identity
    Player's controller

Coordinates:
    Item mounting
    Player boarding

Ship systems have "shortcuts" to this data via properties. (So "core." isn't riddled everywhere.)
    // Path to same reference. Avoid having multiple copies of the reference.
Ship systems can take their own additional actions when a player boards or statistics change.
*/
public class ShipCore : MonoBehaviour {

    public delegate void OnCoreChange();
    public OnCoreChange on_player_board;
    public OnCoreChange on_stat_change;

    // Populated on player boarding.
    private PlayerIdentity identity;
    private AShipController controller;

    // Populated on Awake
    [SerializeField]
    private Stats stats = new Stats();
    private List<ShipSystem> ship_systems = new List<ShipSystem>();

    // Gather systems and subscribe them to us automatically.
    void Awake(){
        ship_systems.AddRange(GetComponentsInChildren<ShipSystem>());
        foreach(ShipSystem system in ship_systems){
            on_player_board += system.OnPlayerBoard;
            on_stat_change += system.OnStatChange;
            Assert.IsTrue(!system.enabled);
        }
    }

    // Populate identity, controller, notify others.
    // Enabled systems now that we have a pilot.
    public void BoardPlayer(PlayerIdentity _identity){
        Assert.IsNotNull(_identity);
        identity = _identity;
        controller = identity.GetControlDevice().GetAShipController();

        Assert.IsNotNull(on_player_board);
        on_player_board();

        foreach(ShipSystem system in ship_systems){
            system.enabled = true;
        }
    }

    public void Mount(Item i){
        i.affect(stats, true);
        Assert.IsNotNull(on_stat_change);
        on_stat_change();
    }

    public void Dismount(Item i){
        i.affect(stats, false);
        Assert.IsNotNull(on_stat_change);
        on_stat_change();
    }

    public PlayerIdentity GetIdentity(){
        return identity;
    }

    public AShipController GetController(){
        return controller;
    }

    public Stats GetStats(){
        return stats;
    }

    // Ship Systems sometimes read input directly.
    // But they can also subscribe to events. These events are checked here, instead of all over the place, in case multiple systems care about them.
    void FixedUpdate(){
        controller.CheckControlEventsFixedUpdate();
    }
}
