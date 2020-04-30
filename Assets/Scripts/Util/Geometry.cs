using UnityEngine;
using UnityEditor;

public class Geometry
{
    public static Vector3 OrientTorque(Vector3 torque)
    {
        // Quaternion's Euler conversion results in (0-360)
        // For torque, we need -180 to 180.

        return new Vector3
        (
            torque.x > 180f ? 180f - torque.x : torque.x,
            torque.y > 180f ? 180f - torque.y : torque.y,
            torque.z > 180f ? 180f - torque.z : torque.z
        );
    }
}