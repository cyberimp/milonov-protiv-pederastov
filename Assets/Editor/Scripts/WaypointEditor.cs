using GachiScripts;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    [CustomEditor(typeof(WayPointsController))]
    public class WaypointEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
//        EditorGUILayout.ToggleLeft("Play On Awake", serializedObject.FindProperty("isMoving").boolValue);
            EditorList.Show(serializedObject.FindProperty("waypoints"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onFinish"), true);
            serializedObject.ApplyModifiedProperties();
        }

        private void OnSceneGUI()
        {
            var edited = target as WayPointsController;
            //Handles.BeginGUI();
            var targetTrans = edited.transform;
            var oldHandle = Vector3.zero;
            Vector3 newHandle;
            Vector3 modHandle;
            if (edited.waypoints == null) return;
            for (var i = 0; i < edited.waypoints.Length; i++)
            {
                newHandle = new Vector3(edited.waypoints[i].x, edited.waypoints[i].y);
                var position = targetTrans.position;
                Handles.DrawLine(position + oldHandle, position + newHandle);
                modHandle = Handles.FreeMoveHandle(position + newHandle,
                                Quaternion.identity, 0.04f, Vector3.one * 0.4f, Handles.DotHandleCap) -
                            position;
                oldHandle = newHandle;
                if (modHandle == oldHandle) continue;
                Undo.RecordObject(edited, "Move");
                edited.waypoints[i].x = modHandle.x;
                edited.waypoints[i].y = modHandle.y;
            }

            //Handles.EndGUI();
        }
    }
}