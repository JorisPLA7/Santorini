using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//THIS IS A STATIC SCRIPT FOR HOLDING DATA
public static class Settings{

    public enum Modes { godless,gods};

    public static Modes selectedMode;

    public static GodCard playerOneChoice;
    public static GodCard playerTwoChoice;
}
