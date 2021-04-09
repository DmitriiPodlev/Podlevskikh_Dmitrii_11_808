import re


def split_by_symbol(inp):
    parts = re.split('@', inp)
    if (len(parts) > 2) or (len(parts) == 1):
        raise Exception("Incorrect mail, too many symbol @")
    else:
        return parts
