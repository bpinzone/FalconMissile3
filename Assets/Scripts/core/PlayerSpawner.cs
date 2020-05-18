using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerSpawner : MonoBehaviour {

    public static PlayerSpawner instance;
    public GameObject player_prefab;
    public GameObject ship_prefab;

    void Awake(){
        if(instance != null && instance != this){
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start(){
        ControlDeviceBank.instance.on_new_controller += CreatePlayer;
        ControlDeviceBank.instance.SetSearchingForNewPlayers(true);
    }

    public void CreatePlayer(AControlDevice new_player_device){

        GameObject new_player = Instantiate(player_prefab);
        PlayerIdentity identity = new_player.GetComponent<PlayerIdentity>();
        identity.Configure(0, new_player_device, Color.blue);

        GameObject new_ship = Instantiate(ship_prefab);
        new_ship.GetComponent<ShipCore>().BoardPlayer(identity);
        new_ship.transform.parent = new_player.transform;
    }

}
