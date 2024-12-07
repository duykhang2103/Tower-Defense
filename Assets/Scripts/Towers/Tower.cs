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
       
        public bool isFocused = false;
        private int status = 0;
        private Coroutine moveCoroutine; 

        void Start()
        {
            ScopeArea.SetActive(false);
            buyArcherBtn.SetActive(false);
            buyWarriorBtn.SetActive(false);
        }

        void Update()
        {
            if (!soldier) {
                status = 0;
            }
        }
        public void ToggleButton()
        {
            isFocused = !isFocused;
            ScopeArea.SetActive(isFocused);
            // buttons showed when status = 0
            buyArcherBtn.SetActive(isFocused && status == 0);
            buyWarriorBtn.SetActive(isFocused && status == 0);
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
                    yield break;
            }

            if (prefabToInstantiate != null)
            {
                soldier = Instantiate(prefabToInstantiate, transform.TransformPoint(new Vector3(0, -1, 0)), Quaternion.identity, transform);
                soldier.GetComponent<Soldier>().SetTower(this);
            }
            yield return new WaitForSeconds(0.1f); 
            status = 1;
            isFocused = false;
            ToggleButton();
            
            



        }
        public void UpgradeTower()
        {
            if (status == 1)
            {
                status = 2;
            }
        }
    }

