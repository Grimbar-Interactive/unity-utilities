using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace GI.UnityToolkit.Utilities
{
    public static class SpriteCache
    {
        private const int MAX_CACHE_AMOUNT = 100;
        
        private static readonly Dictionary<string, LoadedSprite> Cache = new();
        private static readonly Dictionary<Image, string> Loaded = new();

        static SpriteCache()
        {
            Cache.Clear();
            Loaded.Clear();
        }

        public static void LoadFromUrl(this Image image, string url, Action<bool> onCompleted)
        {
            if (image == null)
            {
                Debug.LogWarning("Load failed: image is null!");
                onCompleted?.Invoke(false);
                return;
            }
            
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogWarning("Load failed: URL is null or empty!", image);
                onCompleted?.Invoke(false);
                return;
            }
            
            if (Loaded.TryGetValue(image, out var loadedUrl))
            {
                // Skip loading if the sprite url is already loaded into the given image.
                if (url == loadedUrl)
                {
                    onCompleted?.Invoke(true);
                    return;
                }
                
                image.Unload();
            }

            // Check if sprite is already loaded into cache.
            if (Cache.TryGetValue(url, out var loadedSprite))
            {
                image.sprite = loadedSprite.Sprite;
                
                loadedSprite.UseCount++;
                Cache[url] = loadedSprite;
                
                Loaded.Add(image, url);
                onCompleted?.Invoke(true);
                return;
            }

            if (Cache.Count > MAX_CACHE_AMOUNT)
            {
                Cleanup();
            }
            
            image.StartCoroutine(LoadSpriteFromUrl());

            IEnumerator LoadSpriteFromUrl()
            {
                var request = UnityWebRequestTexture.GetTexture(url);
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogWarning("Failed to load sprite from URL \"{url}\"!");
                    onCompleted?.Invoke(false);
                    yield break;
                }

                var texture = DownloadHandlerTexture.GetContent(request);
                var sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), Vector2.one * 0.5f);
                image.sprite = sprite;
                Loaded.Add(image, url);
                Cache.Add(url, new LoadedSprite(sprite));
                onCompleted?.Invoke(true);
            }
        }

        public static bool Unload(this Image image, bool forceDeletion = false)
        {
            if (image == null)
            {
                Debug.LogWarning("Unload failed: image is null!");
                return false;
            }
            
            if (!Loaded.TryGetValue(image, out var loadedUrl)) return false;
            
            if (Cache.TryGetValue(loadedUrl, out var loadedSprite))
            {
                loadedSprite.UseCount = Mathf.Max(loadedSprite.UseCount - 1, 0);
                Cache[loadedUrl] = loadedSprite;
            }
            else
            {
                Debug.LogError($"Failed to update sprite use count: URL is not present in cache! URL: \"{loadedUrl}\"");
            }

            Loaded.Remove(image);
            return true;
        }

        private static void Cleanup()
        {
            foreach (var kvp in Cache.Where(s => s.Value.UseCount <= 0))
            {
                kvp.Value.Sprite = null;
                Cache.Remove(kvp.Key);
            }
            Resources.UnloadUnusedAssets();
        }

        private class LoadedSprite
        {
            public Sprite Sprite;
            public int UseCount;

            public LoadedSprite(Sprite sprite)
            {
                Sprite = sprite;
                UseCount = 1;
            }
        }
    }
}