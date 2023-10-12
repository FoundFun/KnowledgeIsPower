using CodeBase.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker spawnMarker, GizmoType gizmo)
        {
            CircleGizmo(spawnMarker.transform, 0.5f, Color.red);
        }

        private static void CircleGizmo(Transform transform, float radius, Color color)
        {
            Gizmos.color = color;

            Vector3 position = transform.position;
            
            Gizmos.DrawSphere(position, radius);
        }
    }
}