import test2 as c1
#import idGenerator
column = 1
nbFloor = 10
nbElevator = 2
itenerator = nbElevator
ascenseurA =  5
ascenseurB = 6
varElevatorList = [[None] * nbElevator, [None] * nbElevator]


while itenerator > 0:
    dimension = 2
    varElevatorList[itenerator - 1][dimension - 2] =  itenerator
    varElevatorList[itenerator -1][dimension - 1] = None
    itenerator -= 1

varElevatorList[1][1] = ascenseurA
varElevatorList[0][1] = ascenseurB
print('No Ascenseur ' + "/Etage ou est ascenseur ")
print(varElevatorList)


elevatorA = ascenseurA
elevatorB = ascenseurB

jersey = c1.MyColumn(nbFloor, nbElevator, elevatorA, elevatorB)
jersey.myfunc()

#vicky = idGenerator.MyGenerator(nbFloor, nbElevator, column)
#vicky.myVie()


