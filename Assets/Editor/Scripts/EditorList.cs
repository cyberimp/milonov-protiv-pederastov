using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    public static class EditorList
    {
        private static GUIContent
            duplicateButtonContent = new GUIContent("+", "duplicate"),
            deleteButtonContent = new GUIContent("-", "delete"),
            addButtonContent = new GUIContent("+", "add");

        private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

        public static void Show(SerializedProperty list)
        {
            EditorGUILayout.PropertyField(list);
            EditorGUI.indentLevel += 1;
            if (list.isExpanded)
            {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
                //EditorGUILayout.BeginHorizontal();
                //EditorGUILayout.IntField("X", 0);
                //EditorGUILayout.IntField("Y", 0);
                //EditorGUILayout.IntField("Z", 0);
                //EditorGUILayout.EndHorizontal();
                if (list.arraySize > 0)
                    for (int i = 0; i < list.arraySize; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
                        ShowButtons(list, i);
                        EditorGUILayout.EndHorizontal();
                    }
                else
                {
                    if (GUILayout.Button(addButtonContent))
                        list.InsertArrayElementAtIndex(0);
                }
            }

            EditorGUI.indentLevel -= 1;
        }

        private static void ShowButtons(SerializedProperty list, int index)
        {
            if (GUILayout.Button(duplicateButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth))
            {
                list.InsertArrayElementAtIndex(index);
                if (index < list.arraySize - 2)
                {
                    Vector3 vector1 = list.GetArrayElementAtIndex(index).vector3Value;
                    Vector3 vector2 = list.GetArrayElementAtIndex(index + 2).vector3Value;
                    Vector3 newVector = (vector2 + vector1) / 2;
                    newVector.z = vector1.z;
                    list.GetArrayElementAtIndex(index + 1).vector3Value = newVector;
                }
            }

            if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight, miniButtonWidth))
            {
                list.DeleteArrayElementAtIndex(index);
            }
        }
    }
}