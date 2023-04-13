using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GuardManager))]
public class GuardManagerEditor : Editor
{

    private void OnSceneGUI()
    {
        GuardManager _guard = (GuardManager)target;

        Color c = Color.green;
        if (_guard.alertStage == AlertStage.Intrigued)
            c = Color.Lerp(Color.green, Color.red, _guard.alertLevel / 100f);
        else if (_guard.alertStage == AlertStage.Alerted)
            c = Color.red;

        Handles.color = new Color(c.r, c.g, c.b, 0.3f);
        Handles.DrawSolidArc(
            _guard.transform.position,
            _guard.transform.up,
            Quaternion.AngleAxis(-_guard.fovAngle / 2f, _guard.transform.up) * _guard.transform.forward,
            _guard.fovAngle,
            _guard.fov);

        Handles.color = c;
        _guard.fov = Handles.ScaleValueHandle(
            _guard.fov,
            _guard.transform.position + _guard.transform.forward * _guard.fov,
            _guard.transform.rotation,
            3,
            Handles.SphereHandleCap,
            1);
    }

}
