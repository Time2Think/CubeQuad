using UnityEditor;
using UnityEngine;

namespace Editor
{
    public  class FoldersTemplate : MonoBehaviour
    {
        [MenuItem("Assets/CreateTemplate")]
       
        private static void CreateTemplate()
        {
            // Создание папок шаблона
            AssetDatabase.CreateFolder("Assets", "2D");
            AssetDatabase.CreateFolder("Assets", "3D");
            AssetDatabase.CreateFolder("Assets", "Audio");
            AssetDatabase.CreateFolder("Assets", "Animations");
            AssetDatabase.CreateFolder("Assets", "Materials");
            AssetDatabase.CreateFolder("Assets", "Plugins");
            AssetDatabase.CreateFolder("Assets", "Resources");
            AssetDatabase.CreateFolder("Assets", "Fonts");
            AssetDatabase.CreateFolder("Assets", "Settings");
            AssetDatabase.CreateFolder("Assets", "Prefabs");
            AssetDatabase.CreateFolder("Assets", "Shaders");
            // Дополнительные папки...
        
            // Обновление области проекта
            AssetDatabase.Refresh();
        
            Debug.Log("Folder template created successfully!");
        }
    }
}
