using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// Ship core monobehavior will hold this. Ship systems will interact directly with this.
public abstract class AShipController {


    // === Delegates
    public delegate void OnABChange(bool ab_on);
    public OnABChange on_ab_change;

    public delegate void OnThrustDirChange(int dir);
    public OnThrustDirChange on_thrust_dir_change;

    public delegate void OnRotDirChange(int dir);
    public OnRotDirChange on_rot_dir_change;

    // === Ship Movement ===
    // 1 forward. 0 none; -1 backward;
    public abstract int GetThrustDir();
    public abstract bool ABOn();
    // 1 cw; 0 none; -1 ccw;
    public abstract int GetRotDir();

    // === Ship Weapons ===
    public abstract bool GunButtonHeld();
    public abstract bool BombButtonHeld();

    // === Event checking ===
    public void CheckControlEventsFixedUpdate(){
        ProcessABEvents();
        ProcessThrustDirEvents();
        ProcessRotDirEvents();
    }

    // === Event Processing ===
    // AB
    private bool ab_on_last_frame;
    private void ProcessABEvents(){
        // Status didn't change or nobody cares
        if(ab_on_last_frame == ABOn() || on_ab_change == null){
            return;
        }
        on_ab_change(ABOn());
        ab_on_last_frame = ABOn();
    }
    // Thrust Dir
    private int thrust_dir_last_frame;
    private void ProcessThrustDirEvents(){
        if(thrust_dir_last_frame == GetThrustDir() || on_thrust_dir_change == null){
            return;
        }
        on_thrust_dir_change(GetThrustDir());
        thrust_dir_last_frame = GetThrustDir();
    }
    // Rot Dir
    private int rot_dir_last_frame;
    private void ProcessRotDirEvents(){
        if(rot_dir_last_frame == GetRotDir() || on_rot_dir_change == null){
            return;
        }
        on_rot_dir_change(GetRotDir());
        rot_dir_last_frame = GetRotDir();
    }
}
