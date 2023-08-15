using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fullScreenSupportLabel;
    [SerializeField] private TextMeshProUGUI orientationLabel;

    public Button fullScreenButton;
    public Button exitFullScreenButton;
    public Button lockButton_primary;

    public Button lockButton_secondary;

    public Button unlockButton;

    void Start()
    {
        MobileOrientationDetector.OnOrientationChange += (angle) =>
        {
            if(angle == -999)
            {
                fullScreenSupportLabel.text = "FullScreen is not support";
                if(fullScreenButton != null)
                {
                    fullScreenButton.gameObject.SetActive(false);
                }
                return;
            }
            Debug.Log($"Angle: {angle}");
            orientationLabel.text = $"Angle: {angle}";
        };

        fullScreenButton.onClick.AddListener(() =>
        {

            MobileOrientationDetector.FullScreen();
        });

        lockButton_primary.onClick.AddListener(() =>
        {
            MobileOrientationDetector.ScreenLock_LandScapePrimary();
        });

        lockButton_secondary.onClick.AddListener(() =>
        {
            MobileOrientationDetector.ScreenLock_LandScapeSecondary();
        });

        unlockButton.onClick.AddListener(() =>
        {
            MobileOrientationDetector.ScreenUnlock();
        });

        exitFullScreenButton.onClick.AddListener(() =>
        {
            MobileOrientationDetector.ExitFullScreen();
        });
        

        MobileOrientationDetector.Init();
    }
}
