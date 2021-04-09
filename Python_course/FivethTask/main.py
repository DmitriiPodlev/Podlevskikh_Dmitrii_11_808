import split_by_symbol
import check_left_part
import check_right_part
import check_mail


mail = input("Input your mail: ")
parts = split_by_symbol.split_by_symbol(mail)
left_part = check_left_part.check_left_part(parts[0])
right_part = check_right_part.check_right_part(parts[1])
if len(right_part) < 1 or len(left_part) > 1 or len(left_part[0]) < len(parts[0]):
    print('Invalid mail')
else:
    print('Valid mail')
data = check_mail.check_mail(mail)
if len(data) < 1:
    print('Total invalid mail')
else:
    print('Total valid mail')

