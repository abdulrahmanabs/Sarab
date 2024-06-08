using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cool_text : MonoBehaviour
{
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
                verts[charinfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 6f + orig.x * 0.01f) * 10, 0);

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
