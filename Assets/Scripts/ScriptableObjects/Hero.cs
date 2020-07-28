using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero", menuName ="Hero")]
public class Hero : ScriptableObject
{
    public int index;
    public new string name;

    public Sprite heroIcon;
}
