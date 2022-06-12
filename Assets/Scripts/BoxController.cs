using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoxController : MonoBehaviour
{
    public void setPosition(Vector2 direction) {
        transform.position += (Vector3)direction;
        
    }
}
