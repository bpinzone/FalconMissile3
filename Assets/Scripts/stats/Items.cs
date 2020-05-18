using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// An item has a name, and has an effect on statistics.
// An item takes a Stats, and does something with it.

public class Item {

    public readonly string name;
    public readonly string description;

    public delegate void ItemEffect(Stats stats, bool mounting);

    // affect(stats, true) will mount this item to the statistics.
    // affect(stats, false) will dismountmount this item to the statistics.
    public readonly ItemEffect affect;

    public Item(string _name, string _description, ItemEffect _affect){
        name = _name;
        description = _description;
        affect = _affect;
    }

    public static Item compression_core = new Item(
        "Compression Core", "High recharge module.", (Stats stats, bool mounting) => {
        // Is Mounting Multiplier
        int imm = mounting? 1 : -1;
        stats.reactor_plant.max_energy.start += 1200 * imm;
        stats.reactor_plant.recharge.start += 250 * imm;
    });

    public static Item ion_drive = new Item(
        "Ion Drive", "Balanced Movement.", (Stats stats, bool mounting) => {
        int imm = mounting? 1 : -1;
        stats.sublight_drive.thrust.start += 100 * imm;
        stats.sublight_drive.ab_thrust.start += 200 * imm;
        stats.sublight_drive.rotation.start += 200 * imm;
        stats.sublight_drive.speed.start += 14 * imm;
        stats.sublight_drive.ab_speed.start += 20 * imm;
        stats.sublight_drive.ab_cost.start += 400 * imm;
    });

    public static Item pulse_laser = new Item(
        "Pulse Laser", "Balanced Gun", (Stats stats, bool mounting) => {
        int imm = mounting? 1 : -1;
        stats.gun.delay.start += 200 * imm;
        stats.gun.damage.start += 200 * imm;
        stats.gun.energy.start += 100 * imm;
        stats.gun.speed.start += 20 * imm;
    });

    public static Item _ = new Item(
        "", "", (Stats stats, bool mounting) => {
        int imm = mounting? 1 : -1;
    });
}
