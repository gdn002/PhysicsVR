using UnityEngine;
using System.Collections;

public class HandleTool : Grabbable
{
    public Transform handleTop;
    public Transform handleBottom;

    private Vector3 grabPoint;

    public override Vector3 GrabPoint()
    {
        return transform.TransformPoint(grabPoint);
    }

    public override void GrabAction(Transform grabber)
    {
        grabPoint = FindGrabPosition(grabber.position);
    }

    private Vector3 FindGrabPosition(Vector3 point)
    {
        Vector3 vA = handleBottom.transform.localPosition;
        Vector3 vB = handleTop.transform.localPosition;

        point = transform.InverseTransformPoint(point);

        Vector3 v1 = point - vA;
        Vector3 v2 = (vB - vA).normalized;

        float d = Vector3.Distance(vA, vB);
        float t = Vector3.Dot(v2, v1);

        if (t <= 0)
            return vA;

        if (t >= d)
            return vB;

        Vector3 v3 = v2 * t;
        Vector3 grabPoint = vA + v3;

        return grabPoint;
    }
}
