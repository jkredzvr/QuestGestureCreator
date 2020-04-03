using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OculusSampleFramework;

public class GestureAssetVisualizer : MonoBehaviour
{
    private GestureScriptableAsset gestureAsset;
    [SerializeField] private TMP_Text _textMesh;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _proximitySFX;

    private bool textIsVisible = false;


    // Start is called before the first frame update
    void Start()
    {
        _textMesh.gameObject.SetActive(false);
    }

    public void OnInteractableStateChanged(InteractableStateArgs stateArgEvt)
    {
        InteractableState oldState = stateArgEvt.OldInteractableState;
        InteractableState newState = stateArgEvt.NewInteractableState;
        
        switch(oldState)
        {
            case InteractableState.Default when newState == InteractableState.ProximityState:
                ShowText(true);
                PlayHoverSFX();
                break;
            case InteractableState.ProximityState when newState == InteractableState.Default:
                ShowText(false);
                PlayHoverSFX();
                break;
            default:
                break;
        }
    }

    private void ToggleText()
    {
        textIsVisible = !textIsVisible;
        _textMesh.gameObject.SetActive(textIsVisible);
    }

    private void ShowText(bool value)
    {
        _textMesh.gameObject.SetActive(value);
    }

    private void PlayHoverSFX()
    {
        _audioSource.PlayOneShot(_proximitySFX);
    }

    public void SetGestureAsset(GestureScriptableAsset gestureAsset)
    {
        this.gestureAsset = gestureAsset;
        _textMesh.text = gestureAsset.Name;
    }

}
