import pandas as pd
import os

# CSV 파일 경로 설정
csv_path = "C:/Users/User/Project - C/Assets/기획/Units.csv"
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

    # 트레잇 처리
    trait_lines = ""
    if traits:
        trait_lines = "\n        unitTrait = new System.Collections.Generic.List<UnitTrait>\n        {\n"
        trait_lines += ",\n".join([f"            gameObject.AddComponent<{trait}>()" for trait in traits])
        trait_lines += "\n        };"

    # 업그레이드 블럭 - 적 유닛이면 stats 복붙, 아군이면 upgraded 값 사용
    if faction == "Enemy":
        upgraded_block = f"""

        upgradedStats = new UnitStats
        {{
            maxHealth = {int(row['maxHealth'])},
            currentHealth = {int(row['maxHealth'])},
            attackPower = {int(row['attack'])},
            defensePower = {int(row['defense'])},
            range = {int(row['range'])}
        }};"""
    else:
        upgraded_block = f"""

        upgradedStats = new UnitStats
        {{
            maxHealth = {int(row.get('upgradedHealth', row['maxHealth']))},
            currentHealth = {int(row.get('upgradedHealth', row['maxHealth']))},
            attackPower = {int(row.get('upgradedAttack', row['attack']))},
            defensePower = {int(row.get('upgradedDefense', row['defense']))},
            range = {int(row.get('upgradedRange', row['range']))}
        }};"""

    # 전체 클래스 코드 구성
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
            maxHealth = {int(row['maxHealth'])},
            currentHealth = {int(row['maxHealth'])},
            attackPower = {int(row['attack'])},
            defensePower = {int(row['defense'])},
            range = {int(row['range'])}
        }};{upgraded_block}


    }}
}}
"""

    # 파일 저장
    file_name = f"{class_name}.cs"
    file_path = os.path.join(output_dir, file_name)
    with open(file_path, "w", encoding="utf-8") as f:
        f.write(cs_code)

print(f"Generated {len(df)} unit scripts in '{output_dir}'")
