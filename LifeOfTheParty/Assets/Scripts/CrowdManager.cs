using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrowdManager : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private List<GameObject> npcList = new List<GameObject>();
    private List<Vector3> blackAreaPositions = new List<Vector3>();
    private List<Vector3> randomPositions = new List<Vector3>();
    public int crowdSize = 200;
    public int skipEveryNthPixel = 1;
    private float spawnOffsetZ = 0.01f;

    [SerializeField] private List<Sprite> NPCSpriteOptions;

    [SerializeField] private Image crowdPathImg;
    private Texture2D maskTexture;

    private void Start() {
        maskTexture = crowdPathImg.sprite.texture;
    
        GetBlackAreaPositions();
        PickRandomPositions();
        SpawnNPCs();
        CleanUp();
    }

    private void GetBlackAreaPositions()
    {
        if (maskTexture == null || !maskTexture.isReadable)
        {
            Debug.LogError("Mask texture null or not readable.");
            return;
        }

        Color[] pixels = maskTexture.GetPixels();
        int w = maskTexture.width;
        int h = maskTexture.height;

        RectTransform rt = crowdPathImg.GetComponent<RectTransform>();
        Vector2 rectSize = rt.rect.size;
        Vector2 pivot = rt.pivot;

        if (w < 4 || h < 4 || skipEveryNthPixel < 1) {
            Debug.LogWarning("Suspicious texture size or skip value");
        }

        Debug.Log(w + " " + h);

        blackAreaPositions = new List<Vector3>(10000); 

        int count = 0;

        for (int y = 0; y < h; y += skipEveryNthPixel)
        {
            for (int x = 0; x < w; x += skipEveryNthPixel)
            {
                Color p = pixels[y * w + x];

                if (p.r + p.g + p.b == 0)
                {
                    float uvX = (float)x / (w - 1);
                    float uvY = (float)y / (h - 1);

                    // If using cropped UVs
                    // uvX = Mathf.Lerp(rawImage.uvRect.xMin, rawImage.uvRect.xMax, uvX);
                    // uvY = Mathf.Lerp(rawImage.uvRect.yMin, rawImage.uvRect.yMax, uvY);

                    // Local position — pivot aware, bottom-left origin
                    Vector2 localPos = new Vector2(
                        (uvX - pivot.x) * rectSize.x,
                        (uvY - pivot.y) * rectSize.y
                    );

                    Vector3 worldPos = rt.TransformPoint(localPos);
                    worldPos.z += spawnOffsetZ;

                    blackAreaPositions.Add(worldPos);
                    count++;
                }
            }
        }

        Debug.Log($"Found {count} black positions (skip={skipEveryNthPixel})");
    }

    private void PickRandomPositions() {
        if (blackAreaPositions.Count < crowdSize) {
            randomPositions = blackAreaPositions;
        }

        randomPositions = new List<Vector3>();
        for (int i = 0; i < crowdSize; i++) {
            Vector2 randomPosition = blackAreaPositions[Random.Range(0, blackAreaPositions.Count)];
            randomPositions.Add(randomPosition);
        }
    }

    private void SpawnNPCs() {
        foreach (Vector2 position in randomPositions) {
            GameObject npc = Instantiate(npcPrefab, position, Quaternion.identity, transform);
            npc.GetComponent<NPC>().SetSprite(NPCSpriteOptions[Random.Range(0, NPCSpriteOptions.Count)]);
            npcList.Add(npc);
        }
    }
    private void CleanUp() {
        randomPositions.Clear();
        blackAreaPositions.Clear();
    }
}
