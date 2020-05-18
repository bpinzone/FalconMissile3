using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// Controls ship movement, including limiting movement.
public class EngineSystem : ShipSystem {

    protected override void Awake() {
        base.Awake();
        GatherComponents();
    }

    void FixedUpdate(){

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
        rb.AddRelativeForce(Vector3.forward * controller.GetThrustDir() * thrust_force);
    }

    private void ApplyRotation(){
        if(controller.GetRotDir() == 0){
            return;
        }
        int rot_torque = stats.sublight_drive.rotation.val();
        rb.AddRelativeTorque(Vector3.up * controller.GetRotDir() * rot_torque);
    }

    private void GatherComponents(){
        rb = ship.GetComponent<Rigidbody>();
        ps = ship.GetComponent<PowerSystem>();
    }

    private Rigidbody rb;
    private PowerSystem ps;

}
