using AOT;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class MobileOrientationDetector : MonoBehaviour
{
    public delegate void dlgOrientationChange(int angle);
    public static event dlgOrientationChange OnOrientationChange;
    private static bool isFullScreenSupport;
    
    [DllImport("__Internal")]
    private static extern int JS_OrientationDetectorLib_Init(Action<int> eventHandler);

    [DllImport("__Internal")]
    private static extern int JS_OrientationDetectorLib_GetOrientation();

    [DllImport("__Internal")]
    private static extern void JS_OrientationDetectorLib_FullScreen();
    
    [DllImport("__Internal")]
    private static extern void JS_OrientationDetectorLib_ExitFullScreen();

    [DllImport("__Internal")]
    private static extern void JS_OrientationDetectorLib_Lock_LandScapePrimary();

    [DllImport("__Internal")]
    private static extern void JS_OrientationDetectorLib_Lock_LandScapeSecondary();

    [DllImport("__Internal")]
    private static extern void JS_OrientationDetectorLib_Unlock();


    [MonoPInvokeCallback(typeof(Action<int>))]
    private static void onOrientationChange (int angle)
    {
        if(angle == -999)
        {
            isFullScreenSupport = false;
        }
        OnOrientationChange.Invoke(angle);
    }

    public static void Init()
    {
        isFullScreenSupport = true;

        var res = JS_OrientationDetectorLib_Init(onOrientationChange);
        if (res == -1)
        {
            throw new Exception("This device does not support Screen Orientation API");
        }
    }

    public static int GetOrientation()
    {
        var angle = JS_OrientationDetectorLib_GetOrientation();
        return angle;
    }

    //this cannot be called with out user clicked event. 
    public static void FullScreen()
    {
        if (!isFullScreenSupport) return;
        JS_OrientationDetectorLib_FullScreen();
    }

    public static void ExitFullScreen()
    {
        if (!isFullScreenSupport) return;
        JS_OrientationDetectorLib_ExitFullScreen();
    }

    //this cannot be called with out user clicked event. (because it calls FullScreen())
    public static void ScreenLock_LandScapePrimary()
    {
        if (!isFullScreenSupport) return;
        JS_OrientationDetectorLib_Lock_LandScapePrimary();
    }

    //this cannot be called with out user clicked event. (because it calls FullScreen())
    public static void ScreenLock_LandScapeSecondary()
    {
        if (!isFullScreenSupport) return;
        JS_OrientationDetectorLib_Lock_LandScapeSecondary();
    }


    public static void ScreenUnlock()
    {
        JS_OrientationDetectorLib_Unlock();
    }
}
