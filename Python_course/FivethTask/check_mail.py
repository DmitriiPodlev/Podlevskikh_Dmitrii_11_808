import re


def check_mail(inp):
    return re.findall(r"^[A-za-z0-9_\.]{1,30}[@](?:mail|gmail|kpfu|clerk|eritrea)[.](?:ru|com|lv|cc|net)", inp)