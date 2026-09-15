using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

// Примерный скрипт постобработки WebGL билда.
// Помещается в Assets/Editor/WebGLPostProcess.cs
// После сборки WebGL этот скрипт попытается найти index.html и вставит
// подключение Yandex.Games SDK и контейнер для боковой рекламы.

public static class WebGLPostProcess
{
    [PostProcessBuild(9999)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        #if UNITY_WEBGL
        if (target != BuildTarget.WebGL) return;
        #endif

        string indexPath = Path.Combine(pathToBuiltProject, "index.html");
        if (!File.Exists(indexPath))
        {
            UnityEngine.Debug.LogWarning("WebGL index.html not found: " + indexPath);
            return;
        }

        string index = File.ReadAllText(indexPath);

        string insert = @"<!-- YANDEX GAMES SDK -->
<script src=\"https://yastatic.net/games-sdk/v2/sdk.js\"></script>
<script>
  // Инициализация YaGames. Используйте актуальную документацию SDK.
  // Пример:
  // YaGames.init().then(() => { console.log('YaGames ready'); });
</script>

<!-- Контейнер для боковой рекламы -->
<div id=\"side-ad-container\" style=\"position:fixed;right:0;top:0;z-index:1000;width:300px;height:600px;pointer-events:auto;\"></div>
";

        if (index.Contains("</body>"))
        {
            index = index.Replace("</body>", insert + "\n</body>");
            File.WriteAllText(indexPath, index);
            UnityEngine.Debug.Log("Patched index.html for Yandex.Games SDK.");
        }
    }
}
