using UnityEngine;

public static class HelperFunctions
{
    public static void UpdateOVR()
    {
        OVRInput.Update();
    }

    public static Vector3 GetControllerVelocity(bool isLeft)
    {
        if (isLeft)
            return OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
        return OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
    }

    public static Vector2 TrackControllerInput()
    {
        return new Vector2(
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch),
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch)
        );
    }

    public static Vector2 TrackControllerIndex()
    {
        return new Vector2(
            OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch),
            OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch)
        );
    }

    public static void MyDebug(object msg)
    {
        Debug.Log("Mine: " + msg);
    }
}