using UnityEngine;
using UnityEditor;

namespace NorthLab.CinemaIKDemo
{
    [CustomEditor(typeof(CinemaIK))]
    public class CinemaIKEditor : Editor
    {

        //loading all properties
        private SerializedProperty animator;
        private SerializedProperty gameMode;

        private SerializedProperty lookAt;
        private SerializedProperty lookAtPos;
        private SerializedProperty weight;

        private SerializedProperty leftHandPositionWeight;
        private SerializedProperty leftHandRotationWeight;
        private SerializedProperty leftHandIKPos;
        private SerializedProperty leftHandIKRot;
        private SerializedProperty leftHandIKTarget;

        private SerializedProperty rightHandPositionWeight;
        private SerializedProperty rightHandRotationWeight;
        private SerializedProperty rightHandIKPos;
        private SerializedProperty rightHandIKRot;
        private SerializedProperty rightHandIKTarget;

        private SerializedProperty leftFootPositionWeight;
        private SerializedProperty leftFootRotationWeight;
        private SerializedProperty leftFootIKPos;
        private SerializedProperty leftFootIKRot;
        private SerializedProperty leftFootIKTarget;

        private SerializedProperty rightFootPositionWeight;
        private SerializedProperty rightFootRotationWeight;
        private SerializedProperty rightFootIKPos;
        private SerializedProperty rightFootIKRot;
        private SerializedProperty rightFootIKTarget;

        private SerializedProperty showGizmos;
        private SerializedProperty showBoneLines;

        private CinemaIK script;

        private GUIStyle labelStyle;

        private void OnEnable()
        {
            //finding all properties
            animator = serializedObject.FindProperty("animator");
            gameMode = serializedObject.FindProperty("gameMode");

            lookAt = serializedObject.FindProperty("lookAt");
            lookAtPos = serializedObject.FindProperty("lookAtPos");
            weight = serializedObject.FindProperty("weight");

            leftHandPositionWeight = serializedObject.FindProperty("leftHandPositionWeight");
            leftHandRotationWeight = serializedObject.FindProperty("leftHandRotationWeight");
            leftHandIKPos = serializedObject.FindProperty("leftHandIKPos");
            leftHandIKRot = serializedObject.FindProperty("leftHandIKRot");
            leftHandIKTarget = serializedObject.FindProperty("leftHandIKTarget");

            rightHandPositionWeight = serializedObject.FindProperty("rightHandPositionWeight");
            rightHandRotationWeight = serializedObject.FindProperty("rightHandRotationWeight");
            rightHandIKPos = serializedObject.FindProperty("rightHandIKPos");
            rightHandIKRot = serializedObject.FindProperty("rightHandIKRot");
            rightHandIKTarget = serializedObject.FindProperty("rightHandIKTarget");

            leftFootPositionWeight = serializedObject.FindProperty("leftFootPositionWeight");
            leftFootRotationWeight = serializedObject.FindProperty("leftFootRotationWeight");
            leftFootIKPos = serializedObject.FindProperty("leftFootIKPos");
            leftFootIKRot = serializedObject.FindProperty("leftFootIKRot");
            leftFootIKTarget = serializedObject.FindProperty("leftFootIKTarget");

            rightFootPositionWeight = serializedObject.FindProperty("rightFootPositionWeight");
            rightFootRotationWeight = serializedObject.FindProperty("rightFootRotationWeight");
            rightFootIKPos = serializedObject.FindProperty("rightFootIKPos");
            rightFootIKRot = serializedObject.FindProperty("rightFootIKRot");
            rightFootIKTarget = serializedObject.FindProperty("rightFootIKTarget");

            showGizmos = serializedObject.FindProperty("showGizmos");
            showBoneLines = serializedObject.FindProperty("showBoneLines");

            //label text color
            labelStyle = new GUIStyle();
            labelStyle.normal.textColor = Color.white;

            //getting script
            script = (CinemaIK)target;
        }

        private void OnDisable()
        {
            Tools.hidden = false;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //foldoutstyle
            GUIStyle foldoutStyle = new GUIStyle(EditorStyles.foldout);
            foldoutStyle.fontStyle = FontStyle.Bold;

            //common
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Common", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(animator);
            EditorGUILayout.PropertyField(gameMode);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            if (animator.objectReferenceValue != null)
            {
                EditorGUILayout.Space();

                //look
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.indentLevel++;
                script.LookFoldOut = EditorGUILayout.Foldout(script.LookFoldOut, "Look", foldoutStyle);
                EditorGUI.indentLevel--;

                if (script.LookFoldOut)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(lookAt);
                    if (lookAt.objectReferenceValue == null)
                    {
                        if (GUILayout.Button("Add"))
                        {
                            GameObject go = new GameObject("LookAt");
                            lookAtPos.vector3Value = script.Animator.GetBoneTransform(HumanBodyBones.Head).position + script.transform.forward;
                            lookAt.objectReferenceValue = go.transform;
                            go.transform.position = lookAtPos.vector3Value;
                            go.transform.SetParent(script.transform, true);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    if (lookAt.objectReferenceValue != null)
                    {
                        EditorGUILayout.Space();
                        EditorGUILayout.PropertyField(weight);
                    }
                    else EditorGUILayout.HelpBox("Assign an object to 'Look At' property", MessageType.Info);
                    EditorGUILayout.Space();
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();

                //iks
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.indentLevel++;
                script.IksFoldOut = EditorGUILayout.Foldout(script.IksFoldOut, "IK", foldoutStyle);
                EditorGUI.indentLevel--;

                if (script.IksFoldOut)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    script.LeftHandFoldOut = EditorGUILayout.Foldout(script.LeftHandFoldOut, "Left hand", foldoutStyle);
                    EditorGUI.indentLevel--;
                    if (script.LeftHandFoldOut)
                    {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(leftHandIKTarget);
                        if (leftHandIKTarget.objectReferenceValue == null)
                        {
                            if (GUILayout.Button("Add"))
                            {
                                GameObject go = new GameObject("LeftHandIK");
                                leftHandIKPos.vector3Value = script.Animator.GetBoneTransform(HumanBodyBones.LeftHand).position;
                                leftHandIKRot.quaternionValue = script.Animator.GetBoneTransform(HumanBodyBones.LeftHand).rotation;
                                leftHandIKTarget.objectReferenceValue = go.transform;
                                go.transform.position = leftHandIKPos.vector3Value;
                                go.transform.rotation = leftHandIKRot.quaternionValue;
                                go.transform.SetParent(script.transform, true);
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.PropertyField(leftHandPositionWeight);
                        EditorGUILayout.PropertyField(leftHandRotationWeight);
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndVertical();

                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    script.RightHandFoldOut = EditorGUILayout.Foldout(script.RightHandFoldOut, "Right hand", foldoutStyle);
                    EditorGUI.indentLevel--;
                    if (script.RightHandFoldOut)
                    {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(rightHandIKTarget);
                        if (rightHandIKTarget.objectReferenceValue == null)
                        {
                            if (GUILayout.Button("Add"))
                            {
                                GameObject go = new GameObject("RightHandIK");
                                rightHandIKPos.vector3Value = script.Animator.GetBoneTransform(HumanBodyBones.RightHand).position;
                                rightHandIKRot.quaternionValue = script.Animator.GetBoneTransform(HumanBodyBones.RightHand).rotation;
                                rightHandIKTarget.objectReferenceValue = go.transform;
                                go.transform.position = rightHandIKPos.vector3Value;
                                go.transform.rotation = rightHandIKRot.quaternionValue;
                                go.transform.SetParent(script.transform, true);
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.PropertyField(rightHandPositionWeight);
                        EditorGUILayout.PropertyField(rightHandRotationWeight);
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndVertical();

                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    script.LeftFootFoldOut = EditorGUILayout.Foldout(script.LeftFootFoldOut, "Left foot", foldoutStyle);
                    EditorGUI.indentLevel--;
                    if (script.LeftFootFoldOut)
                    {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(leftFootIKTarget);
                        if (leftFootIKTarget.objectReferenceValue == null)
                        {
                            if (GUILayout.Button("Add"))
                            {
                                GameObject go = new GameObject("LeftFootIK");
                                leftFootIKPos.vector3Value = script.Animator.GetBoneTransform(HumanBodyBones.LeftFoot).position;
                                leftFootIKRot.quaternionValue = script.Animator.GetBoneTransform(HumanBodyBones.LeftFoot).rotation;
                                leftFootIKTarget.objectReferenceValue = go.transform;
                                go.transform.position = leftFootIKPos.vector3Value;
                                go.transform.rotation = leftFootIKRot.quaternionValue;
                                go.transform.SetParent(script.transform, true);
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.PropertyField(leftFootPositionWeight);
                        EditorGUILayout.PropertyField(leftFootRotationWeight);
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndVertical();

                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    script.RightFootFoldOut = EditorGUILayout.Foldout(script.RightFootFoldOut, "Right foot", foldoutStyle);
                    EditorGUI.indentLevel--;
                    if (script.RightFootFoldOut)
                    {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(rightFootIKTarget);
                        if (rightFootIKTarget.objectReferenceValue == null)
                        {
                            if (GUILayout.Button("Add"))
                            {
                                GameObject go = new GameObject("RightFootIK");
                                rightFootIKPos.vector3Value = script.Animator.GetBoneTransform(HumanBodyBones.RightFoot).position;
                                rightFootIKRot.quaternionValue = script.Animator.GetBoneTransform(HumanBodyBones.RightFoot).rotation;
                                rightFootIKTarget.objectReferenceValue = go.transform;
                                go.transform.position = rightFootIKPos.vector3Value;
                                go.transform.rotation = rightFootIKRot.quaternionValue;
                                go.transform.SetParent(script.transform, true);
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.PropertyField(rightFootPositionWeight);
                        EditorGUILayout.PropertyField(rightFootRotationWeight);
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.HelpBox("Assign an animator before we start", MessageType.Info);
            }

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(showGizmos);
            EditorGUILayout.PropertyField(showBoneLines);

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void OnSceneGUI()
        {
            Tools.hidden = showGizmos.boolValue && AnimationMode.InAnimationMode() && Selection.Contains(script.gameObject);

            if (animator.objectReferenceValue == null || !showGizmos.boolValue)
                return;

            if (Application.isPlaying)
                return;

            Vector3 newLookAtPos = script.LookAtPos;

            Vector3[] newIksPos = new Vector3[4];
            Quaternion[] newIksRot = new Quaternion[4];
            newIksPos[0] = script.LeftHandIKPos;
            newIksRot[0] = script.LeftHandIKRot;
            newIksPos[1] = script.RightHandIKPos;
            newIksRot[1] = script.RightHandIKRot;
            newIksPos[2] = script.LeftFootIKPos;
            newIksRot[2] = script.LeftFootIKRot;
            newIksPos[3] = script.RightFootIKPos;
            newIksRot[3] = script.RightFootIKRot;

            SceneView.RepaintAll();

            EditorGUI.BeginChangeCheck();

            //lookat
            if (script.Animator && script.LookAt && script.LookFoldOut)
            {
                Handles.color = new Color(1, 1, 0, 0.75f);
                Handles.DrawLine(script.Animator.GetBoneTransform(HumanBodyBones.Head).position, script.LookAt.position);
                newLookAtPos = Handles.PositionHandle(script.LookAtPos, Quaternion.identity);
            }

            //iks
            if (script.Animator && script.IksFoldOut)
            {
                if (script.LeftHandIKTarget && script.LeftHandFoldOut)
                {
                    newIksPos[0] = Handles.PositionHandle(script.LeftHandIKPos, Quaternion.identity);
                    newIksRot[0] = Handles.RotationHandle(script.LeftHandIKRot, script.LeftHandIKPos);
                    Handles.Label(script.LeftHandIKPos, "   Left hand", labelStyle);
                }
                if (script.RightHandIKTarget && script.RightHandFoldOut)
                {
                    newIksPos[1] = Handles.PositionHandle(script.RightHandIKPos, Quaternion.identity);
                    newIksRot[1] = Handles.RotationHandle(script.RightHandIKRot, script.RightHandIKPos);
                    Handles.Label(script.RightHandIKPos, "   Right hand", labelStyle);
                }
                if (script.LeftFootIKTarget && script.LeftFootFoldOut)
                {
                    newIksPos[2] = Handles.PositionHandle(script.LeftFootIKPos, Quaternion.identity);
                    newIksRot[2] = Handles.RotationHandle(script.LeftFootIKRot, script.LeftFootIKPos);
                    Handles.Label(script.LeftFootIKPos, "   Left foot", labelStyle);
                }
                if (script.RightFootIKTarget && script.RightFootFoldOut)
                {
                    newIksPos[3] = Handles.PositionHandle(script.RightFootIKPos, Quaternion.identity);
                    newIksRot[3] = Handles.RotationHandle(script.RightFootIKRot, script.RightFootIKPos);
                    Handles.Label(script.RightFootIKPos, "   Right foot", labelStyle);
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                if (script.LookAt)
                {
                    Undo.RecordObject(script, "Change lookAt pos");
                    script.LookAtPos = newLookAtPos;
                    script.LookAt.position = newLookAtPos;
                }

                if (script.LeftHandIKTarget)
                {
                    Undo.RecordObject(script, "Change leftHandIKTarget");
                    script.LeftHandIKPos = newIksPos[0];
                    script.LeftHandIKRot = newIksRot[0];
                    script.LeftHandIKTarget.position = newIksPos[0];
                    script.LeftHandIKTarget.rotation = newIksRot[0];
                }
                if (script.RightHandIKTarget)
                {
                    Undo.RecordObject(script, "Change rightHandIKTarget");
                    script.RightHandIKPos = newIksPos[1];
                    script.RightHandIKRot = newIksRot[1];
                    script.RightHandIKTarget.position = newIksPos[1];
                    script.RightHandIKTarget.rotation = newIksRot[1];
                }
                if (script.LeftFootIKTarget)
                {
                    Undo.RecordObject(script, "Change leftFootIKTarget");
                    script.LeftFootIKPos = newIksPos[2];
                    script.LeftFootIKRot = newIksRot[2];
                    script.LeftFootIKTarget.position = newIksPos[2];
                    script.LeftFootIKTarget.rotation = newIksRot[2];
                }
                if (script.RightFootIKTarget)
                {
                    Undo.RecordObject(script, "Change rightFootIKTarget");
                    script.RightFootIKPos = newIksPos[3];
                    script.RightFootIKRot = newIksRot[3];
                    script.RightFootIKTarget.position = newIksPos[3];
                    script.RightFootIKTarget.rotation = newIksRot[3];
                }
            }
        }
    }
}