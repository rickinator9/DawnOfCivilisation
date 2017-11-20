using UnityEditor;
using UnityEngine;

public class TextureArrayWizard : ScriptableWizard
{
    public Texture2D[] Textures;

    [MenuItem("Assets/Create/Texture Array")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<TextureArrayWizard>("Create Texture Array", "Create");
    }

    void OnWizardCreate()
    {
        if (Textures.Length == 0) return;

        var path = EditorUtility.SaveFilePanelInProject("Save Texture Array", "Texture Array", "asset", "Save Texture Array");
        if (path.Length == 0) return;

        var t = Textures[0];
        var textureArray = new Texture2DArray(t.width, t.height, Textures.Length, t.format, t.mipmapCount > 1);
        textureArray.anisoLevel = t.anisoLevel;
        textureArray.filterMode = t.filterMode;
        textureArray.wrapMode = t.wrapMode;

        for(var i = 0; i < Textures.Length; i++)
        {
            var texture = Textures[i];
            for (int mipLevel = 0; mipLevel < t.mipmapCount; mipLevel++)
            {
                Graphics.CopyTexture(texture, 0, mipLevel, textureArray, i, mipLevel);
            }
        }

        AssetDatabase.CreateAsset(textureArray, path);
    }
}
