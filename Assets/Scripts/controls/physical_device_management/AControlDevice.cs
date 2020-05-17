using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents a physical device a player owns in order to give input.
// Responsible for generating abstract controllers for specific entities. (Ship, menu)
public abstract class AControlDevice {

    public abstract AShipController GenerateAShipController();
    public abstract bool StartPressed();
}
