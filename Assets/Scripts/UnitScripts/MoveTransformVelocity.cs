using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformVelocity : MonoBehaviour, IMoveVelocity
{
    [SerializeField] private float moveSpeed;

    private Vector3 velocityVector;
    //private Character_Base characterBase;                //For character animation later

  //  private void Awake()
  //  {
     
        //characterBase = GetComponent<Character_Base>();
  // }

    public void SetVelocity(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
    }

    private void Update()
    {
        transform.position += velocityVector * moveSpeed * Time.deltaTime;
        //characterBase.PlayMoveAnim(velocityVector);
    }
}  