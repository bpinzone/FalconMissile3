using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class KeyboardShipController : AShipController {

    public static KeyboardShipController FULL_KEYBOARD = new KeyboardShipController(
        "Vertical", "Horizontal", KeyCode.A, KeyCode.D, KeyCode.F
    );

    public override int GetThrustDir(){
        return (int) Input.GetAxisRaw(thrust_axis);
    }

    public override bool ABOn(){
        return GetThrustDir() != 0 && Input.GetKey(thrust_key);
    }

    public override int GetRotDir(){
        return (int) Input.GetAxisRaw(rot_axis);
    }

    public override bool GunButtonHeld(){
        return Input.GetKey(gun_key);
    }

    public override bool BombButtonHeld(){
        return Input.GetKey(bomb_key);
    }

    private KeyboardShipController(
            string _thrust_axis, string _rot_axis,
            KeyCode _thrust_key, KeyCode _gun_key, KeyCode _bomb_key){
        thrust_axis = _thrust_axis;
        rot_axis = _rot_axis;
        thrust_key = _thrust_key;
        gun_key = _gun_key;
        bomb_key = _bomb_key;
    }

    private string thrust_axis;
    private string rot_axis;

    private KeyCode thrust_key;
    private KeyCode gun_key;
    private KeyCode bomb_key;

}
