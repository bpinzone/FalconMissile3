using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// Purpose: Just data for stats. No functionality.
[Serializable]
public class Stats {

    public SublightDrive sublight_drive = new SublightDrive();
    public ReactorPlant reactor_plant = new ReactorPlant();
    public Consumable consumable = new Consumable();
    public Gun gun = new Gun();
    public Bomb bomb = new Bomb();
}

[Serializable]
public class SublightDrive {

    public StepInt rotation = new StepInt("rotation");  // deg/sec
    public StepInt speed = new StepInt("speed");
    public StepInt ab_speed = new StepInt("ab speed");
    public StepInt thrust = new StepInt("thrust");
    public StepInt ab_thrust = new StepInt("ab thrust");
    public BasicInt ab_cost = new BasicInt("ab cost");  // energy/sec
}

[Serializable]
public class ReactorPlant {
    public StepInt max_energy = new StepInt("max energy");
    public StepInt recharge = new StepInt("recharge");  // energy/sec
}

[Serializable]
public class Consumable {
    public Burst burst = new Burst();
}

[Serializable]
public class Projectile {

    public Projectile(string name) {
        damage = new BasicInt(name + " damage");
        delay = new BasicInt(name + " delay");  // ms
        energy = new BasicInt(name + " energy");
        speed = new StepInt(name + " speed");
        bounces = new BasicInt(name + " bounces");
    }

    public BasicInt damage;
    public BasicInt delay;
    public BasicInt energy;
    public StepInt speed;
    public BasicInt bounces;
}

[Serializable]
public class Burst {
    public Projectile itself = new Projectile("burst");
    public BasicInt count = new BasicInt("burst count");
}

[Serializable]
public class Gun : Projectile{
    public Gun() : base("gun"){
    }
}

[Serializable]
public class Bomb {
    public Projectile itself = new Projectile("bomb");
    public Projectile shrap = new Projectile("bomb shrap");
    public BasicFloat prox = new BasicFloat("bomb prox");
}