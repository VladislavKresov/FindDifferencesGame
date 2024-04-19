using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelConstructor : MonoBehaviour {
    [SerializeField] private GameObject[] _levelPrefabs;
    [SerializeField] private RectTransform _imagesParent;
    private float _pixelsPerUnit = 100f;

    private void Start() {
        CreateLevel(0);
        _pixelsPerUnit = _imagesParent?.GetComponentInParent<CanvasScaler>()?.referencePixelsPerUnit ?? 100f;
    }

    public void CreateLevel(int level) {
        if (level < 0 || _levelPrefabs.Length <= level) {
            Debug.LogError("Trying to create invalid level");
        }

        var renderers = _levelPrefabs[level].transform.GetComponentsInChildren<SpriteRenderer>();
        int minLayerOrder = renderers.Min(renderer => renderer.sortingOrder);
        var backgroundRenderer = renderers.Where(renderer => renderer.sortingOrder == minLayerOrder).Last();
        var hiddenObjectsRenderers = renderers.Where(renderer => renderer != backgroundRenderer).ToArray();

        var backgroundWithHiddenObjectsImage = InstantiateImageWithSprite(backgroundRenderer.name, null, backgroundRenderer.sprite);
        backgroundWithHiddenObjectsImage.rectTransform.sizeDelta = backgroundRenderer.size * _pixelsPerUnit;

        for (int rendererIndex = 0; rendererIndex < hiddenObjectsRenderers.Length; rendererIndex++) {
            SpriteRenderer renderer = hiddenObjectsRenderers[rendererIndex];
            var hiddenObject = InstantiateImageWithSprite($"HiddenObject {rendererIndex}", backgroundWithHiddenObjectsImage.rectTransform, renderer.sprite);
            hiddenObject.AddComponent<HiddenObjectClickHandler>();
            var rectTransform = hiddenObject.rectTransform;
            var offset = (renderer.transform.position - backgroundRenderer.transform.position) * _pixelsPerUnit;
            rectTransform.position = backgroundWithHiddenObjectsImage.rectTransform.position + offset;
            rectTransform.sizeDelta = renderer.size * _pixelsPerUnit;
            hiddenObject.AddComponent<ScaleProportionallyToParent>();
        }

        Image clearBackgroundImage = Instantiate(backgroundWithHiddenObjectsImage);
        var images = clearBackgroundImage.GetComponentsInChildren<Image>();
        foreach (Image image in images) {
            if(image != clearBackgroundImage)
            image.color = new Color(0,0,0,0);
        }

        clearBackgroundImage.transform.SetParent(_imagesParent);
        backgroundWithHiddenObjectsImage.transform.SetParent(_imagesParent);
    }

    private Image InstantiateImageWithSprite(string imageGameObjectName, RectTransform parent, Sprite sprite) {
        GameObject imageGO = new GameObject(imageGameObjectName, typeof(RectTransform), typeof(Image));
        imageGO.transform.SetParent(parent);
        var image = imageGO.GetComponent<Image>();
        image.sprite = sprite;
        return image;
    }
}
