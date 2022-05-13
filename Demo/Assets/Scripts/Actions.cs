using UnityEngine;
using UnityEngine.Events;

public class Actions
{
   public static UnityAction OnGameStart;
   public static UnityAction OnGameDone;
   
   public static UnityAction<int> OnDiamondValue;
   public static UnityAction<int> OnScoreText;
   public static UnityAction<int> OnScoreFinishText;
   public static UnityAction<GameObject> OnDiamondDead;
}
