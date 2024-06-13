using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SafeArea))]
public class SafeAreaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SafeArea safeArea = (SafeArea)target;

        GUIStyle enabledStyle = new GUIStyle(GUI.skin.button);
        enabledStyle.normal.background = MakeTex(600, 1, new Color(0.05f, 0.05f, 0.05f)); // 272727
        GUIStyle disabledStyle = new GUIStyle(GUI.skin.button);
        disabledStyle.normal.background = MakeTex(600, 1, new Color(0.18f, 0.18f, 0.18f)); // 2e2e2e

        // تكبير الأزرار بنسبة 30%
        float buttonWidth = 100 * 1.3f;
        float buttonHeight = 25 * 1.3f;
        float buttonSpacing = 10; // مسافة بين الأزرار

        // إعداد ستايل الكونتينر
        GUIStyle containerStyle = new GUIStyle(GUI.skin.box);
        containerStyle.normal.background = MakeTex(600, 1, new Color(0.188f, 0.188f, 0.188f)); // لون الخلفية للكونتينر #303030
        containerStyle.padding = new RectOffset(10, 10, 10, 10); // Padding 5%

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Safe Area (Script)", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // حساب حجم وموقع الكونتينر
        Rect containerRect = EditorGUILayout.BeginVertical();
        GUILayout.Space(5); // لتوفير بعض المساحة العلوية
        GUILayout.BeginVertical(containerStyle);
        GUILayout.Label("Safe Area", EditorStyles.boldLabel);
        GUILayout.Space(buttonSpacing);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        safeArea.applyTop = GUILayout.Toggle(safeArea.applyTop, "Top", safeArea.applyTop ? enabledStyle : disabledStyle, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(buttonSpacing);

        GUILayout.BeginHorizontal();
        safeArea.applyLeft = GUILayout.Toggle(safeArea.applyLeft, "Left", safeArea.applyLeft ? enabledStyle : disabledStyle, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight));
        GUILayout.FlexibleSpace();
        safeArea.applyRight = GUILayout.Toggle(safeArea.applyRight, "Right", safeArea.applyRight ? enabledStyle : disabledStyle, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight));
        GUILayout.EndHorizontal();

        GUILayout.Space(buttonSpacing);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        safeArea.applyBottom = GUILayout.Toggle(safeArea.applyBottom, "Bottom", safeArea.applyBottom ? enabledStyle : disabledStyle, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(buttonSpacing);

        GUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        // رسم الحواف المنحنية للكونتينر
        Vector4 cornerRadii = new Vector4(10, 10, 10, 10); // نصف قطر الانحناء 10%
        DrawRoundedRect(containerRect, cornerRadii, new Color(0.188f, 0.188f, 0.188f));

        EditorGUILayout.Space();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(safeArea);
        }
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++)
        {
            pix[i] = col;
        }

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }

    private void DrawRoundedRect(Rect position, Vector4 cornerRadii, Color color)
    {

    }
}
