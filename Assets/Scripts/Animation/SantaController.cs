using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class SantaController : MonoBehaviour
{
    [SerializeField] private VisualEffect visualEffect; 
    [SerializeField] private Animator santaAnimator;

    private string animationName = "TESTNAME";
    
    [Range(0f,1f)]
    [SerializeField] private float animationFrame = 0f;
    
    private void Update()
    {
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        visualEffect.Play();
        
        if (animationFrame < 1f)
            StartCoroutine(PlayEatingAnimation());
    }

    private IEnumerator PlayEatingAnimation()
    {
        float currentTime = 0f;
        float animationTime = 1f;

        float animationIncrement = 0.1f;

        while (currentTime <= animationTime)
        {
            currentTime += Time.deltaTime;
            animationFrame = animationIncrement * currentTime / animationTime;
            santaAnimator.Play(animationName, 0, animationFrame);
            yield return null;
        }
        
        animationFrame = Mathf.Clamp01(animationFrame + animationIncrement);
    }
}
