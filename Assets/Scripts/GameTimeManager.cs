using UnityEngine;


public class GameTimeManager : MonoBehaviour
{


    void OnEnable()
    {

        GameEvents.MenuButtonPushed += PauseGame;

        GameEvents.MenuCloseButtonPushed += ResumeGame;

        GameEvents.InstructionButtonPushed += PauseGame;

        GameEvents.InstructionCloseButtonPushed += ResumeGame;

        GameEvents.DisplayTutorial += PauseGame;

        GameEvents.TutorialClose += ResumeGame;

    }


    void OnDisable()
    {
        GameEvents.MenuButtonPushed -= PauseGame;

        GameEvents.MenuCloseButtonPushed -= ResumeGame;

        GameEvents.InstructionButtonPushed -= PauseGame;

        GameEvents.InstructionCloseButtonPushed -= ResumeGame;

        GameEvents.DisplayTutorial -= PauseGame;

        GameEvents.TutorialClose -= ResumeGame;

    }

    // ゲームを再開するGameManegerより呼び出し
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }


    private void PauseGame()
    {
        Time.timeScale = 0f;
    }


 


}
