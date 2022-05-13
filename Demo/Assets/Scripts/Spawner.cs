using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
     public List<GameObject> activeDiamondList;
     public List<SpawnPoint> spawnPointsList;
     public List<SpawnPoint> activeSpawnPointsList;
     public ObjectPool objectPool;
     
     private bool _timerActive;
     [SerializeField] private float timerValue;
     [SerializeField] private float defaultTimerValue;

     private void OnEnable()
     {
          Actions.OnDiamondDead += DeactiveDiamond;
     }

     private void OnDisable()
     {
          Actions.OnDiamondDead -= DeactiveDiamond;
     }

     private void Start()
     {
          StartCoroutine(GameStartSpawn());
     }

     private void Update()
     {
          if (_timerActive)
          {
               timerValue -= Time.deltaTime;
               if (timerValue <= 0)
               {
                    _timerActive = false;
                    timerValue = defaultTimerValue;
                    if (activeDiamondList.Count < 5)
                    {
                         SpawnDiamond();
                    }
               }
          }
     }

     IEnumerator GameStartSpawn()
     {
          yield return new WaitForEndOfFrame();
          for (int i = 0; i < 5; i++)
          {
               SpawnDiamond();
               yield return new WaitForSeconds(0.2f);
          }
     }

     public void SpawnDiamond()
     {
          CollectableDiamond diamond = ObjectPool.SharedInstance.GetPooledObject().GetComponent<CollectableDiamond>();
          if (diamond != null)
          {
               SpawnPoint spawnPoint = GetSpawnPoint();
               spawnPoint.HasDiamond = true;

               diamond.transform.SetParent(spawnPoint.transform);
               diamond.transform.localPosition = Vector3.zero;
               diamond.gameObject.SetActive(true);
               activeDiamondList.Add(diamond.gameObject);
          }
     }

     public SpawnPoint GetSpawnPoint()
     {
          SpawnPoint spawnPoint = spawnPointsList[Random.Range(0, spawnPointsList.Count)];
          spawnPointsList.Remove(spawnPoint);
          activeSpawnPointsList.Add(spawnPoint);
          return spawnPoint;
     }

     public void Reset()
     {
          for (int i = 0; i < activeSpawnPointsList.Count; i++)
          {
               SpawnPoint spawnPoint = activeSpawnPointsList[i];
               spawnPoint.HasDiamond = false;
               spawnPointsList.Add(activeSpawnPointsList[i]);
          }
          activeSpawnPointsList.Clear();

          for (int i = 0; i < activeDiamondList.Count; i++)
          {
               CollectableDiamond diamond = activeDiamondList[i].GetComponent<CollectableDiamond>();
               DeactiveDiamond(diamond.gameObject);
          }
     }
     
     public void DeactiveDiamond(GameObject currentDiamond)
     {
          if(!activeDiamondList.Contains(currentDiamond))  return;
          _timerActive = true;
          
          activeDiamondList.Remove(currentDiamond);
          
          SpawnPoint spawnPoint = currentDiamond.GetComponentInParent<SpawnPoint>();
          spawnPoint.HasDiamond = false;
          activeSpawnPointsList.Remove(spawnPoint);
          spawnPointsList.Add(spawnPoint);
          
          currentDiamond.transform.SetParent(objectPool.transform);
          currentDiamond.SetActive(false);
     }
}
