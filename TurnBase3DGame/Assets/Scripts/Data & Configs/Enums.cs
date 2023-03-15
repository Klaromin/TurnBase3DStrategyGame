using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    
}

public enum GridVisualType
{
    White,
    Blue,
    Red,
    SoftRed,
    Yellow
}

public enum State
{
    WaitingForEnemyTurn,
    TakingTurn,
    Busy,
}
