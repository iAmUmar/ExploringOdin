using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu()]
public class ItemSO : ScriptableObject
{
    [PreviewField(60), HideLabel]
    [HorizontalGroup("Split", 60)]
    public Sprite itemSprite;

    [VerticalGroup("Split/Right"), LabelWidth(120)]
    public string itemName;

    [VerticalGroup("Split/Right"), LabelWidth(120)]
    public string itemType;

    [VerticalGroup("Split/Right"), LabelWidth(120)]
    [Range(0, 10)]
    public int itemAmount;

    [VerticalGroup("Split/Right"), LabelWidth(120)]
    public int itemPrice;
}
