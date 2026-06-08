using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class UIManager : MonoBehaviour
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Static
    public static UIManager Ins { get; private set; }

    //
    private bool _isTransitioning;
    private readonly float _fadeDuration = 0.3f;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private CanvasGroup _sceneTransitionsBg;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void Awake()
    {
        if (Ins == null)
            Ins = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private IEnumerator SceneTransition(string sceneName)
    {
        _isTransitioning = true;
        _sceneTransitionsBg.blocksRaycasts = true;

        // Fade to black
        yield return Fade(0f, 1f);

        // Load scene
        SceneManager.LoadScene(sceneName);

        // Wait one frame so the new scene is loaded
        yield return null;

        // Fade from black
        yield return Fade(1f, 0f);

        _isTransitioning = false;
        _sceneTransitionsBg.blocksRaycasts = false;
    }

    private IEnumerator Fade(float from, float to)
    {
        float timer = 0f;

        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;

            _sceneTransitionsBg.alpha = Mathf.Lerp(from, to, timer / _fadeDuration);

            yield return null;
        }

        _sceneTransitionsBg.alpha = to;
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void LoadScene(string name)
    {
        if (_isTransitioning)
            return;

        StartCoroutine(SceneTransition(name));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
