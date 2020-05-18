using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// A persistent identify and control method for a player.

// Workflow for creating this:
// Listening to Device Bank for a controller. When you get one, create and configure one of these.
public class PlayerIdentity : MonoBehaviour {

    public void Configure(int _team, AControlDevice _control_device, Color _color){
        id = num_players++;
        team = _team;
        control_device = _control_device;
        color = _color;
        AssertConfigured();
        DontDestroyOnLoad(gameObject);
    }

    public int GetTeam(){
        AssertConfigured();
        return team;
    }

    public AControlDevice GetControlDevice(){
        AssertConfigured();
        return control_device;
    }

    private void AssertConfigured(){
        Assert.IsNotNull(control_device);
    }

    private static int num_players;

    [SerializeField]
    private int id;

    [SerializeField]
    private int team;

    private AControlDevice control_device;

    private Color color;
}
