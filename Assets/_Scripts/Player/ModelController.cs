using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    [SerializeField] private List<SkinnedMeshRenderer> _skinnedMeshRenderer = new();

    public void ChangeModel(Material material)
    {
        if(_skinnedMeshRenderer == null) return;
        if(material == null) return;

        foreach(var skin in _skinnedMeshRenderer)
        {
            skin.material = material;
        }
    }
}
