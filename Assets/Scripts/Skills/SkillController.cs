using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class SkillController : MonoBehaviour
{
    public GameObject skillArea;
    public GameObject explosionPrefab;
    bool checkerActiveSkill = false;
    private Camera mainCamera;
    public TMP_Text numSkillText;
    public int numSkills;
    public GameObject shop;
    int damage = 50;

    private void Start()
    {
        mainCamera = Camera.main;
        skillArea.SetActive(false);

        numSkills = PlayerPrefs.GetInt("Skills");
        Debug.Log("numSkills " + numSkills);
        numSkillText.text = numSkills.ToString();
    }


    public void clickSkill()
    {
        
        checkerActiveSkill = !checkerActiveSkill;
        skillArea.SetActive(checkerActiveSkill);
    }

    private void Update()
    {
        numSkills = PlayerPrefs.GetInt("Skills");
        numSkillText.text = numSkills.ToString();
        if (checkerActiveSkill)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.nearClipPlane;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            skillArea.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
           
            
            // mouse right click = cancle skill
            if (Input.GetMouseButtonDown(1))
            {
                clickSkill();
                
                return;
            }
            // left click = active skill
            if (Input.GetMouseButtonDown(0) && !PointerOnUI())
            {
                // if run off skills
                if (numSkills == 0) {
                    clickSkill();
                    Time.timeScale = 0f;
                    openShop();
                    return;
                }
                StartCoroutine(ActivateExplosionCoroutine());
                ApplyDameForEnemy();
                numSkills --;
                PlayerPrefs.SetInt("Skills", numSkills);
                numSkillText.text = numSkills.ToString();
                return;
            }
        }
    }
    private IEnumerator ActivateExplosionCoroutine()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.SetActive(true);
        explosion.transform.SetParent(skillArea.transform);
        explosion.transform.localScale = skillArea.transform.Find("Explosions").transform.localScale;
        explosion.transform.position = skillArea.transform.position;
        explosion.transform.SetParent(skillArea.transform.parent);
        
        yield return new WaitForSeconds(0.9f);
        Destroy(explosion);
        
    }
    private bool PointerOnUI()
    {
        bool isOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        Debug.Log($"Pointer over UI: {isOverUI}");
        return isOverUI;
    }
    private void ApplyDameForEnemy() {
        Collider2D skillAreaCollider = skillArea.GetComponent<Collider2D>();
        if (skillAreaCollider != null)
        {
            List<Collider2D> colliders = new List<Collider2D>();
            int colliderCount = skillAreaCollider.OverlapCollider(new ContactFilter2D(), colliders);

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("enemy"))
                {
                    // Debug.Log("Enemy detected in skill area!");
                    collider.GetComponent<Torch>().TakeDamage(damage);
                }
            }
        }
        else
        {
            Debug.LogWarning("No Collider found on skillArea!");
        }
    }
    public void openShop() {
        
        shop.SetActive(true);
    }
    public void closeShop() {
        shop.SetActive(false);
    }
}

