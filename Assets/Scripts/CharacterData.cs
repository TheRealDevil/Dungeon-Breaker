using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    public string className;
    public int maxHealth;
    public float moveSpeed;
    public int attackDamage;
    public Sprite characterSprite;

    [Header("Combat")]
    public GameObject bulletPrefab;
    public float fireRate;
    public int damage;

    [Header("Visuals")]
    public RuntimeAnimatorController animatorController;
}
