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
		[SerializeField] private Canvas canvasYesNo, canvasOK;

		private Canvas canvas;
		private CanvasGroup canvasGroup;

		private Action buttonACallback, buttonBCallback;

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
			messageText.text = bodyText;

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
			}
		}

		public void OnClickYes()
		{
			buttonACallback?.Invoke();
			EnableCanvas(false);
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
		}

		private void ShowOKButton()
		{
			canvasYesNo.enabled = false;
			canvasOK.enabled = true;
		}

		private void EnableCanvas(bool enable)
		{
			canvas.enabled = enable;
			canvasGroup.interactable = enable;
			canvasGroup.blocksRaycasts = enable;
		}
	}
}