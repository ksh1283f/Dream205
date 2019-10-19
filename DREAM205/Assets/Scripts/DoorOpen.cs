using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : InteractableObj
{
    public Animation DoorAni;
    public Animation LightOff;
    public AudioSource DoorSound;
    [SerializeField] Material GlowMaterial;
    [SerializeField] AudioSource phoneRing;
    [SerializeField] float fadeDuration;
    [SerializeField] bool isNeedInactiveCol;
    [SerializeField] Renderer propsRenderer;
    [SerializeField] SoundFadeEffect ambienceEffect;
    private Collider doorCol;

    // Start is called before the first frame update
    void Start()
    {
        if (isNeedInactiveCol)
        {
            doorCol = GetComponent<Collider>();
            doorCol.enabled = false;
        }

        OnExecuteInteract += ActivateGlowMaterial;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("handle"))
        {
            isInteractionEnd = true;
            DoorAni.Play();
            DoorSound.Play();
        }
    }

    void Flicker()
    {
        LightOff.Play();
    }

    void OnDoorAniEnd()
    {
        FadeAmbience();
        StartCoroutine(FadeSound());
        //SceneManager.LoadScene("level2Room"); // 로딩은 코루틴 종료 후
    }

    IEnumerator FadeSound()
    {
        if (phoneRing == null)
        {
            Debug.LogError("ringSound is null");
            yield break;
        }

        float volume = phoneRing.volume;
        float startTime = 0;
        volume = Mathf.Lerp(1f, 0, startTime);
        while (volume > 0f)
        {
            startTime += Time.deltaTime / fadeDuration;
            volume = Mathf.Lerp(1f, 0, startTime);
            phoneRing.volume = volume;

            yield return null;
        }

        SceneLoadingManager.Instance.SceneType = E_SceneType.level2Room;
    }

    void FadeAmbience()
    {
        if (ambienceEffect == null)
            return;

        ambienceEffect.SetEffectType(E_EffectType.DecreaseFromOneToZero);
        ambienceEffect.SetFadeDuration(fadeDuration);
        StartCoroutine(ambienceEffect.FadeEffect());
    }

    public void ActivateGlowMaterial()
    {
        if (GlowMaterial == null)
        {
            Debug.LogError(string.Concat(gameObject.name, ": glowMaterial is null"));
            return;
        }

        if (doorCol == null)
        {
            Debug.LogError(string.Concat(gameObject.name, ": glowMaterial is null"));
            return;
        }

        if (propsRenderer == null)
        {
            Debug.LogError("propsRenderer is null");
            return;
        }

        propsRenderer.material = GlowMaterial;
        doorCol.enabled = true;
    }
}
