using UnityEngine;

public static class HelperFunctions
{
    public static void UpdateOVR()
    {
        OVRInput.Update();
    }

    public static Vector2 TrackControllerInput()
    {
        return new Vector2(
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch),
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch)
        );
    }

    public static void MyDebug(object msg)
    {
        Debug.Log("Mine: " + msg);
    }
}