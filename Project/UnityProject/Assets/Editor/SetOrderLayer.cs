using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class SetOrderLayer : EditorWindow 
{
    [MenuItem("Window/SetOrderLayer")]//设置层的顺序
    public static void OrderLayer()
    {
        SetOrderLayer window = (SetOrderLayer)EditorWindow.GetWindow(typeof(SetOrderLayer));
        window.Show();
    }

    private void testc(int i, Rect r)
    {
        Repaint();
    }


    private static bool isEnabled;

    void OnEnable()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= testc;
        EditorApplication.hierarchyWindowItemOnGUI += testc;
    }

    void OnDisable()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= testc;
    }

    private int order = 0;
    public void OnGUI()
    {
        
        Transform tsf = Selection.activeTransform;
        if (!tsf)
        {
            GUILayout.Label("请选择一个游戏对象");
            return;
        }
        int layer = LayerMask.NameToLayer("UI2");
        int currentLayer;

        GUILayout.BeginVertical();
        
        ParticleSystem[] particles = FindAllInChildren<ParticleSystem>(tsf);
        if (particles.Length > 0)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("--------------------------粒子------------------------------");
            order = EditorGUILayout.IntField(order);
            if (GUILayout.Button("设置所有粒子"))
            {
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].renderer.sortingOrder = order;
                }
            }

            if (GUILayout.Button("All -", GUILayout.Width(60)))
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].renderer.sortingOrder--;
                }
            if (GUILayout.Button("All +", GUILayout.Width(60)))
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].renderer.sortingOrder++;
                }

            GUILayout.EndHorizontal();
            for (int i = 0; i < particles.Length; i++)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField("", particles[i], typeof(Object), true);
                particles[i].renderer.sortingOrder = EditorGUILayout.IntField(particles[i].renderer.sortingOrder, GUILayout.Width(60));

                currentLayer = particles[i].gameObject.layer;
                if (particles[i].gameObject.layer != layer)
                    GUI.color = Color.red;
                GUILayout.Label("Layer:" + LayerMask.LayerToName(currentLayer), GUILayout.Width(80));
                GUI.color = Color.white;

                if (GUILayout.Button("-", GUILayout.Width(20)))
                    particles[i].renderer.sortingOrder--;
                if (GUILayout.Button("+", GUILayout.Width(20)))
                    particles[i].renderer.sortingOrder++;

                EditorUtility.SetDirty(particles[i]);
                GUILayout.EndHorizontal();
            }
        }
        

        //
        SpriteRenderer[] rs = FindAllInChildren<SpriteRenderer>(tsf);
        if (rs.Length > 0)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("--------------------------SpriteRenderer------------------------------");
            if (GUILayout.Button("All -", GUILayout.Width(60)))
                for (int i = 0; i < rs.Length; i++)
                {
                    rs[i].sortingOrder--;
                }
            if (GUILayout.Button("All +", GUILayout.Width(60)))
                for (int i = 0; i < rs.Length; i++)
                {
                    rs[i].sortingOrder++;
                }
            GUILayout.EndHorizontal();

            for (int i = 0; i < rs.Length; i++)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField("", rs[i], typeof(Object), true);
                rs[i].sortingOrder = EditorGUILayout.IntField(rs[i].sortingOrder, GUILayout.Width(60));

                currentLayer = rs[i].gameObject.layer;
                if (rs[i].gameObject.layer != layer)
                    GUI.color = Color.red;
                GUILayout.Label("Layer:" + LayerMask.LayerToName(currentLayer), GUILayout.Width(80));
                GUI.color = Color.white;

                if (GUILayout.Button("-", GUILayout.Width(20)))
                    rs[i].sortingOrder--;
                if (GUILayout.Button("+", GUILayout.Width(20)))
                    rs[i].sortingOrder++;
                EditorUtility.SetDirty(rs[i]);
                GUILayout.EndHorizontal();
            }
        }
        
        MeshRenderer[] ms = FindAllInChildren<MeshRenderer>(tsf);
        if (ms.Length > 0)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("--------------------------MeshRenderer------------------------------");
            if (GUILayout.Button("All -", GUILayout.Width(60)))
                for (int i = 0; i < ms.Length; i++)
                {
                    ms[i].sortingOrder--;
                }
            if (GUILayout.Button("All +", GUILayout.Width(60)))
                for (int i = 0; i < ms.Length; i++)
                {
                    ms[i].sortingOrder++;
                }
            GUILayout.EndHorizontal();

            for (int i = 0; i < ms.Length; i++)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField("", ms[i], typeof(Object), true);
                ms[i].sortingOrder = EditorGUILayout.IntField(ms[i].sortingOrder, GUILayout.Width(60));

                currentLayer = ms[i].gameObject.layer;
                if (ms[i].gameObject.layer != layer)
                    GUI.color = Color.red;
                GUILayout.Label("Layer:" + LayerMask.LayerToName(currentLayer), GUILayout.Width(80));
                GUI.color = Color.white;

                if (GUILayout.Button("-", GUILayout.Width(20)))
                    ms[i].sortingOrder--;
                if (GUILayout.Button("+", GUILayout.Width(20)))
                    ms[i].sortingOrder++;
                EditorUtility.SetDirty(ms[i]);
                GUILayout.EndHorizontal();
            }
        }
        

        GUILayout.EndVertical();
    }

    private T[] FindAllInChildren<T>(Transform tsf) where T : Component
    {
        List<T> list = new List<T>();
        FindAllComponents<T>(tsf, list);
        return list.ToArray();
    }

    private void FindAllComponents<T>(Transform tsf, List<T> list) where T : Component
    {
        for (int i = 0; i < tsf.childCount; i++)
        {
            FindAllComponents<T>(tsf.GetChild(i), list);
        }
        T t = tsf.GetComponent<T>();
        if (t)
            list.Add(t);
    }
}

