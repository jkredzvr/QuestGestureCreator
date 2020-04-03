using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using OculusSampleFramework;
using System.Threading.Tasks;

public class GestureCopyMachine : MonoBehaviour
{
    public BoxCollider CopyVolume;
    public List<OVRHand> HandsInBox;
    public ButtonListener buttonListener;
    public ButtonController buttonController;
    public GestureRecognizer gestureRecognizer;
    public List<GestureScriptableAsset> CreatedGestures = new List<GestureScriptableAsset>();
    public bool IsCopying = false;

    private void Awake()
    {
        Assert.IsNotNull(CopyVolume);
        Assert.IsNotNull(buttonController);
        Assert.IsNotNull(gestureRecognizer);
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonController.ContactZoneEvent += ButtonController_ContactZoneEvent;
    }

    private void OnDestroy()
    {
        buttonController.ContactZoneEvent -= ButtonController_ContactZoneEvent;
    }


    // Update is called once per frame
    void Update()
    {
        //check if only one hand inside && not copying 
        bool onlyOneHand = (HandsInBox.Count == 1);
        //buttonListener.enabled = onlyOneHand;


        //if (IsCopying && !onlyOneHand)
        //{
        //    CancelCopy();
        //}    
    }

    private void OnTriggerEnter(Collider other)
    {
        //Add hand to hands volume
        OVRHand hand = other.gameObject.GetComponent<OVRHand>();
        if (hand != null)
        {
            HandsInBox.Add(hand);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //remove  hands from hands volume
        OVRHand hand = other.gameObject.GetComponent<OVRHand>();
        if (hand != null)
        {
            HandsInBox.Remove(hand);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void ButtonController_ContactZoneEvent(ColliderZoneArgs obj)
    {
        if(IsCopying)
        {
            return;
        }
        else
        {
            IsCopying = true;
            OVRSkeleton handSkeleton = HandsManager.Instance.RightHandSkeleton;
            AsyncMakeCopy(handSkeleton);
        }    
    }

    private async void AsyncMakeCopy(OVRSkeleton handSkeleton)
    {
        await GestureRecognizer.SaveHandGesture(handSkeleton, OnGestureCreated);
    }

    private void OnGestureCreated(GestureScriptableAsset gestureScriptableAsset)
    {
        IsCopying = false;
        CreatedGestures.Add(gestureScriptableAsset);
    }

    /// <summary>
    /// Cancel if no hands
    /// </summary>
    void CancelCopy()
    {
        //Cancel Copy Coroutine
        IsCopying = false;
    }
}
