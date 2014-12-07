using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    private bool isPaused;
    private bool isNewGame;
    private bool flag;
    public Image screenFader;
    public Button newGame;
    public Button continueGame;
    public Button exit;

    void Awake()
    {
        flag = false;
        var c = screenFader.color;
        screenFader.color = new Color(c.r,c.g,c.b,0);
        isNewGame = false;
        flag = true;
        isPaused = false;
        Time.timeScale = 0;
    }

    Vector3 ex;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Joystick1Button6))
            Application.LoadLevel(Application.loadedLevel);
       
        if (Input.GetKeyDown(KeyCode.Joystick1Button7) ||Input.GetKeyDown(KeyCode.Escape) || flag)
        {
            flag = false;
            if (isPaused)
            {
                EventSystem.current.SetSelectedGameObject(newGame.gameObject);
                Time.timeScale = 0;
                isPaused = false;
                if (!isNewGame)
                {
                    ex = continueGame.transform.position;
                    continueGame.transform.position = new Vector3(ex.x, ex.y, 0);
                }
                isNewGame = false;

                ex = newGame.transform.position;
                newGame.transform.position = new Vector3(ex.x, ex.y, 0);

                ex = exit.transform.position;
                exit.transform.position = new Vector3(ex.x, ex.y, 0);

                var c = screenFader.color;
                screenFader.color = new Color(c.r, c.g, c.b, 1);
             
            }
            else
            {
                ex = newGame.transform.position;
                newGame.transform.position = new Vector3(ex.x, ex.y, -10000);

                ex = continueGame.transform.position;
                continueGame.transform.position = new Vector3(ex.x, ex.y, -10000);

                ex = exit.transform.position;
                exit.transform.position = new Vector3(ex.x, ex.y, -10000);

                var c = screenFader.color;
                screenFader.color = new Color(c.r, c.g, c.b, 0);

                Time.timeScale = 1;
                isPaused = true;
            }
        }
    }

    public void OnNewGameClick()
    {
        isPaused = false;
        isNewGame = false;
        Application.LoadLevel("MainScene");
    }

    public void OnContinueClick()
    {
        flag = true;
        isPaused = false;
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

}
