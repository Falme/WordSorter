using System;
using TMPro;
using UnityEngine;

namespace WordSorter
{
	public enum PopupType
	{
		OK = 0,
		YES_NO = 1,
		END_LEVEL = 2
	}

	public class Popup : MonoBehaviour
	{
		public static Popup Instance { get; private set; }

		[SerializeField] private TextMeshProUGUI messageText;
		[SerializeField] private Canvas canvasYesNo, canvasOK, canvasScore;
		[SerializeField] private Score score;

		private Canvas canvas;
		private CanvasGroup canvasGroup;

		private Action buttonACallback, buttonBCallback;

		public int MovesCounter { get; set; }

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
				return;
			}

			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		private void Start()
		{
			canvas = GetComponent<Canvas>();
			canvasGroup = GetComponent<CanvasGroup>();
		}

		public void OpenPopup(string bodyText, PopupType popupType, Action buttonACallback = null, Action buttonBCallback = null)
		{
			EnableCanvas(true);
			messageText.SetText(Localization.GetLocalizedMessage(bodyText));

			this.buttonACallback = buttonACallback;
			this.buttonBCallback = buttonBCallback;

			switch (popupType)
			{
				case PopupType.OK:
					ShowOKButton();
					break;
				case PopupType.YES_NO:
					ShowDecisionButtons();
					break;
				case PopupType.END_LEVEL:
					score.ShowResult(MovesCounter);
					ShowOKButton();
					ShowScore();
					break;
			}
		}

		public void OnClickYes()
		{
			buttonACallback?.Invoke();
			EnableCanvas(false);
			score.Restart();
		}

		public void OnClickNo()
		{
			buttonBCallback?.Invoke();
			EnableCanvas(false);
		}

		private void ShowDecisionButtons()
		{
			canvasYesNo.enabled = true;
			canvasOK.enabled = false;
			canvasScore.enabled = false;
		}

		private void ShowOKButton()
		{
			canvasYesNo.enabled = false;
			canvasOK.enabled = true;
			canvasScore.enabled = false;
		}

		private void ShowScore()
		{
			canvasScore.enabled = true;
		}

		private void EnableCanvas(bool enable)
		{
			canvas.enabled = enable;
			canvasGroup.interactable = enable;
			canvasGroup.blocksRaycasts = enable;
		}
	}
}