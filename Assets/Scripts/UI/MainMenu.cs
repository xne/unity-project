using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button backButton;

    private readonly GameObjectStack stack = new();

    private void Start()
    {
        stack.Push(mainMenu);
    }

    private void OnEnable()
    {
        playButton.onClick.AddListener(Play);
        settingsButton.onClick.AddListener(Settings);
        quitButton.onClick.AddListener(Quit);

        backButton.onClick.AddListener(Back);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();

        backButton.onClick.RemoveAllListeners();
    }

    public void Play()
    {
        SceneManager.LoadScene("Level01");
    }

    public void Settings()
    {
        stack.Push(settingsMenu);
    }

    public void Quit()
    {
        Game.Quit();
    }

    public void Back()
    {
        stack.Pop();
    }
}
