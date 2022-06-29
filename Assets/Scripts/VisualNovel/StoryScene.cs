using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "NewStoryScene",menuName = "Data/New Story Scene")]
[System.Serializable]
public class StoryScene : GameScene
{
    public List<Sentence> sentences;
    public Sprite background;
    public string[] nextScene;

    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public Sprite currentSprite;
    }
}
public class GameScene: ScriptableObject
{

}
