using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// Controls ship movement, including limiting movement.
public class EngineSystem : ShipSystem {

    // NOTE: FM2 was 200 deg/sec. Which is 3.5 rad/sec.
    // Want rot reasonable avg rot (500) to correspond to this.
    private static readonly float c_rot_stat_to_max_ang_vel = 3.5f / 500.0f;

    protected override void Awake() {
        base.Awake();
        rb = ship.GetComponent<Rigidbody>();
        ps = ship.GetComponent<PowerSystem>();
    }

    public override void OnStatChange(){
        rb.maxAngularVelocity = stats.sublight_drive.rotation.val() * c_rot_stat_to_max_ang_vel;
    }

    public override void OnPlayerBoard(){
        controller.on_rot_dir_change += MaybeStopRotation;
    }

    void FixedUpdate(){
        ApplyThrust();
        ApplyRotation();
        // TODO: limit speed.
    }

    private void ApplyThrust(){
        if(controller.GetThrustDir() == 0){
            return;
        }
        int thrust_force = stats.sublight_drive.thrust.val();
        if(controller.ABOn()){
            thrust_force = stats.sublight_drive.ab_thrust.val();
            // TODO: discharge power system
            // ps.Discharge(stats.sublight_drive.ab_cost.val() * Time.deltaTime);
        }
        rb.AddForce(transform.up * controller.GetThrustDir() * thrust_force);
    }

    // The ship is a non-kinematic rigidbody.
    // But we want the feel of instant kinematic rotation, while
    // doing our best to respect physics engine.
    private void ApplyRotation(){
        if(controller.GetRotDir() == 0){
            return;
        }

        float large_insta_rotate_torque = 9999.0f;
        rb.AddTorque(-transform.forward * controller.GetRotDir() * large_insta_rotate_torque);
    }

    // Only happens when player releases the last horizontal arrow key. (Not every frame!)
    private void MaybeStopRotation(int new_rot_dir){
        if(new_rot_dir == 0){
            rb.angularVelocity = Vector3.zero;
        }
    }

    private Rigidbody rb;
    private PowerSystem ps;

}
