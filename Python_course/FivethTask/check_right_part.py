import re


def check_right_part(inp):
    return re.findall(r"(?:mail|gmail|kpfu|clerk|eritrea)[.](?:ru|com|lv|cc|net)", inp)