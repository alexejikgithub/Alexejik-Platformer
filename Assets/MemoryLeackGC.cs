using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryLeackGC : MonoBehaviour
{
    [SerializeField] private Sprite sprite;

    private List<Sprite> _spites = new List<Sprite>();

    private void Update()
    {
        for (int i = 0; i < 100; i++)
        {
            var instance = Instantiate(sprite);
            Destroy(instance);
        }

    }
}
