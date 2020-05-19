using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class KeyboardControlDevice : AControlDevice {

    /*
    NOTE:
    https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Keyboard.html

    At the moment, Unity platform backends generally do not support distinguishing between multiple keyboards. While the Input System supports having many Keyboard devices at any point, platform backends generally only report a single keyboard and route input from all attached keyboards to the one keyboard device.

    COULD have multiple keyboards, but have them use different sections. Lets not do that right now.
    */

    public static KeyboardControlDevice instance = new KeyboardControlDevice();

    public override AShipController GetAShipController() {
        return KeyboardShipController.FULL_KEYBOARD;
    }

    public override bool StartPressed(){
        return Keyboard.current.enterKey.isPressed;
    }

    private KeyboardControlDevice(){

    }

}
