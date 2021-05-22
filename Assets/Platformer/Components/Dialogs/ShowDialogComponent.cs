using Platformer.Model.Definitions;
using System.Collections;
using UnityEngine;

namespace Platformer.Components.Dialogs
{
	public class ShowDialogComponent : MonoBehaviour
	{

		[SerializeField] private Mode _mode;
		[SerializeField] private DialogData _bound;
		[SerializeField] private DialogDef _external;

		public enum Mode
		{
			Bound,
			External
		}
	}
}