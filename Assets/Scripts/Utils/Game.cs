using UnityEngine;

public static class Game
{
    public static void Pause()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
