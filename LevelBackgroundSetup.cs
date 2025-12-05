using UnityEngine;

public class LevelBackgroundSetup : MonoBehaviour
{
    public SpriteRenderer myBackground;
    public Sprite darkSprite;
    void Start()
    {
        if (MainMenuController.isDarkMode == true)
        {
            myBackground.sprite = darkSprite;
        }
    }
}