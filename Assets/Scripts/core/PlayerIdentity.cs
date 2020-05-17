using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerIdentity : MonoBehaviour {

    public void Configure(AControlDevice _player_control_device, int _team, Color _color){
        id = num_players++;
        team = _team;
        player_control_device = _player_control_device;
        player_ship_controller = player_control_device.GenerateAShipController();
        color = _color;
    }

    public int GetTeam(){
        return team;
    }

    public AShipController GetAShipController(){
        return player_ship_controller;
    }

    private static int num_players = 0;

    [SerializeField]
    private int id;

    [SerializeField]
    private int team;

    private AControlDevice player_control_device;
    private AShipController player_ship_controller;

    private Color color;
}
