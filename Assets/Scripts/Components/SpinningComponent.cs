using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.Components
{
    public class SpinningComponent : MonoBehaviour
    {
        [SerializeField]private float _rotationSpeed;

        

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(0,0,1) * _rotationSpeed * Time.deltaTime);
        }
    }
}
