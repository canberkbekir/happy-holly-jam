using System;
using System.Collections;
using Globals;
using UnityEngine;
using UnityEngine.VFX;

public class SantaController : MonoBehaviour
{
    public event Action OnGameWin;
    
    [SerializeField] private VisualEffect visualEffect; 
    [SerializeField] private Animator santaAnimator;

    private string bloatingAnimationName = "Bloating";
    
    [Range(0f,1f)]
    [SerializeField] private float animationFrame = 0f;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject.transform.parent.gameObject);

        GameManager.Instance.SatisfactionController.IncreaseSatisfaction();
        
        if (animationFrame < 1f)
            StartCoroutine(PlayEatingAnimation());
    }

    private IEnumerator PlayEatingAnimation()
    {
        float currentTime = 0f;
        float animationTime = 1f;

        float animationIncrement = 0.1f;
        float originalAnimationFrame = animationFrame;

        while (currentTime <= animationTime)
        {
            currentTime += Time.deltaTime;
            animationFrame = originalAnimationFrame + (animationIncrement * currentTime / animationTime);
            santaAnimator.Play(bloatingAnimationName, 0, animationFrame);
            yield return null;
        }
        
        animationFrame = Mathf.Clamp01(originalAnimationFrame + animationIncrement);
        
        if (animationFrame >= 1f)
            OnGameWin!.Invoke();
    }
}
