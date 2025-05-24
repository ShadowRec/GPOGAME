using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public RectTransform mainMenuPanel;
    public RectTransform runSetupPanel;
    public float transitionDuration = 0.5f;

    private bool isTransitioning = false;

    public void OnNewRunClicked()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionTo(runSetupPanel));
        }
    }

    public void OnBackClicked()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionTo(mainMenuPanel));
        }
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



    private IEnumerator TransitionTo(RectTransform target)
    {
        isTransitioning = true;

        Vector2 start = mainMenuPanel.anchoredPosition;
        Vector2 end = target.anchoredPosition;

        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / transitionDuration);

            // Сдвигаем обе панели одновременно для эффекта "сдвига"
            mainMenuPanel.anchoredPosition = Vector2.Lerp(start, new Vector2(-1920, 0), t);
            runSetupPanel.anchoredPosition = Vector2.Lerp(new Vector2(1920, 0), Vector2.zero, t);

            yield return null;
        }

        // Гарантируем финальное положение
        if (target == runSetupPanel)
        {
            mainMenuPanel.anchoredPosition = new Vector2(-1920, 0);
            runSetupPanel.anchoredPosition = Vector2.zero;
        }
        else
        {
            mainMenuPanel.anchoredPosition = Vector2.zero;
            runSetupPanel.anchoredPosition = new Vector2(1920, 0);
        }

        isTransitioning = false;
    }


}
