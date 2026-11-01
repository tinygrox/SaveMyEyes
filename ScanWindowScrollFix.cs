using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SaveMyEyes;

public static class ScanWindowScrollFix
{
    public static ScrollRect Prepare(TextMeshProUGUI text)
    {
        RectTransform panel = text.rectTransform.parent as RectTransform;

        RectTransform root = CreateRect("Scroll View", panel);
        root.gameObject.SetActive(false);

        RectTransform viewport = CreateRect("Viewport", root);
        viewport.anchorMin = Vector2.zero;
        viewport.anchorMax = Vector2.one;
        viewport.offsetMin = Vector2.zero;
        viewport.offsetMax = Vector2.zero;
        viewport.gameObject.AddComponent<RectMask2D>();

        Image blocker = viewport.gameObject.AddComponent<Image>();
        blocker.color = new Color(0f, 0f, 0f, 0f);
        blocker.raycastTarget = true;

        UIDragHandlerForwarder dragForwarder = viewport.gameObject.AddComponent<UIDragHandlerForwarder>();
        dragForwarder.handler = panel.GetComponentInParent<UIDragHandler>(true);

        ScrollRect scrollRect = root.gameObject.AddComponent<ScrollRect>();
        scrollRect.viewport = viewport;
        scrollRect.horizontal = false;
        scrollRect.vertical = true;
        scrollRect.movementType = ScrollRect.MovementType.Elastic;
        scrollRect.elasticity = 0.1f;
        scrollRect.scrollSensitivity = 40f;
        scrollRect.inertia = true;

        return scrollRect;
    }

    public static void Wrap(ScrollRect scrollRect, TextMeshProUGUI text)
    {
        if (scrollRect.content == null)
        {
            Bind(text, scrollRect);
        }

        scrollRect.gameObject.SetActive(true);
        scrollRect.verticalNormalizedPosition = 1f;
    }

    private static void Bind(TextMeshProUGUI text, ScrollRect scrollRect)
    {
        RectTransform content = text.rectTransform;
        RectTransform root = scrollRect.transform as RectTransform;
        RectTransform viewport = scrollRect.viewport;

        root.anchorMin = content.anchorMin;
        root.anchorMax = content.anchorMax;
        root.pivot = content.pivot;
        root.anchoredPosition = content.anchoredPosition;
        root.sizeDelta = content.sizeDelta;
        root.SetSiblingIndex(content.GetSiblingIndex());

        content.SetParent(viewport, false);
        content.anchorMin = new Vector2(0f, 1f);
        content.anchorMax = new Vector2(1f, 1f);
        content.pivot = new Vector2(0.5f, 1f);
        content.anchoredPosition = Vector2.zero;
        content.sizeDelta = Vector2.zero;

        LayoutElement layoutElement = content.gameObject.AddComponent<LayoutElement>();
        layoutElement.minHeight = viewport.rect.height;
        content.gameObject.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        text.enableAutoSizing = false;
        text.fontSize = text.fontSizeMax;
        text.raycastTarget = false;

        scrollRect.content = content;
    }

    private static RectTransform CreateRect(string name, RectTransform parent)
    {
        GameObject target = new GameObject(name, typeof(RectTransform));
        RectTransform rect = (RectTransform)target.transform;
        rect.SetParent(parent, false);
        return rect;
    }
}
