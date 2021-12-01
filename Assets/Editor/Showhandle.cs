using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class HandleExample : MonoBehaviour
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawGameObjectName(Transform transform, GizmoType gizmoType)
    {
        if (transform.gameObject.CompareTag("NavNode") || transform.gameObject.CompareTag("FleeNode")) {
            Handles.Label(transform.position, transform.gameObject.name);
        }
    }
}