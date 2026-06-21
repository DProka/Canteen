using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StaffType
{
    Tape,
    Bread,
    Sauce,
    Burner,
    Glass,
    Drink,
    RawFood,
}

public class ShopController : MonoBehaviour
{
    [SerializeField] ShopStaffPrefab tapesPrefab;
}
