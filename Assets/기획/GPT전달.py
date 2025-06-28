import os

def collect_cs_to_txt(src_dir, output_file):
    with open(output_file, 'w', encoding='utf-8') as out_f:
        for root, _, files in os.walk(src_dir):
            for file in files:
                if file.endswith('.cs'):
                    cs_path = os.path.join(root, file)
                    try:
                        # 먼저 UTF-8로 시도
                        with open(cs_path, 'r', encoding='utf-8') as cs_file:
                            content = cs_file.read()
                    except UnicodeDecodeError:
                        try:
                            # 실패 시 CP949로 재시도
                            with open(cs_path, 'r', encoding='cp949') as cs_file:
                                content = cs_file.read()
                        except Exception as e:
                            print(f'Failed to read {cs_path}: {e}')
                            continue
                    
                    out_f.write(f'// File: {cs_path}\n')
                    out_f.write(content)
                    out_f.write('\n\n')

# 사용 예시
src_directory = "C:/Users/User/Project - C/Assets/Scripts"
output_txt = "output_combined.txt"
collect_cs_to_txt(src_directory, output_txt)