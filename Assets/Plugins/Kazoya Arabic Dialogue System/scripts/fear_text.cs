using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fear_text : MonoBehaviour
{
    public float time = 6;
    public float origx = 0.01f;
    public float origy = 10f;

    private TMP_Text text;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        text.ForceMeshUpdate();
        var textinfo = text.textInfo;
        for (int i = 0; i < textinfo.characterCount; i++)
        {
            var charinfo = textinfo.characterInfo[i];

            if (!charinfo.isVisible)
            {
                continue;
            }
            var verts = textinfo.meshInfo[charinfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; j++)
            {
                var orig = verts[charinfo.vertexIndex + j];
                verts[charinfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * time + orig.x * origx) * origy, 0);

            }
        }
        for (int i = 0; i < textinfo.meshInfo.Length; i++)
        {
            var meshinfo = textinfo.meshInfo[i];
            meshinfo.mesh.vertices = meshinfo.vertices;
            text.UpdateGeometry(meshinfo.mesh, i);
        }
    }
}
