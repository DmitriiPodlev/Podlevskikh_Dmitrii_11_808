# перевод чисел в десятичную систему
def get_sequence_of_numbers(sequence):
    seq_el = []

    for seq in sequence: 
        s = 0
        for i in range(0, len(seq)):
            if seq[i] == 1:
                s = s + 2 ** i
        seq_el.append(s)   
    return seq_el


