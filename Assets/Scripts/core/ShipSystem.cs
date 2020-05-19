using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// A convenient cache of common objects, accessible for ship systems via base class.
// Usable after a player boards the ship.

// If it uses stats, its a ship system. Yes, thats basically everything!
public abstract class ShipSystem : MonoBehaviour {

    private ShipCore core;
    protected GameObject ship;

    protected virtual void Awake(){
        core = GetComponentInParent<ShipCore>();
        Assert.IsNotNull(core);
        ship = core.gameObject;
    }

    public Stats stats { get { return core.GetStats();} }
    public PlayerIdentity identity { get { return core.GetIdentity();} }
    public AShipController controller {get { return core.GetController();} }

    public virtual void OnPlayerBoard(){ }
    public virtual void OnStatChange(){ }

}
