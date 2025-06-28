import os
from notion_client import Client

# 📌 Notion API 토큰 및 데이터베이스 ID 설정
NOTION_TOKEN = "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" # ← 반드시 본인의 secret_... 토큰으로 변경
DATABASE_ID =  "2192e4144684809fab16f5684610a78e"   # ← v= 뒤의 ID 사용

# 🔧 Notion 클라이언트 초기화
notion = Client(auth=NOTION_TOKEN)

# 📁 데스크탑 경로 설정
desktop_path = os.path.join(os.path.expanduser("~"), "Desktop")
output_txt_path = os.path.join(desktop_path, "notion_export.txt")


# ✅ 블록 내용 재귀적으로 추출 (표 포함)
def extract_text_from_block_recursive(block_id, level=0):
    result_lines = []

    blocks = notion.blocks.children.list(block_id=block_id)
    for block in blocks["results"]:
        block_type = block["type"]

        # 일반 텍스트 블록
        if "text" in block.get(block_type, {}):
            texts = block[block_type]["text"]
            line = ''.join([t["plain_text"] for t in texts])
            if line.strip():
                indent = '  ' * level
                result_lines.append(f"{indent}{line}")

        # 표 블록
        elif block_type == "table":
            result_lines.append("Table:")
            table_rows = notion.blocks.children.list(block["id"])
            for row in table_rows["results"]:
                if row["type"] == "table_row":
                    cells = row["table_row"]["cells"]
                    row_data = []
                    for cell in cells:
                        cell_text = ''.join(t.get("plain_text", "") for t in cell)
                        row_data.append(cell_text)
                    result_lines.append("  " + " | ".join(row_data))

        # 기타 자식 블록이 있는 경우 재귀 탐색
        elif block.get("has_children"):
            result_lines.extend(extract_text_from_block_recursive(block["id"], level=level+1))

    return result_lines


# ✅ 전체 페이지 순회하여 텍스트 저장
def export_notion_database():
    results = notion.databases.query(database_id=DATABASE_ID)

    with open(output_txt_path, "w", encoding="utf-8") as f:
        for i, page in enumerate(results['results'], 1):
            page_id = page["id"]
            f.write(f"\n--- Page {i} ---\n")

            # 제목 출력
            for key, val in page["properties"].items():
                if val["type"] == "title":
                    title = ''.join([t["plain_text"] for t in val["title"]])
                    f.write(f"Title: {title}\n")
                    break

            # 본문 블록 출력 (표 포함)
            block_lines = extract_text_from_block_recursive(page_id)
            for line in block_lines:
                f.write(line + "\n")

    print(f"✅ Export complete: {output_txt_path}")


# 실행
export_notion_database()
