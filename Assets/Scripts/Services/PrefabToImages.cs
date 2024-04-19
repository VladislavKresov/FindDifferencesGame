using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PrefabToImages : MonoBehaviour, IService {
    public RectTransform ImagesContainer;
    private float _pixelsPerUnit = 100f;

    public void CreateImages(GameObject levelPrefab) {
        var renderers = levelPrefab.transform.GetComponentsInChildren<SpriteRenderer>();
        int minLayerOrder = renderers.Min(renderer => renderer.sortingOrder);
        var backgroundRenderer = renderers.Where(renderer => renderer.sortingOrder == minLayerOrder).Last();
        var hiddenObjectsRenderers = renderers.Where(renderer => renderer != backgroundRenderer).ToArray();

        var ChangedImage = CreateImageWithSprite(backgroundRenderer.name, null, backgroundRenderer.sprite);
        ChangedImage.rectTransform.sizeDelta = backgroundRenderer.size * _pixelsPerUnit;

        for (int rendererIndex = 0; rendererIndex < hiddenObjectsRenderers.Length; rendererIndex++) {
            SpriteRenderer renderer = hiddenObjectsRenderers[rendererIndex];
            var hiddenObject = CreateImageWithSprite($"HiddenObject {rendererIndex}", ChangedImage.rectTransform, renderer.sprite);
            hiddenObject.AddComponent<HiddenObjectClickHandler>();
            var rectTransform = hiddenObject.rectTransform;
            var offset = (renderer.transform.position - backgroundRenderer.transform.position) * _pixelsPerUnit;
            rectTransform.position = ChangedImage.rectTransform.position + offset;
            rectTransform.sizeDelta = renderer.size * _pixelsPerUnit;
            hiddenObject.AddComponent<ScaleProportionallyToParent>();
        }

        Image RefferenceImage = CreateRefferenceImage(ChangedImage);

        RefferenceImage.transform.SetParent(ImagesContainer);
        ChangedImage.transform.SetParent(ImagesContainer);
    }

    private static Image CreateRefferenceImage(Image backgroundWithHiddenObjectsImage) {
        Image clearBackgroundImage = Instantiate(backgroundWithHiddenObjectsImage);
        var images = clearBackgroundImage.GetComponentsInChildren<Image>();
        foreach (Image image in images) {
            if (image != clearBackgroundImage)
                image.color = new Color(0, 0, 0, 0);
        }

        return clearBackgroundImage;
    }

    private Image CreateImageWithSprite(string imageGameObjectName, RectTransform parent, Sprite sprite) {
        GameObject imageGO = new GameObject(imageGameObjectName, typeof(RectTransform), typeof(Image));
        imageGO.transform.SetParent(parent);
        var image = imageGO.GetComponent<Image>();
        image.sprite = sprite;
        return image;
    }
}
