    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Tower : MonoBehaviour
    {
        public GameObject archerPrefab = null;
        public GameObject warriorPrefab = null;


        public GameObject ScopeArea;
        public GameObject buyArcherBtn;
        public GameObject buyWarriorBtn;


        private GameObject soldier;
       
        private bool isFocused = false;
        private int status = 0;
        private Coroutine moveCoroutine; 

        void Start()
        {
            SetButtonsActive(false);
        }

        void Update()
        {

        }

        
        private void OnMouseDown()
        {
            Debug.Log("isFocused");
            isFocused = !isFocused;
            SetButtonsActive(isFocused);
        }

        private void SetButtonsActive(bool active)
        {
        
            ScopeArea.SetActive(active);
            // buttons showed when status = 0
            buyArcherBtn.SetActive(active && status == 0);
            buyWarriorBtn.SetActive(active && status == 0);
            // TODO: buttons showed when status = 1

            // TODO: buttons showed when status = 2
        }
        public IEnumerator BuySoldier(string soldierType) 
        {
            GameObject prefabToInstantiate = null;
            switch (soldierType)
            {
                case "Archer":
                    prefabToInstantiate = archerPrefab; break;
                case "Warrior":
                    prefabToInstantiate = warriorPrefab; break;
                default:
                    Debug.LogWarning("Unknown soldier type: " + soldierType);
                    yield break; // Dừng coroutine nếu loại lính không hợp lệ
            }

            if (prefabToInstantiate != null)
            {
                Debug.Log("Tower position: " + transform.localPosition);
                soldier = Instantiate(prefabToInstantiate, transform.TransformPoint(new Vector3(0, -1, -1)), Quaternion.identity, transform);

                soldier.GetComponent<Soldier>().SetTower(this);
                Debug.Log("Soldier position: " + soldier.transform.localPosition);
                
            }
            yield return new WaitForSeconds(0.1f); 
            status = 1;
            SetButtonsActive(true);
        }
        public void UpgradeTower()
        {
            if (status == 1)
            {
                status = 2;
            }
        }
    }

