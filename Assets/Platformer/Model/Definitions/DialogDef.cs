using System.Collections;
using UnityEngine;

namespace Platformer.Model.Definitions
{
	[CreateAssetMenu(menuName ="Defs/Dialog", fileName = "Dialog")]
	public class DialogDef : ScriptableObject
	{

		[SerializeField] private DialogData _data;
		public DialogData Data => _data;
	}
}