using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Assertions;

// TODO: Learn more of Unity's input system and don't do this.
public class RawAxis {

    public RawAxis(ButtonControl _pos_key, ButtonControl _neg_key){
        pos_key = _pos_key;
        neg_key = _neg_key;
    }

    public int Get(){
        if(pos_key.isPressed && neg_key.isPressed){
            if(pos_key.wasPressedThisFrame){
                return 1;
            }
            Assert.IsTrue(neg_key.wasPressedThisFrame);
            return -1;
        }
        if(pos_key.isPressed){
            return 1;
        }
        if(neg_key.isPressed){
            return -1;
        }
        return 0;
    }

    private ButtonControl pos_key;
    private ButtonControl neg_key;
}

public class KeyboardShipController : AShipController {

    public static KeyboardShipController FULL_KEYBOARD = new KeyboardShipController(
        Keyboard.current.upArrowKey, Keyboard.current.downArrowKey,
        Keyboard.current.rightArrowKey, Keyboard.current.leftArrowKey,
        Keyboard.current.shiftKey, Keyboard.current.leftCtrlKey, Keyboard.current.tabKey
    );

    public override int GetThrustDir(){
        return thrust_axis.Get();
    }

    public override bool ABOn(){
        return GetThrustDir() != 0 && thrust_key.isPressed;
    }

    public override int GetRotDir(){
        return rot_axis.Get();
    }

    public override bool GunButtonHeld(){
        return gun_key.isPressed;
    }

    public override bool BombButtonHeld(){
        return bomb_key.isPressed;
    }

    private KeyboardShipController(
            ButtonControl _thrust_pos_axis, ButtonControl _thrust_neg_axis,
            ButtonControl _rot_pos_axis, ButtonControl _rot_neg_axis,
            ButtonControl _thrust_key, ButtonControl _gun_key, ButtonControl _bomb_key){
        thrust_axis = new RawAxis(_thrust_pos_axis, _thrust_neg_axis);
        rot_axis = new RawAxis(_rot_pos_axis, _rot_neg_axis);
        thrust_key = _thrust_key;
        gun_key = _gun_key;
        bomb_key = _bomb_key;
    }

    private RawAxis thrust_axis;
    private RawAxis rot_axis;

    private ButtonControl thrust_key;
    private ButtonControl gun_key;
    private ButtonControl bomb_key;

}
