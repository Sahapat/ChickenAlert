using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem {

    string Name { get; }

    Sprite Image { get;  }

    void OnPickup();

    void OnDrop();

    void OnUse();
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }

    public IInventoryItem Item;
}

