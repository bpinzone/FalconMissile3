using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PadShipController : AShipController {

    public PadShipController(Gamepad _pad) {
        pad = _pad;
    }

    public override int GetThrustDir(){
        if(pad.rightTrigger.ReadValue() > c_trigger_active_threshold){
            return 1;
        }
        if(pad.leftTrigger.ReadValue() > c_trigger_active_threshold){
            return -1;
        }
        return 0;
    }

    public override bool ABOn(){
        if(GetThrustDir() == 0){
            return false;
        }
        return (
            pad.rightShoulder.ReadValue() == 1 ||
            pad.leftShoulder.ReadValue() == 1
        );
    }

    public override int GetRotDir(){
        float x_val = pad.leftStick.x.ReadValue();
        if(x_val < -stick_rotate_threshold){
            return -1;
        }
        if(x_val > stick_rotate_threshold){
            return 1;
        }
        return 0;
    }

    public override bool GunButtonHeld(){
        return pad.aButton.ReadValue() == 1;
    }

    public override bool BombButtonHeld(){
        return pad.bButton.ReadValue() == 1;
    }

    private static readonly float c_trigger_active_threshold = 0.01f;
    private static readonly float stick_rotate_threshold = 0.5f;

    private Gamepad pad;

}
