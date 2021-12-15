using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpikeController))]
public class SpikeControllerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		SpikeController spikeController = (SpikeController)target;
		if(GUILayout.Button("Activate Spikes"))
		{
			spikeController.StartSpikes();
		}
	}
}
