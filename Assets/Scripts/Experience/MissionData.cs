using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "Scriptable Objects/MissionData")]
public class MissionData : ScriptableObject
{
    public List<String> missionDescriptions;
}
