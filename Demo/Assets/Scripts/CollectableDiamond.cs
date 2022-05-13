using UnityEngine;

public class CollectableDiamond : MonoBehaviour
{
   public int DiamondValue = 10;
   public bool DiamondActive;

   private void OnEnable()
   {
      DiamondActive = true;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player") && DiamondActive)
      {
         DiamondActive = false;
         Actions.OnDiamondDead?.Invoke(this.gameObject);
         Actions.OnDiamondValue?.Invoke(DiamondValue);
        
      }
   }
}
