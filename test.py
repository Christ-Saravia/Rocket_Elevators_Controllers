systemOuElevatorSePromene = [False,False,False,False,False,False,False,False,False,False]
ascenseurA = True
indexAscenseur = 1
ascenseurB = True
indexAscenseurB = 9

systemOuElevatorSePromene.insert(indexAscenseur,ascenseurA)
lastPositionA = systemOuElevatorSePromene.index(ascenseurA)
systemOuElevatorSePromene.pop(indexAscenseur + 1)

systemOuElevatorSePromene.insert(indexAscenseurB,ascenseurB)
lastPositionB = systemOuElevatorSePromene.index(ascenseurB)
systemOuElevatorSePromene.pop(indexAscenseurB + 1)

for y in systemOuElevatorSePromene:
    print(y)



mouvementVersHaut = 1
mouvementVersBas = -1

print("Vers quel etage voulez-vous aller: ")

etageDesiree = int(input())

if etageDesiree > lastPositionA:

    difference = etageDesiree - lastPositionA 
    iterator = mouvementVersHaut * difference
    while iterator > 0:
        systemOuElevatorSePromene.pop(lastPositionA)
        systemOuElevatorSePromene.insert(lastPositionA + mouvementVersHaut,ascenseurA)
        lastPositionA = systemOuElevatorSePromene.index(ascenseurA)
        iterator -= 1

else:

    difference = etageDesiree - lastPositionA 
    iterator = mouvementVersBas * difference
    while iterator > 0:
        systemOuElevatorSePromene.pop(lastPositionA)
        systemOuElevatorSePromene.insert(lastPositionA + mouvementVersBas,ascenseurA)
        lastPositionA = systemOuElevatorSePromene.index(ascenseurA)
        iterator -= 1



for z in systemOuElevatorSePromene:
    print(z)






