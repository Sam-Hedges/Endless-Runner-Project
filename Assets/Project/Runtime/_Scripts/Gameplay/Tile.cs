using System;
using UnityEngine;
using UnityEditor;
using Cinemachine;

namespace Project.Runtime._Scripts.Gameplay
{
    public enum TileType
    {
        STRAIGHT,
        LEFT,
        RIGHT,
        BI,
        QUAD
    }
    
    public class Tile : MonoBehaviour
    {
        public TileType type;
        public Transform startPivot;
        public Transform endPivot;
        public MeshRenderer roadRenderer;
        public Vector3 direction;


        [HideInInspector][SerializeField] public CinemachinePath[] lanes = new CinemachinePath[3];
        [HideInInspector][SerializeField] public CinemachinePath[] leftLanes = new CinemachinePath[3];
        [HideInInspector][SerializeField] public CinemachinePath[] rightLanes = new CinemachinePath[3];

        [HideInInspector][SerializeField] public BoxCollider[] turnTriggers = new BoxCollider[2];
        [HideInInspector][SerializeField] public BoxCollider[] disableInputTrigger;


        // Custom Editor to make tile component only reveal the relevant
        // Arrays / variables for the CinemachinePath lanes
        [CustomEditor(typeof(Tile))]
        public class TileScriptEditor : Editor
        {
            private SerializedProperty s_Lanes;
            private SerializedProperty s_LeftLanes;
            private SerializedProperty s_RightLanes;
            private SerializedProperty s_TurnTriggers;
            private SerializedProperty s_DisableInputTrigger;

            private void OnEnable()
            {
                s_Lanes = serializedObject.FindProperty("lanes");
                s_LeftLanes = serializedObject.FindProperty("leftLanes");
                s_RightLanes = serializedObject.FindProperty("rightLanes");
                s_TurnTriggers = serializedObject.FindProperty("turnTriggers");
                s_DisableInputTrigger = serializedObject.FindProperty("disableInputTrigger");
            }

            public override void OnInspectorGUI()
            {
                serializedObject.Update();

                // Call normal GUI (displaying "a" and any other variables you might have)
                base.OnInspectorGUI();

                // Reference the variables in the script
                Tile tile = (Tile)target;

                switch (tile.type)
                {
                    case TileType.LEFT:
                    case TileType.RIGHT:
                    case TileType.STRAIGHT:
                        EditorGUILayout.PropertyField(s_Lanes, new GUIContent("Lanes"));
                        serializedObject.ApplyModifiedProperties();
                        break;
                    case TileType.BI:
                        EditorGUILayout.PropertyField(s_TurnTriggers, new GUIContent("Turn Triggers"));
                        EditorGUILayout.PropertyField(s_DisableInputTrigger, new GUIContent("Disable Input Trigger"));
                        EditorGUILayout.PropertyField(s_LeftLanes, new GUIContent("Left Lanes"));
                        EditorGUILayout.PropertyField(s_RightLanes, new GUIContent("Right Lanes"));
                        serializedObject.ApplyModifiedProperties();
                        break;
                    case TileType.QUAD:
                        EditorGUILayout.PropertyField(s_TurnTriggers, new GUIContent("Turn Triggers"));
                        EditorGUILayout.PropertyField(s_DisableInputTrigger, new GUIContent("Disable Input Trigger"));
                        EditorGUILayout.PropertyField(s_Lanes, new GUIContent("Straight Lanes"));
                        EditorGUILayout.PropertyField(s_LeftLanes, new GUIContent("Left Lanes"));
                        EditorGUILayout.PropertyField(s_RightLanes, new GUIContent("Right Lanes"));
                        serializedObject.ApplyModifiedProperties();
                        break;
                }
            }
        }
    }
}
