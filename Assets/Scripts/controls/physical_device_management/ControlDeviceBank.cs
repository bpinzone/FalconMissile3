using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

/*
TODO: Sometime: "OnAdd"/"OnRemove" for when things are plugged/unplugged
https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.Gamepad.html#UnityEngine_InputSystem_Gamepad_MakeCurrent
*/

// Can be made to listen for new controllers,
// and gives out devices when they are activated.
public class ControlDeviceBank : MonoBehaviour {

    public static ControlDeviceBank instance;

    // === Events
    // The listener will receive the new device that just had input.
    // NOTE: With todo above, maybe make this return the PlayerIdentity it was given to.
    public delegate void OnNewController(AControlDevice new_device);
    public OnNewController on_new_controller;

    // Control when we look for new players.
    // Someone should be listening for the new players.
    public void SetSearchingForNewPlayers(bool search){
        searching_for_new_players = search;
    }

    // Player must return their control device for someone else to take it.
    // Bank does NOT keep track of devices that have been given away!
    public void ReturnControlDevice(AControlDevice device){
        taken_devices.Remove(device);
        available_devices.Add(device);
    }

    void Awake(){
        // Singleton management
        if(instance != null && instance != this){
            Destroy(gameObject);
            return;
        }
        // Its me!
        instance = this;
        DontDestroyOnLoad(gameObject);

        InitAvailableDevices();
    }

    void Update(){
        if(searching_for_new_players){
            SearchForNewPlayers();
        }
    }

    private bool searching_for_new_players = false;
    private HashSet<AControlDevice> available_devices = new HashSet<AControlDevice>();
    private HashSet<AControlDevice> taken_devices = new HashSet<AControlDevice>();

    private void InitAvailableDevices(){

        available_devices.Add(KeyboardControlDevice.instance);
        foreach(Gamepad pad in Gamepad.all){
            available_devices.Add(new PadControlDevice(pad));
        }
    }

    void SearchForNewPlayers(){

        // Someone must be listening!
        Assert.IsNotNull(on_new_controller);

        HashSet<AControlDevice> devices_to_give = new HashSet<AControlDevice>();
        foreach(AControlDevice avail_device in available_devices){
            if(avail_device.StartPressed()){
                devices_to_give.Add(avail_device);
            }
        }
        foreach(AControlDevice device_to_give in devices_to_give){
            available_devices.Remove(device_to_give);
            taken_devices.Add(device_to_give);
            on_new_controller(device_to_give);
        }
    }
}
