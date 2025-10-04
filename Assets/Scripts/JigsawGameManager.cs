using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JigsawGameManager : MonoBehaviour {

    [Header("UI Elements")]
    [SerializeField] private List<Texture2D> imageTextures;
    [SerializeField] private Transform levelSelectPanel;
    [SerializeField] private Image levelSelectPrefabs;

    void Start(){
        //Create the UI
        foreach (Texture2D texture in imageTextures) {
            Image image = Instantiate(levelSelectPrefabs, levelSelectPanel);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        }
    }
}
