using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UnitType {
    X,
    O
}
public class Unit : MonoBehaviour
{
    public string unitName;
    public UnitType UnitType;


    public UnitType getUnitType() {
        return UnitType;
    }

    void Start() {
    }
}
