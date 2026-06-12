using UnityEngine;
using PrimeTween;
using System.Collections;

namespace KH
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource), typeof(CanvasGroup))]
    public class KHUI : MonoBehaviour
    {

        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region Fields
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #endregion

        private static WaitForSeconds _waitForSeconds1 = new(1f);
        private Vector3 _startScale;
        private Vector3 _startPos;

        // Components
        private CanvasGroup _canvasGroup;
        private RectTransform _canvasRect;
        private AudioSource _audioSource;

        //
        private Tween _activeTween;

        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region INSPECTOR
        // █████████████████████████████████████████████████████████████████████████████████████████████████



        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region UNITY EVENTS
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        private void Reset()
        {
            _audioSource = GetComponent<AudioSource>();

            _audioSource.playOnAwake = false;
        }

        private void Awake()
        {
            _startScale = transform.localScale;
            _startPos = transform.localPosition;

            if (TryGetComponent(out CanvasGroup canvasG))
            {
                _canvasGroup = canvasG;
                _canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            }

            _audioSource = GetComponent<AudioSource>();
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region PRIVATE
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        private void RestoreDefaults(bool wait1Sec = false)
        {
            transform.localScale = _startScale;
            transform.localPosition = _startPos;

            _canvasGroup.alpha = 1;

            if (wait1Sec)
            {
                StartCoroutine(DelayedInteractable());
            }
            else
            {
                _canvasGroup.interactable = true;
            }
        }

        private IEnumerator DelayedInteractable()
        {
            yield return _waitForSeconds1;
            _canvasGroup.interactable = true;
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region PUBLIC
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        public void LoadStartScene()
        {
            UIManager.Ins.LoadScene(GameScenes.START);
        }

        public void LoadPlayerScreenScene()
        {
            // When pause level timescale == 0, so we need to make 1.
            Time.timeScale = 1;

            UIManager.Ins.LoadScene(GameScenes.PLAYER_SCREEN);
        }

        public void LoadPlayLevel()
        {
            UIManager.Ins.LoadScene(PlayerPrefs.GetInt(GamePP.CURRENT_LEVEL, 1).ToString());
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void TogglePauseLevel()
        {
            LevelManager.Ins.TogglePauseLevel();
        }


        // TRANSITIONS & ANIMATIONS


        #region  POP

        public void KH_Show_Pop()
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
                _canvasGroup.interactable = false;

                _audioSource.PlayOneShot(GameManager.Ins.Data.PopShow);

                transform.localScale = Vector3.zero;

                if (_activeTween.isAlive)
                {
                    _activeTween.Stop();
                }
                _activeTween = Tween.Scale(
                    transform,
                    endValue: _startScale,
                    duration: GameConst.UI_SHOW_SPEED,
                    ease: Ease.OutBack
                ).OnComplete(() =>
                {
                    RestoreDefaults(true);
                });
            }
        }

        public void KH_Hide_Pop()
        {
            if (gameObject.activeInHierarchy)
            {
                _audioSource.PlayOneShot(GameManager.Ins.Data.PopHide);

                _canvasGroup.interactable = false;

                if (_activeTween.isAlive)
                {
                    _activeTween.Stop();
                }
                _activeTween = Tween.Scale(
                    transform,
                    endValue: Vector3.zero,
                    duration: GameConst.UI_HIDE_SPEED,
                    ease: Ease.InBack
                ).OnComplete(() =>
                {
                    RestoreDefaults();
                    gameObject.SetActive(false);
                });
            }
        }

        public void KH_Toggle_Pop()
        {
            KH_Hide_Pop();
            KH_Show_Pop();
        }

        #endregion

        #region LEFT

        public void KH_Show_FromLeft()
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);

                transform.localPosition = _startPos + Vector3.left * _canvasRect.rect.width;

                if (_activeTween.isAlive)
                {
                    _activeTween.Stop();
                }
                _activeTween = Tween.LocalPosition(
                    transform,
                    endValue: _startPos,
                    duration: GameConst.UI_SHOW_SPEED,
                    ease: Ease.OutCubic
                ).OnComplete(() =>
                {
                    RestoreDefaults(true);
                });
            }
        }

        public void KH_Hide_FromLeft()
        {
            if (gameObject.activeInHierarchy)
            {
                _canvasGroup.interactable = false;

                if (_activeTween.isAlive)
                {
                    _activeTween.Stop();
                }
                _activeTween = Tween.LocalPosition(
                    transform,
                    endValue: _startPos + Vector3.left * _canvasRect.rect.width,
                    duration: GameConst.UI_HIDE_SPEED,
                    ease: Ease.InCubic
                ).OnComplete(() =>
                {
                    RestoreDefaults();
                    gameObject.SetActive(false);
                });
            }
        }

        public void KH_Toggle_FromLeft()
        {
            KH_Hide_FromLeft();
            KH_Show_FromLeft();
        }

        #endregion

        #region RIGHT

        public void KH_Show_FromRight()
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);

                _canvasGroup.interactable = true;
                gameObject.SetActive(true);

                transform.localPosition = _startPos + Vector3.right * _canvasRect.rect.width;

                if (_activeTween.isAlive)
                {
                    _activeTween.Stop();
                }
                _activeTween = Tween.LocalPosition(
                    transform,
                    endValue: _startPos,
                    duration: GameConst.UI_SHOW_SPEED,
                    ease: Ease.OutCubic
                ).OnComplete(() =>
                {
                    RestoreDefaults(true);
                });
            }
        }

        public void KH_Hide_FromRight()
        {
            if (gameObject.activeInHierarchy)
            {
                _canvasGroup.interactable = false;

                if (_activeTween.isAlive)
                {
                    _activeTween.Stop();
                }
                _activeTween = Tween.LocalPosition(
                    transform,
                    endValue: _startPos + Vector3.right * _canvasRect.rect.width,
                    duration: GameConst.UI_HIDE_SPEED,
                    ease: Ease.InCubic
                ).OnComplete(() =>
                {
                    RestoreDefaults();
                    gameObject.SetActive(false);
                });
            }
        }

        public void KH_Toggle_FromRight()
        {
            KH_Hide_FromRight();
            KH_Show_FromRight();
        }

        #endregion
        #region FADE

        public void KH_Show_Fade()
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);

                _canvasGroup.alpha = 0;

                if (_activeTween.isAlive)
                {
                    _activeTween.Stop();
                }
                _activeTween = Tween.Alpha(
                    _canvasGroup,
                    endValue: 1f,
                    duration: GameConst.UI_SHOW_SPEED
                ).OnComplete(() =>
                {
                    RestoreDefaults(true);
                });
            }
        }

        public void KH_Hide_Fade()
        {
            if (gameObject.activeInHierarchy)
            {
                _canvasGroup.interactable = false;

                if (_activeTween.isAlive)
                {
                    _activeTween.Stop();
                }
                _activeTween = Tween.Alpha(
                    _canvasGroup,
                    endValue: 0f,
                    duration: GameConst.UI_HIDE_SPEED
                ).OnComplete(() =>
                {
                    RestoreDefaults();
                    gameObject.SetActive(false);
                });
            }
        }

        public void KH_Toggle_Fade()
        {
            KH_Hide_Fade();
            KH_Show_Fade();
        }

        #endregion

        #endregion
    }
}