using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;

namespace RPG.Control
{
  public class PlayerController : MonoBehaviour
  {
    private void Update()
    {
      InteractWithCombat();
      InteractWithMovement();
    }

    private void InteractWithCombat()
    {
      throw new NotImplementedException();
    }

    private void InteractWithMovement()
    {
      if (Input.GetMouseButton(0))
      {
        MoveToCursor();
      }
    }

    private void MoveToCursor()
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      bool hasHit = Physics.Raycast(ray, out hit);
      if (hasHit)
      {
        GetComponent<Mover>().MoveTo(hit.point);
      }
    }
  }
}
