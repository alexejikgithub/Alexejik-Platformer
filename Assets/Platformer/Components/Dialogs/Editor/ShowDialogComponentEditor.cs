using UnityEditor;


namespace Platformer.Components.Dialogs.Editor
{
	public class ShowDialogComponentEditor : UnityEditor.Editor
	{
		private SerializedProperty _modeProperty;
		private void OnEnable()
		{
			_modeProperty = serializedObject.FindProperty("_mode");
		}
		public override void OnInspectorGUI()
		{
			EditorGUILayout.PropertyField(_modeProperty);
		}
	}
}