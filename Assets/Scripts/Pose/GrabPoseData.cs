using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "GrabPoseData", menuName = "GrabPoseData", order = 1)]
public class GrabPoseData : ScriptableObject
{
    public Vector3 position;
    public Vector3 angles;
}
