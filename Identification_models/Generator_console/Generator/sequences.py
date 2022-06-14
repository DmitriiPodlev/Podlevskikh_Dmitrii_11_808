from sum import sum_on_abs_2

# получение последовательностей
def create_sequences(N, n, k, pows):
    ar = []
    sequence = []

    sequence.append(1)
    for i in range(1, n):
        sequence.append(0)
    
    ar.append(sequence)
    last_seq = sequence

    for i in range(1, N):
        for t in range(0, k):
            sequence = []
            s = 0
            for j in range(0, len(last_seq)):
                if j in pows:
                    s = sum_on_abs_2(s, last_seq[j])
            for j in range(0, len(last_seq)):
                if j == len(last_seq) - 1:
                    sequence.append(s)
                else:
                    sequence.append(last_seq[j+1])
            last_seq = sequence
        ar.append(sequence)
    return ar


