class MyColumn:
    def __init__(self, numberFloor, numberElevator, ascenseurPosition, ascenseurPositionB):
        self.nf = numberFloor
        self.ne = numberElevator
        self.na = ascenseurPosition
        self.nab = ascenseurPositionB
    
    def myfunc(self):
        elevatorIdList = [None] * self.ne
        lst = [None] * self.nf
        print(elevatorIdList)
        lst[self.na] = True
        lst[self.nab] = True
        print(lst)





