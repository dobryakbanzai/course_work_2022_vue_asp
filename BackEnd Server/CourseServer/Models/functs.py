import random
lengh = 10


def expr(s, i=0):
    a, i = sum(s, i)
    while i < len(s) and (s[i] == '+' or s[i] == '-'):
        ch = s[i]
        i += 1
        b, i = sum(s, i)
        if ch == '+':
            a += b
        else:
            a -= b
    return a, i


def sum(s, i):
    a, i = mult(s, i)
    while i < len(s) and (s[i] == '*' or s[i] == '/'):
        ch = s[i]
        i += 1
        b, i = mult(s, i)
        if ch == '*':
            a *= b
        else:
            a = a // b
    return a, i


def mult(s, i):
    a = 0
    if s[i] == '(':
        i += 1
        a, i = expr(s, i)
        i += 1
    else:
        a, i = num(s, i)
    return a, i


def num(s, i):
    st = ""
    p = 1
    # if s[i] == '-':
    #     p = -1
    #     i += 1
    while i < len(s) and s[i] >= '0' and s[i] <= '9':
        st += s[i]
        i += 1
    return p * int(st), i


def e(len):
    if (len <= lengh) and (len > 0):
        l = random.randint(1, 3)
        if l == 1:
            return "T1+E"
        elif l == 2:
            return "T1-E"
        elif l == 3:
            return "T1"
    elif len > lengh:
        return "T1"
    elif len == -1:
        l = random.randint(1, 2)
        if l == 1:
            return "T1+E"
        elif l == 2:
            return "T1-E"

def t1(len):
    if len < lengh:
        l = random.randint(1, 3)
        if l == 1:
            return "T2*T1"
        elif l == 2:
            # return "T2/T1"
            return "T2"
        elif l == 3:
            return "T2"
    else:
        return "T2"


def t2():
    return "T3"


def t3(len):
    if len < lengh:
        l = random.randint(1, 2)
        if l == 1:
            return nums()
        elif l == 2:
            et3 = e(-1)
            st3 = "(" + et3 + ")"
            return st3
    else:
        return nums()


def nums():
    return str(random.randint(1, 10))


def gen(strin="E"):
    while ("E" in strin) or ("T1" in strin) or ("T2" in strin) or ("T3" in strin):
        if "E" in strin:
            strin = strin.replace("E", e(len(strin)), 1)
        if "T1" in strin:
            strin = strin.replace("T1", t1(len(strin)), 1)
        if "T2" in strin:
            strin = strin.replace("T2", t2(), 1)
        if "T3" in strin:
            strin = strin.replace("T3", t3(len(strin)), 1)
    return strin
