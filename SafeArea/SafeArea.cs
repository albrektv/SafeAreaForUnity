using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    RectTransform rectTransform;
    Rect lastSafeArea = Rect.zero;
    Vector2Int lastScreenSize = Vector2Int.zero;
    ScreenOrientation lastOrientation = ScreenOrientation.AutoRotation;

    // قائمة العناصر المستثناة
    public List<RectTransform> excludeElements = new List<RectTransform>();

    // متغيرات لتحديد الجوانب المفعلة
    public bool applyLeft = true;
    public bool applyRight = true;
    public bool applyTop = true;
    public bool applyBottom = true;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Refresh();
        // ابدأ التحقق الدوري
        InvokeRepeating(nameof(CheckForChanges), 1.0f, 1.0f); // التحقق كل ثانية
    }

    void CheckForChanges()
    {
        if (lastSafeArea != Screen.safeArea
            || lastScreenSize.x != Screen.width
            || lastScreenSize.y != Screen.height
            || lastOrientation != Screen.orientation)
        {
            Refresh();
        }
    }

    void Refresh()
    {
        lastSafeArea = Screen.safeArea;
        lastScreenSize.x = Screen.width;
        lastScreenSize.y = Screen.height;
        lastOrientation = Screen.orientation;

        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        if (rectTransform == null) return;

        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;

        if (!applyLeft) anchorMin.x = 0;
        if (!applyBottom) anchorMin.y = 0;
        if (!applyRight) anchorMax.x = Screen.width;
        if (!applyTop) anchorMax.y = Screen.height;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;

        // تطبيق المنطقة الآمنة للعناصر المستثناة
        foreach (var excludedRect in excludeElements)
        {
            if (excludedRect == null) continue;

            excludedRect.anchorMin = new Vector2(0, 0);
            excludedRect.anchorMax = new Vector2(1, 1);
        }
    }
}
