using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu_UI : MonoBehaviour {
	public Canvas pauseMenuCanvas;
	
	// Make sure the pause system is properly displaying upon start of level
	void Start () {
		pauseMenuCanvas.enabled = false;
	}
	
	// Models the behavior for Pause Button
	public void PausePressed() {
		pauseMenuCanvas.enabled = true;
		
		Time.timeScale = 0; // This pauses the game
	}
	
	// Models the behavior for Resume Button
	public void ResumePressed() {
		pauseMenuCanvas.enabled = false;
		
		Time.timeScale = 1; // This resumes the game
	}
	
	// Models the behavior for Quit Button
	public void QuitPressed() {
		Time.timeScale = 1; 
	}
}