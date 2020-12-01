using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum Direction {
        Left,
        Right,
        None

};
public class TouchHandler : MonoBehaviour , IPointerUpHandler, IPointerDownHandler
{
    
    [SerializeField] Direction btnDirection = Direction.None;
   public void OnPointerDown(PointerEventData eventData)
   {
       if (btnDirection == Direction.Left) {
           PlayerController.direction = Direction.Left;
       } else if(btnDirection == Direction.Right) {
           PlayerController.direction = Direction.Right;
       }
   }
   public void OnPointerUp(PointerEventData eventData)
   {
     if (PlayerController.direction == Direction.Left || PlayerController.direction == Direction.Right)
        PlayerController.direction = Direction.None;
       
   }
}
