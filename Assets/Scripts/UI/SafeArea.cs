using UnityEngine;
using Device = UnityEngine.Device;

[ExecuteInEditMode, RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    private RectTransform rectTransform;

    private ScreenOrientation orientation;

    private DrivenRectTransformTracker tracker = new();

#if UNITY_EDITOR
    private string deviceModel;
#endif

    private bool Dirty
    {
        get
        {
#if UNITY_EDITOR
            if (deviceModel != Device.SystemInfo.deviceModel)
                return true;
#endif

            return Device.Screen.orientation != orientation;
        }
    }

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        orientation = Device.Screen.orientation;
#if UNITY_EDITOR
        deviceModel = Device.SystemInfo.deviceModel;
#endif

        tracker.Clear();
        tracker.Add(this, rectTransform, DrivenTransformProperties.All);
    }

    protected virtual void Update()
    {
        if (Dirty)
        {
            orientation = Device.Screen.orientation;
#if UNITY_EDITOR
            deviceModel = Device.SystemInfo.deviceModel;
#endif

            SetProperties();
        }
    }

    protected virtual void OnEnable()
    {
        tracker.Clear();
        tracker.Add(this, rectTransform, DrivenTransformProperties.All);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += SetProperties;
#else
        SetProperties();
#endif
    }

    protected virtual void OnDisable()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall -= SetProperties;
#endif

        tracker.Clear();
    }

    private void SetProperties()
    {
        rectTransform.pivot = Vector2.zero;
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        Vector2 screenSize = new(Device.Screen.width, Device.Screen.height);
        rectTransform.anchorMin = Device.Screen.safeArea.min / screenSize;
        rectTransform.anchorMax = Device.Screen.safeArea.max / screenSize;
    }
}
