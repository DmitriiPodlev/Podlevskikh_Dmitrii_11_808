from entropy import get_entropy
from pows import get_pows

# рассчитываем необходимые параметры и рассчитываем энтропию и матрицы
if __name__ == "__main__":
    print('Введите многочлен:')
    s = input()
    print('Введите k:')
    k = int(input())
    print('Введите n:')
    n = int(input())

    pows = get_pows(s)
    N = 2 ** n - 1

    for m in range(N // n - 1, N // n + 2):
        print(f'Вычисления для m = {m}\n')
        d = N % m
        num = N // m
        H = get_entropy(k, n, pows, N, m, d, num)
        print(f'Энтропия для m = {m} равна {H}\n')