using System.Collections;
using Games;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel, endStar;
    [SerializeField] private GameObject points;
    [SerializeField] private GameObject star1, star2, star3, star4, star5, star6;
    [SerializeField] private Image backdrop;
    [SerializeField] private float alpha;
    [SerializeField] private float moveTime;
    [SerializeField] private float waitTime;
    [SerializeField] private float onScreenTime;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    public void EndGameStart() => StartCoroutine(StartEnd());

    private IEnumerator StartEnd()
    {
        particle.Play();
        LeanTween.moveLocalY(endStar, 105 , moveTime).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(points, -1000, moveTime).setEase(LeanTweenType.easeInBack).setOnComplete(() => audioSource.PlayOneShot(audioClip));
        StartCoroutine(FadeIn());
        
        LeanTween.moveLocalY(endGamePanel, 0 , moveTime).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(waitTime);
        
        LeanTween.moveLocalY(star4, -100 , moveTime).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(waitTime);
        LeanTween.moveLocalY(star1, -100 , moveTime).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(waitTime);
        LeanTween.moveLocalY(star2, -10 , moveTime).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(waitTime);
        LeanTween.moveLocalY(star6, -5 , moveTime).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(waitTime);
        LeanTween.moveLocalY(star3, 110 , moveTime).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(waitTime);
        LeanTween.moveLocalY(star5, 120 , moveTime).setEase(LeanTweenType.easeOutBack);
        
        yield return new WaitForSeconds(onScreenTime);
        Indicator.Instance.HideSign();
        
        LeanTween.moveLocalY(star4, -1000 , moveTime).setEase(LeanTweenType.easeInBack);
        yield return new WaitForSeconds(waitTime);
        LeanTween.moveLocalY(star1, -1000 , moveTime).setEase(LeanTweenType.easeInBack);
        yield return new WaitForSeconds(waitTime);
        LeanTween.moveLocalY(star2, -1000 , moveTime).setEase(LeanTweenType.easeInBack);
        yield return new WaitForSeconds(waitTime);
        LeanTween.moveLocalY(star6, -1000 , moveTime).setEase(LeanTweenType.easeInBack);
        yield return new WaitForSeconds(waitTime);
        LeanTween.moveLocalY(star3, -1000 , moveTime).setEase(LeanTweenType.easeInBack);
        yield return new WaitForSeconds(waitTime);
        LeanTween.moveLocalY(star5, -1000 , moveTime).setEase(LeanTweenType.easeInBack);
        yield return new WaitForSeconds(waitTime);
        
        LeanTween.moveLocalY(endGamePanel, -1000 , moveTime).setEase(LeanTweenType.easeInBack);
        yield return new WaitForSeconds(waitTime);

        LeanTween.moveLocalY(endStar, -1000 , moveTime).setEase(LeanTweenType.easeInBack).setOnComplete(() => particle.Stop());
        StartCoroutine(Fadeout());
    }

    private IEnumerator FadeIn()
    {
        var timer = 0f;
        while (timer < moveTime)
        {
            timer += Time.deltaTime;
            var percent = Mathf.Clamp01(timer/waitTime);
            var color = new Color(0,0,0,percent * alpha);
            backdrop.color = color;
            yield return null;
        }
    }
    private IEnumerator Fadeout()
    {
        var timer = moveTime;
        while (timer > 0 )
        {
            timer -= Time.deltaTime;
            var percent = Mathf.Clamp01(timer/waitTime);
            var color = new Color(0,0,0,percent * alpha);
            backdrop.color = color;
            yield return null;
        }
    }

}
