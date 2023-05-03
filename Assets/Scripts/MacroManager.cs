using UnityEngine;
using extOSC;

public class MacroManager : MonoBehaviour
{
    public static MacroManager Instance { get; private set; }
    [HideInInspector] public float AbletonFactor = 0.00390625f;
}