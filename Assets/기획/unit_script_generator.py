import pandas as pd
import os

# CSV 파일 경로 설정
csv_path = "C:/Users/User/Project - C/Assets/기획/Units.csv"  # 실제 CSV 파일 경로로 수정하세요
df = pd.read_csv(csv_path).fillna("")

# 출력 폴더 설정
output_dir = "C:/Users/User/Project - C/Assets/Scripts/Units"
os.makedirs(output_dir, exist_ok=True)

# 병종 문자열을 UnitType으로 매핑
type_map = {
    "Melee": "Melee",
    "Cavalry": "Cavalry",
    "Ranged": "Ranged",
    "RangedCavalry": "RangedCavalry"
}

# 진영 문자열을 Faction으로 매핑
faction_map = {
    "Friendly": "Friendly",
    "Enemy": "Enemy"
}

for idx, row in df.iterrows():
    class_name = row['name'].replace(" ", "")
    unit_type = type_map.get(row['type'], "Melee")
    faction = faction_map.get(row['faction'], "Friendly")
    traits = [t.strip() for t in str(row['traits']).split(',') if t.strip()]

    trait_lines = ""
    if traits:
        trait_lines = "\n        unitTrait = new System.Collections.Generic.List<UnitTrait>\n        {\n"
        trait_lines += ",\n".join([f"            gameObject.AddComponent<{trait}>()" for trait in traits])
        trait_lines += "\n        };"

    cs_code = f"""using UnityEngine;

public class {class_name} : Unit
{{
    private void Awake()
    {{
        unitName = "{row['name']}";
        faction = Faction.{faction};
        unitType = UnitType.{unit_type};

        stats = new UnitStats
        {{
            maxHealth = {row['maxHealth']},
            currentHealth = {row['maxHealth']},
            attackPower = {row['attack']},
            defensePower = {row['defense']},
            range = {row['range']}
        }};

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;{trait_lines}
    }}
}}
"""

    file_name = f"{class_name}.cs"
    file_path = os.path.join(output_dir, file_name)
    with open(file_path, "w", encoding="utf-8") as f:
        f.write(cs_code)

print(f"Generated {len(df)} unit scripts in '{output_dir}'")
