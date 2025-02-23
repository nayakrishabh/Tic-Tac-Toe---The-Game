using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonTagENUM {
    UNTAGGED,
    X,
    O,
}
public class ButtonTag : MonoBehaviour
{
    public static ButtonTag instance;
    public ButtonTagENUM ButtonTG;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void SetTag(UnitType unitTY) {
        if(unitTY == UnitType.X) {
            ButtonTG = ButtonTagENUM.X;
        }
        else if (unitTY == UnitType.O) {
            ButtonTG = ButtonTagENUM.O;
        }
    }
    public ButtonTagENUM GetTag() {
        return ButtonTG;
    }
    public void resetTag() {
        ButtonTG = ButtonTagENUM.UNTAGGED;
    }
}
