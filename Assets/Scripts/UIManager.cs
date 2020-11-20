using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance { get; private set; }
    public List<GameObject> list_Of_Prefabs;

    private List<GameObject> uiCanvases;

    private Button aboutButton;
    private Button mainMenuButton_aboutScene;
    private Button mainMenuButton_creditsScene;
    private Button settingsButton;
    private Button playGameButton;
    private Button quitButton;
    private PlayerManager playerManager;
    private TextMeshProUGUI level_info_text;
    private TextMeshProUGUI lives_left;

    private string myActiveScene = "Scene_Main";

    private void Awake()
    {
        if (_instance == null)
        {
            playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
            uiCanvases = new List<GameObject>();

            _instance = this;
            DontDestroyOnLoad(this);

            myActiveScene = SceneManager.GetActiveScene().name;

            // storing canvases to the 1 list
            foreach (GameObject prefab in list_Of_Prefabs)
            {
                GameObject newObj = Instantiate(prefab);
                newObj.name = prefab.name;
                newObj.transform.SetParent(transform);
                uiCanvases.Add(newObj);
            }

            if (myActiveScene == "Scene_Main_Menu")
            {
                initMainScene();
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setAllCanvasesToInactive()
    {
        foreach (GameObject canvasgo in uiCanvases)
        {
            canvasgo.SetActive(false);
        }
    }

    public void initMainScene()
    {
        setAllCanvasesToInactive();
        GameObject go = uiCanvases[0];
        go.SetActive(true);
        aboutButton = GameObject.Find("Button_Play").GetComponent<Button>();
        aboutButton.onClick.AddListener(() => loadSceneByNumber(1));

        settingsButton = GameObject.Find("Button_About").GetComponent<Button>();
        settingsButton.onClick.AddListener(() => loadSceneByNumber(5));

        playGameButton = GameObject.Find("Button_Credits").GetComponent<Button>();
        playGameButton.onClick.AddListener(() => loadSceneByNumber(6));

        quitButton = GameObject.Find("Button_Quit").GetComponent<Button>();
        quitButton.onClick.AddListener(() => quitGame(0));
        myActiveScene = "Scene_Main";
    }

    public void loadSceneByNumber(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void activateCanvas(int canvasNum)
    {
        setAllCanvasesToInactive();
        GameObject go = uiCanvases[canvasNum];
        go.SetActive(true);
    }

    void OnLevelWasLoaded(int index)
    {
        if (uiCanvases == null)
        {
            return;
        }

        if (index == 0)
        {
            activateCanvas(0);
        }

        if (index == 1 || index == 2 || index == 3)
        {
            activateCanvas(1);
            level_info_text = GameObject.Find("Text_TMP_Level").GetComponent<TextMeshProUGUI>();
            lives_left = GameObject.Find("Text_TMP_Lives_Count").GetComponent<TextMeshProUGUI>();
            updateHUD(index);
        }

        if (index == 4)
        {
            activateCanvas(2);
            
        }

        // about scene
        if (index == 5)
        {
            activateCanvas(3);
            if (mainMenuButton_aboutScene == null)
            {
                mainMenuButton_aboutScene = GameObject.Find("Button_Main_Menu").GetComponent<Button>();
                mainMenuButton_aboutScene.onClick.AddListener(() => loadSceneByNumber(0));
            }
        }

        // Credits Scene
        if (index == 6)
        {
            activateCanvas(4);
            if (mainMenuButton_creditsScene == null)
            {
                mainMenuButton_creditsScene = GameObject.Find("Button_Main_Menu").GetComponent<Button>();
                mainMenuButton_creditsScene.onClick.AddListener(() => loadSceneByNumber(0));
            }
        }
    }

    public void updateHUD(int index)
    {
        level_info_text.text = index.ToString() + "/3";
        lives_left.text = "X " + playerManager.player.getLives().ToString();
    }

    public void updateHUD()
    {
        lives_left.text = "X " + playerManager.player.getLives().ToString();
    }

    public void quitGame(int ignoreLevel)
    {
        Debug.Log("quit called");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void showGameOverScreen()
    {
        loadSceneByNumber(7);
    }
}
