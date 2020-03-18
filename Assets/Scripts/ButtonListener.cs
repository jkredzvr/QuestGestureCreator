using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using System.ComponentModel.Design;

public class ButtonListener : MonoBehaviour
{
    public UnityEvent proximityEvent;
    public UnityEvent contactEvent;
    public UnityEvent actionEvent;
    public UnityEvent defaultEvent;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
    }

    private void InitiateEvent(InteractableStateArgs state)
    {
        switch (state.NewInteractableState)
        {
            case InteractableState.ProximityState:
                proximityEvent.Invoke();
                break;
            case InteractableState.ContactState:
                contactEvent.Invoke();
                break;
            case InteractableState.ActionState:
                actionEvent.Invoke();
                break;
            default:
                defaultEvent.Invoke();
                break;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
