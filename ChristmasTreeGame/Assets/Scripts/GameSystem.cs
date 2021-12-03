using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public enum GameStates
    {
        Menu = 0,
        GamePlay,
        GamePause,
        LevelComplete,
        //New states here
        //States for camera cut scenes also need to go here (for now, could be moved to Camera Director in the future)
        NumOfGameStates
    }


    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas inPlayCanvas;
    [SerializeField] Canvas menuCanvas;
    [SerializeField] Canvas levelCompleteCanvas;
    //[SerializeField] GameObject player;
    //Animator playerAnimator;

    private GameStates newGameState = GameStates.Menu;
    private GameStates currentGameState = GameStates.Menu;
    private static GameSystem instance;

    //variables for fading between canvases
    private float fadeTime = 2.5f;
    private bool startButtonPressed = false;
    private bool startButtonLatched = false;

    public static GameSystem Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<GameSystem>();
            }
            return instance;
        }
    }

    public GameStates NewGameState
    {
        get
        {
            return newGameState;
        }

        set
        {
            newGameState = value;
            HandleGameStateTransitions();
        }
    }

    public GameStates CurrentGameState { get => currentGameState; }

    private void Awake()
    {
        levelCompleteCanvas.enabled = false;
        pauseCanvas.enabled = false;
        inPlayCanvas.enabled = true;
        menuCanvas.enabled = false;
        currentGameState = GameStates.Menu;
        newGameState = currentGameState;
        CameraDirector.Instance.SetCamera(CameraDirector.CameraList.InPlayCam);
        //playerAnimator = player.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Things that happen repeatidly per state go here
        switch (currentGameState)
        {
            case GameStates.Menu:                
                if(startButtonPressed && !startButtonLatched)
                {
                    startButtonLatched = true;
                    StartCoroutine(FadeCanvas(menuCanvas));
                    //set next camera position to blend to
                    CameraDirector.Instance.SetCamera(CameraDirector.CameraList.InPlayCam);
                }
                if(startButtonLatched && menuCanvas.GetComponent<CanvasGroup>().alpha == 0)
                {
                    //CameraDirector.Instance.SetCamera(CameraDirector.CameraList.FollowCam);
                }
                if(startButtonLatched && !CameraDirector.Instance.GetIsLive(CameraDirector.CameraList.MenuCam))
                {
                    startButtonPressed = false;
                    startButtonLatched = false;
                    NewGameState = GameStates.GamePlay;
                }
                break;
            case GameStates.GamePlay:
                /*Win conditions
                if ()
                {
                    GameSystemController.Instance.winGame();
                }
                */
                break;
            case GameStates.GamePause:
                break;
            case GameStates.LevelComplete:
                break;
            case GameStates.NumOfGameStates: //No break so flows into default
            default:
                //Do nothing
                break;
        }
    }

    private void HandleGameStateTransitions()
    {
        //Check for 1-off transition actions which should be performed immediatley
        //Update functionw will handle the cyclic state behaviors

        switch (currentGameState)
        {
            case GameStates.Menu:
                //Check transitions and execute transition actions
                if (NewGameState == GameStates.GamePlay)
                {
                    
                    inPlayCanvas.enabled = true;

                    //complete the transition
                    currentGameState = NewGameState;
                }
                else
                {
                    // no valid transitions
                    
                    NewGameState = currentGameState;
                }

                break;
            case GameStates.GamePlay:

                if (NewGameState == GameStates.LevelComplete)
                {
                    //Set camera to new level complete cam 
                    CameraDirector.Instance.SetCamera(CameraDirector.CameraList.LevelCompleteCam);
                    levelCompleteCanvas.enabled = true;
                    //player win animation?
                    //playerAnimator.SetBool("winGame", true);
                    currentGameState = NewGameState;
                }
                else if(NewGameState == GameStates.GamePause)
                {
                    //freeze players and moving objects
                    /*
                    GameObject[] helpers;
                    helpers = GameObject.FindGameObjectsWithTag("Helper");
                    foreach (GameObject helper in helpers)
                    {
                        helper.SendMessage("PauseHelper");
                    }
                    */
                    pauseCanvas.enabled = true;
                    currentGameState = NewGameState;
                }
                else
                {
                    NewGameState = currentGameState;
                }
                break;
            case GameStates.GamePause:
                if (NewGameState == GameStates.GamePlay)
                {
                    //unfreeze moving objects
                    /*
                    GameObject[] helpers;
                    helpers = GameObject.FindGameObjectsWithTag("Helper");
                    foreach (GameObject helper in helpers)
                    {
                        helper.SendMessage("ResumeHelper");
                    }
                    */
                    pauseCanvas.enabled = false;
                    currentGameState = NewGameState;
                }
                else
                {
                    NewGameState = currentGameState;
                }
                break;
           
            case GameStates.LevelComplete:

                
                // no valid transitions yet
                // maybe add retry / replay / etc.
                NewGameState = currentGameState;
                
                break;
           
            case GameStates.NumOfGameStates:
                break;
            default:
                break;
        }
        //Debug.Log("Current Game State: " + currentGameState.ToString());
    }

    public void RetryButtonPress()
    {
        if(currentGameState == GameStates.LevelComplete)
        {
            NewGameState = GameStates.GamePlay;
        }
        
    }

   

    public void GotoMenuButtonPress()
    {
        //goto main menu
        if(currentGameState == GameStates.LevelComplete)
        {
            NewGameState = GameStates.Menu;
        }
    }


    public void StartButtonPressed()
    {

        switch (currentGameState)
        {
            case GameStates.Menu:
                startButtonPressed = true;
                break;
            case GameStates.GamePlay:
                NewGameState = GameStates.GamePause;
                break;
            case GameStates.GamePause:
                NewGameState = GameStates.GamePlay;
                break;
            case GameStates.LevelComplete:
                //reload the scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case GameStates.NumOfGameStates: //removed break so that it will flow to default
            default:
                break;
        }
    }

    private IEnumerator FadeCanvas(Canvas canvas)
    {
        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();

        float rate = 1.0f / fadeTime;

        int startAlpha = 1;
        int endAlpha = 0;

        float progress = 0.0f;

        while (progress < 1.0)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);
            progress += rate * Time.deltaTime;

            yield return null;
        }
        canvasGroup.alpha = endAlpha;
        yield return new WaitForSeconds(fadeTime);
    }

    public void WinGame()
    {
        if(currentGameState == GameStates.GamePlay)
        {
            NewGameState = GameStates.LevelComplete;
        }
    }
}
