using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class doorScript : MonoBehaviour
{
    [SerializeField] private Swtich[] swtiches;
   [SerializeField] private Tilemap doorTile;
    private bool Alltrue = false;

    // Update is called once per frame
    void Update()
    {
        if (allButtonPressed())
        {
            doorTile.ClearAllTiles();
        }
    }

    private bool allButtonPressed()
    {
        Alltrue = true;
        for (int i = 0; i < swtiches.Length; ++i)
        {
            if (swtiches[i].isPressed == false)
            {
                Alltrue = false;
                break;
            }
        }
        return Alltrue;
    }
}
