from bigrams import get_bigrams
from dictionary import calculate_dictionary_of_numbers_in_intervals
from matrix import calculate_matrix
from figure import get_sequence_of_numbers
from pi_vector import create_pi_vector
from sequences import create_sequences
from vector_b import calculate_vector_b
import math

# расчет энтропии
def get_entropy(k, n, pows, N, m, d, num):    
    sequence = create_sequences(N,n,k,pows)
    print('Последовательности:\n')
    for seq in sequence:
        seq.reverse()
        print(seq)
        seq.reverse()
        
    seq_el = get_sequence_of_numbers(sequence)
    print(f'\nДесятичные значения чисел:\n{seq_el}\n')
    
    vector = create_pi_vector(m, d, num, N)
    print(f'Вектор pi:\n{vector[0]}\n')
    print(f'Вектор a:\n{vector[1]}\n')
    
    numbers = []
    for i in range(1,N+1):
        numbers.append(i)
    
    d_of_y = calculate_dictionary_of_numbers_in_intervals(vector[1], numbers)
    
    b = calculate_vector_b(seq_el, d_of_y)
    print(f'Вектор b:\n{b}\n')
    
    bigram = get_bigrams(b)
    
    matrix = calculate_matrix(m, bigram, vector[1])
    print("Матрица:")
    for ar in matrix:
        print(ar)
    print('\n')
    
    H = 0
    for i in range(0, m):
        for j in range(0, m):
            if matrix[i][j] != 0:
                H = H + vector[0][i] * matrix[i][j] * math.log(2, matrix[i][j])
    H = -H
    return H


