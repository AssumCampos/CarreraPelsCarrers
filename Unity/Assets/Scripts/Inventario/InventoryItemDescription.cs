using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Tipos de inventario
 */
public enum Type
{
    MONUMENTS,
    GARBAGE
}

[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItemDescription : ScriptableObject
{
    public int id;
    public Type type;
    public Sprite icon;
    public int value;
    public AudioClip sound;
}