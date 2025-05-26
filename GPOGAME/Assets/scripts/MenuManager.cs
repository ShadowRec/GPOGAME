using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public RectTransform mainMenuPanel;
    public RectTransform runSetupPanel;
    public RectTransform progressPanel;
    public RectTransform settingsPanel;

    public float slideDuration = 0.3f;
    public Vector2 slideOffset = new Vector2(1920, 0); // смещение по оси X (для FullHD экрана)


    public void OnNewRun()
    {
        ShowPanel(mainMenuPanel, runSetupPanel);
    }

    public void OnNewRunBack()
    {
        ShowPanel(runSetupPanel, mainMenuPanel);
    }

    public void OpenProgress()
    {
        ShowPanel(mainMenuPanel, progressPanel);
    }

    public void OpenProgressBack()
    {
        ShowPanel(progressPanel, mainMenuPanel);
    }

    public void OpenSettings()
    {
        ShowPanel(mainMenuPanel, settingsPanel);
    }

    public void OpenSettingsBack()
    {
        ShowPanel(settingsPanel, mainMenuPanel);
    }



    public void OnExitClicked()
    {
        Application.Quit();
        Debug.Log("Выход из игры"); // Работает только в редакторе

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

    public void LoadGameScene()
    {
        GameManager game = new GameManager();
        //Вызов создания уровня
        SceneManager.LoadScene("GameScene"); // заменить на имя сцены уровня
    }

    private void ShowPanel(RectTransform from, RectTransform to)
    {
        StartCoroutine(SlidePanels(from, to));
    }

    private IEnumerator SlidePanels(RectTransform from, RectTransform to)
    {
        Vector2 offScreenPos = slideOffset;

        // Вычислить смещение "относительно from"
        to.gameObject.SetActive(true);
        to.anchoredPosition = -offScreenPos;

        Vector2 fromStart = from.anchoredPosition;
        Vector2 fromEnd = fromStart + offScreenPos;
        Vector2 toStart = to.anchoredPosition;
        Vector2 toEnd = Vector2.zero;

        float elapsed = 0;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / slideDuration);

            from.anchoredPosition = Vector2.Lerp(fromStart, fromEnd, t);
            to.anchoredPosition = Vector2.Lerp(toStart, toEnd, t);

            yield return null;
        }

        from.gameObject.SetActive(false);
    }
}



