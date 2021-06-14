
using Platformer.Model.Definitions;
using UnityEngine;

namespace Platformer.UI.HUD.Dialogs
{
	public class PersonalizedDialogBoxController : DialogBoxController
	{
		[SerializeField] private DialogContent _right;
		protected override DialogContent CurrentContent => CurrentSentence.DialogBoxSide == Side.Left ? _content : _right;

		protected override void OnStartDialogAnimation()
		{
			_right.gameObject.SetActive(CurrentSentence.DialogBoxSide == Side.Right);
			_content.gameObject.SetActive(CurrentSentence.DialogBoxSide == Side.Left);

			base.OnStartDialogAnimation();
		}
	}
}

