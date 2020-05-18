using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// A convenient cache of common objects, accessible for ship systems via base class.
// Usable after a player boards the ship.
public abstract class ShipSystem : MonoBehaviour {

    // Populated on Awake
    protected ShipCore core;
    protected GameObject ship;
    protected Stats stats;

    // Populated on player board
    protected PlayerIdentity identity;
    protected AShipController controller;

    protected virtual void Awake(){
        // NOTE: Assumes ShipCore is component of ship game object.
        core = GetComponentInParent<ShipCore>();
        Assert.IsNotNull(core);
        ship = core.gameObject;
        stats = core.GetStats();
        core.on_player_boarded += CachePlayerInfo;
    }

    public void CachePlayerInfo(PlayerIdentity _identity, AShipController _controller){
        identity = _identity;
        controller = _controller;
    }
}
