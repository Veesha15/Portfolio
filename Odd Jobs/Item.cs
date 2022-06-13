using UnityEngine;

[CreateAssetMenu(menuName = ("Scriptable Object/Item"), fileName = ("New Item"))]
public class Item : ScriptableObject
{
    public Sprite icon;
    public int load;
}
