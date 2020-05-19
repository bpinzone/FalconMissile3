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

        GameObject new_player = Instantiate(player_prefab, Vector3.zero, Quaternion.identity, null);
        PlayerIdentity identity = new_player.GetComponent<PlayerIdentity>();
        identity.Configure(0, new_player_device, Color.blue);

        GameObject new_ship = Instantiate(
            ship_prefab, Vector3.zero, Quaternion.identity, new_player.transform
        );

        ShipCore core = new_ship.GetComponent<ShipCore>();
        core.BoardPlayer(identity);  // enabled systems.

        // TODO: put this somewhere else.
        core.Mount(Item.ion_drive);
    }

}
