using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableParticles : MonoBehaviour
{
    public Scalable scalable;
    public float scaleCoefficient = 1;
    ParticleSystem particleSystem;

    Coroutine? disableParticlesRoutine;
    Coroutine? addParticlesRoutine;
    // Start is called before the first frame update
    void Start()
    {
        addParticlesRoutine = null;
        disableParticlesRoutine = null;
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.enableEmission = false;
        float scale = scalable.stateOnStart == ScaleState.Large ? scalable.smallScale : scalable.largeScale;
        particleSystem.startColor = scalable.GetComponent<Renderer>().material.GetColor(scalable.stateOnStart == ScaleState.Large ? "_SmallScaleColour" : "_LargeScaleColour");
        particleSystem.transform.localScale = new Vector3(scale, scale, scale) * scaleCoefficient;
        scalable.OnTransitionStart += (object sender, ScaleState target) =>
        {
            if (scalable.stateOnStart == target)
            {
                if (addParticlesRoutine != null) StopCoroutine(addParticlesRoutine);
                disableParticlesRoutine = StartCoroutine(DisableParticles());
            }
            else
            {
                if (disableParticlesRoutine != null) StopCoroutine(disableParticlesRoutine);
                addParticlesRoutine = StartCoroutine(AddParticles());
            }
        };
    }

    IEnumerator DisableParticles()
    {
        yield return new WaitForSeconds(scalable.scaleDuration / 2000f);
        particleSystem.enableEmission = false;
        yield return null;
    }

    IEnumerator AddParticles()
    {
        yield return new WaitForSeconds(scalable.scaleDuration / 2000f);
        if (!particleSystem.isPlaying) particleSystem.Play();
        particleSystem.enableEmission = true;
        yield return null;
    }
}
