using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PadControlDevice : AControlDevice {

    public PadControlDevice(Gamepad _pad) {
        pad = _pad;
    }

    public override AShipController GenerateAShipController() {
        return new PadShipController(pad);
    }

    public override bool StartPressed(){
        return pad.startButton.ReadValue() == 1;
    }

    private Gamepad pad;
}
