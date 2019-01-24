using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

	[Header ("╔═══════════════[References to Menus]══════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]
	public GameObject backMenuUI;
	[Space (10)]
	public GameObject settingsMenuUI;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Audio Sliders]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public GameObject MasterVolumeSlider;
	[Space (10)]
	public GameObject MusicVolumeSlider;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[FullScreen Buttons]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public GameObject FullScreen_Button_SliderON;
	[Space (10)]
	public GameObject FullScreen_Button_SliderOFF;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Resolution Dropdown]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public GameObject DropdownMenu;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[UI Rune Buttons]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public GameObject UI_Rune_Buttone_SliderOff;
	[Space (10)]
	public GameObject UI_Rune_Buttone_SliderRadial;
	[Space (10)]
	public GameObject UI_Rune_Buttone_SliderNumerical;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[Helping Arrow Buttons]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public GameObject HelpingArrow_Button_SliderOff;
	[Space (10)]
	public GameObject HelpingArrow_Button_SliderAuto;
	[Space (10)]
	public GameObject HelpingArrow_Button_SliderOn;

	[Header ("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (20)]
	[Header ("╔═════════════════[HealthBar Buttons]═════════════════════════════════════════════════════════════════════════════════════════════")]
	[Space (10)]

	public GameObject HealthBar_Button_SliderON;
	[Space (10)]
	public GameObject HealthBar_Button_SliderOff;

	void Awake () {
		MasterVolumeSlider.GetComponent<Slider> ().value = GetVolumeLevel ("Master_Volume");
		MusicVolumeSlider.GetComponent<Slider> ().value = GetVolumeLevel ("Music_Volume");

		DropdownMenu.GetComponent<Dropdown> ().value = QualitySettings.GetQualityLevel ();

		if (!GameManager.Instance.FullScreen) {
			FullScreen_Button_SliderON.SetActive (false);
			FullScreen_Button_SliderOFF.SetActive (true);
		}

		if (GameManager.Instance.UIRuneEnabled != "RADIAL") {
			if (GameManager.Instance.UIRuneEnabled == "OFF") {
				UI_Rune_Buttone_SliderOff.SetActive (false);
				UI_Rune_Buttone_SliderRadial.SetActive (true);
				UI_Rune_Buttone_SliderNumerical.SetActive (true);
			}
			if (GameManager.Instance.UIRuneEnabled == "NUMERICAL") {
				UI_Rune_Buttone_SliderOff.SetActive (true);
				UI_Rune_Buttone_SliderRadial.SetActive (true);
				UI_Rune_Buttone_SliderNumerical.SetActive (false);
			}
		}

		if (GameManager.Instance.HelpArrowsEnabled != "AUTO") {
			if (GameManager.Instance.HelpArrowsEnabled == "OFF") {
				HelpingArrow_Button_SliderOff.SetActive (false);
				HelpingArrow_Button_SliderAuto.SetActive (true);
				HelpingArrow_Button_SliderOn.SetActive (true);
			}
			if (GameManager.Instance.HelpArrowsEnabled == "ON") {
				HelpingArrow_Button_SliderOff.SetActive (true);
				HelpingArrow_Button_SliderAuto.SetActive (true);
				HelpingArrow_Button_SliderOn.SetActive (false);
			}
		}

		if (!GameManager.Instance.HealthBarEnabled) {
			HealthBar_Button_SliderON.SetActive (false);
			HealthBar_Button_SliderOff.SetActive (true);
		}
	}

	void LateUpdate () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (settingsMenuUI.activeSelf) {
				Back ();
			}
		}
		//Debug.Log (GameManager.Instance.HealthBarEnabled);
	}

	public void Back () {
		//Debug.Log ("Loading Pause Menu...");
		settingsMenuUI.SetActive (false);
		backMenuUI.SetActive (true);
	}

	public void SetMasterVolume (float volume) {
		GameManager.Instance.audioMixer.SetFloat ("Master_Volume", volume);
	}

	public void SetMusicVolume (float volume) {
		GameManager.Instance.audioMixer.SetFloat ("Music_Volume", volume);
	}

	public void EnableFullScreen () {
		Screen.fullScreen = true;
		GameManager.Instance.FullScreen = true;
	}

	public void DisableFullScreen () {
		Screen.fullScreen = false;
		GameManager.Instance.FullScreen = false;
	}

	public void SetQuality (int qualityIndex) {
		QualitySettings.SetQualityLevel (qualityIndex);
	}

	public void UI_Rune_OFF () {
		GameManager.Instance.UIRuneEnabled = "OFF";
	}

	public void UI_Rune_RADIAL () {
		GameManager.Instance.UIRuneEnabled = "RADIAL";
	}

	public void UI_Rune_NUMERICAL () {
		GameManager.Instance.UIRuneEnabled = "NUMERICAL";
	}

	public void HelpArrows_OFF () {
		GameManager.Instance.HelpArrowsEnabled = "OFF";
	}

	public void HelpArrows_AUTO () {
		GameManager.Instance.HelpArrowsEnabled = "AUTO";
	}

	public void HelpArrows_ON () {
		GameManager.Instance.HelpArrowsEnabled = "ON";
	}

	public void EnableHealthBars () {
		GameManager.Instance.HealthBarEnabled = true;
	}

	public void DisableHealthBars () {
		GameManager.Instance.HealthBarEnabled = false;
	}

	public float GetVolumeLevel (string MixerName) {
		float value;
		bool result = GameManager.Instance.audioMixer.GetFloat (MixerName, out value);
		if (result) {
			return value;
		} else {
			return 0f;
		}
	}
}