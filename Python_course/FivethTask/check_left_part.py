import re


def check_left_part(inp):
    return re.findall(r"[A-za-z0-9_\.]{1,30}", inp)