using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Games
{
    public abstract class GameBase : MonoBehaviour
    {
        [SerializeField] private float time;
        protected float TimeMultiplier = 1f;
        [SerializeField] private BarFill barFill;
        protected Action OnEnterGame;
        protected Action OnExitGame;
        protected bool IsActive;
        [SerializeField] private AudioSource mainAudioSource;
        [SerializeField] private AudioClip ring;
        [SerializeField] private float pitchMiddle = 1.4f; 
        [SerializeField] private float pitchEnd = 1.7f;
        
        public void SetTime(float newTime) => TimeMultiplier = newTime;
        public virtual void StartGame()
        {
            barFill.BarStatus(true);
            StartCoroutine(StartTimer());
            OnEnterGame?.Invoke();
        }

        private IEnumerator StartTimer()
        {
            IsActive = true;
            barFill.BarStatus(true);
            Ambiance.Instance.SetVolume(0.05f);
            
            var timer = 0f;
            mainAudioSource.Play();
            while (timer < time * TimeMultiplier)
            {
                timer += Time.deltaTime;
                barFill.Fill(timer / time);
                mainAudioSource.pitch = (timer / time) switch
                {
                    > 0.7f when mainAudioSource.pitch < pitchMiddle => pitchMiddle,
                    > 0.9f when mainAudioSource.pitch < pitchEnd => pitchEnd,
                    _ => mainAudioSource.pitch
                };
                yield return null;
            }

            Ambiance.Instance.SetVolume(0.1f);
            mainAudioSource.pitch = 1f;
            mainAudioSource.Stop();
            mainAudioSource.PlayOneShot(ring);

            IsActive = false;
            OnExitGame?.Invoke();
            GameManager.Instance.ExitGame();
            MouseController.Instance.OnGameExit();
            barFill.BarStatus(false);
            TimeMultiplier = 1;
        }
    }
}
