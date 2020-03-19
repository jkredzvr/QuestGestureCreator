using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using OculusSampleFramework;

public class GestureCopyMachine : MonoBehaviour
{
    public BoxCollider CopyVolume;
    public List<OVRHand> HandsInBox;
    public ButtonListener buttonListener;
    public ButtonController buttonController;
    public bool IsCopying = false;

    private void Awake()
    {
        Assert.IsNotNull(CopyVolume);
        Assert.IsNotNull(buttonController);
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonController.ContactZoneEvent += ButtonController_ContactZoneEvent;
    }

    
    // Update is called once per frame
    void Update()
    {
        //check if only one hand inside && not copying
            //button enabled

        //check if button pressed
            //IsCopying
            //count down 
            //create gesture asset
            //!Copying
    }

    private void OnTriggerEnter(Collider other)
    {
       //Add hand to hands volume
    }

    private void OnTriggerExit(Collider other)
    {
        //remove  hands from hands volume
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void ButtonController_ContactZoneEvent(ColliderZoneArgs obj)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Cancel if no hands
    /// </summary>
    void CancelCopy()
    {
        //Cancel Copy Coroutine
        //!Copying
    }
}
