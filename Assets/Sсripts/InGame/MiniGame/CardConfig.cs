using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Card Config")]
public class CardConfig : ScriptableObject
{
    public Sprite backSprite;
    public Sprite frontSprite;
    public Sprite winSprite;
    public int number;
}
