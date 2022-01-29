using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomController))]
public class RoomControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RoomController controller = (RoomController)target;

        if (GUILayout.Button("Teleport to map"))
        {
            controller.Teleport();
        }
    }
}
