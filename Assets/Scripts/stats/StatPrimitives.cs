using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// What ship systems will read, universally.
public abstract class AStat<T> {

    public AStat(string _name){
        name = _name;
    }

    public abstract T val();

    public string GetName(){
        return name;
    }

    private string name;
}

[Serializable]
public class BasicInt : AStat<int> {

    public BasicInt(string _name)
        : base(_name) {
    }

    public override int val(){
        return start * mult;
    }

    // [SerializeField]
    public int start;
    public int mult = 1;
}

[Serializable]
public class BasicFloat : AStat<float> {

    public BasicFloat(string _name)
        : base(_name) {
    }

    public override float val(){
        return start * mult;
    }

    public float start;
    public float mult = 1;
}

[Serializable]
public class StepInt : AStat<int> {

    public StepInt(string _name)
        : base(_name){
    }

    public override int val(){
        return (start + (step * units)) * mult;
    }

    public int start;
    public int step;
    public int units;
    public int mult = 1;
}

[Serializable]
public class BasicBool : AStat<bool>{

    public BasicBool(string _name)
        : base(_name){
    }

    public override bool val(){
        return value;
    }

    public bool value;
}

[Serializable]
public class VoteBool : AStat<bool> {

    public VoteBool(string _name)
        : base(_name){
    }

    public override bool val(){
        return true_count > 0;
    }

    public int true_count;
}