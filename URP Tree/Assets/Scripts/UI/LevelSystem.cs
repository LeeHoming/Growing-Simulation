using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public SkillSettingPanel SkillPanel;

    public GameOfPlants GOP;

    private int

            T0Max = 5,
            T1Max = 4,
            T0,
            T1;

    private float

            NearbyRadiusMin = 2.5f,
            WaterResistFactorMax,
            NearbyRadius,
            WaterResistFactor;

    private void Awake()
    {
        //SkillPanel = GetComponent<SkillSettingPanel>();
    }

    public void ParameterCulculator()
    {
        //Grow speed adjustment
        //Get GS from panel
        int GS = SkillPanel.GetNumByType(SkillType.GrowSpeed);
        if (GS < 3)
        {
            T0 = T0Max - GS;
        }
        else if (GS >= 3)
        {
            T0 = T0Max - 3;
            T1 = T1Max - GS + 2;
        }

        //Drought adjust
        int DR = SkillPanel.GetNumByType(SkillType.Drought);
        if (DR >= 4)
        {
            //CrowdFactor = 3f;
        }

        //Spread distance
        int DS = SkillPanel.GetNumByType(SkillType.Distance);
        NearbyRadius = NearbyRadiusMin + 0.8f * (float) DS;
    }
}
