using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PadControlDevice : AControlDevice {

    public PadControlDevice(Gamepad _pad) {
        pad = _pad;
        controller = new PadShipController(pad);
    }

    public override AShipController GetAShipController() {
        return controller;
    }

    public override bool StartPressed(){
        return pad.startButton.ReadValue() == 1;
    }

    private Gamepad pad;
    private PadShipController controller;
}
