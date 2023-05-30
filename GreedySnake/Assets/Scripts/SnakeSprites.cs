using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SnakeSprites", menuName = "SnakeSprites")]
public class SnakeSprites : ScriptableObject
{
    [SerializeField]
    public SerializedDictionary<Options.Theme, Sprite> head = new SerializedDictionary<Options.Theme, Sprite>();

    [SerializeField] public SerializedDictionary<Options.Theme, Sprite[]> body =
        new SerializedDictionary<Options.Theme, Sprite[]>();
}