num_floor = 10
elevator_number = 2
positionAscenseurA = 2
positionAscenseurB = 6
minimumFloor = 1
maximumFloor = num_floor
status = 0
floorWhereRequestWasMade = 1




class Column:
    def __init__(self, num_floor, elevator_number, positionAscenseurA, positionAscenseurB, minimumFloor, maximumFloor):
        self.num_floor = num_floor
        self.elevator_number = elevator_number
        self.positionAscenseurA = positionAscenseurA
        self.positionAscenseurB = positionAscenseurB
        self.minimumFloor = minimumFloor
        self.maximumFloor = maximumFloor

    def createElevatorList(self):
        elevatorList = [None] * self.elevator_number, [None] * self.elevator_number
        elevatorList [0][elevator_number - 1] = positionAscenseurB
        elevatorList [0][elevator_number - 2] = positionAscenseurA
        elevatorList [1][elevator_number - 1] = elevator_number
        elevatorList [1][elevator_number - 2] = elevator_number - 1
        for x in elevatorList:
            print(x)

# creation de button haut et bas pour chaque etage a l'exterieur des ascenseurs

    def createButtonList(self):
        buttonList = ['UP'] * self.num_floor , ['DOWN'] * self.num_floor
        buttonList[self.elevator_number - self.elevator_number][self.maximumFloor - 1] = 'DISABLE'
        buttonList[self.elevator_number - 1][self.minimumFloor - 1] = 'DISABLE'
        for externView in buttonList:
            print(externView)  

#Sa cest ce que l'utilisateur vont voir a l'interieur de l'ascenseur

    def createPanelList(self):
        panelList = [None] * self.num_floor
        iterator = self.num_floor
        while iterator > 0:
            panelList[iterator - 1] = iterator
            iterator -= 1
        print('Voici panel list: ')
        print(panelList)


#
class Elevator:
    def __init__(self, num_floor, ux, status):
        self.num_floor = num_floor
        self.ux = ux
        self.status = status
    
    def createDoorsList(self):
        doorsList = ['CLOSED'] * self.num_floor
        doorsList[self.status] = ux
        print('voici etat des portes par etages vue de lexterieur')
        print(doorsList)


#ouvrir ou fermer les portes
class Doors:
    def __init__(self, status):
        self.status = status
    
    def doorsControl(self):
        if self.status == 1:
            doorsStatus = 'OPENED'
        else:
            doorsStatus = 'CLOSED'
        return doorsStatus

#appel un ascenseur ou signaler qu'un etage demande un ascenseur 
class Button:
    def __init__(self, num_floor,floorWhereRequestWasMade, status):
        self.floor = num_floor
        self.signal = floorWhereRequestWasMade
        self.status = status
    
    def RequestElevator(self):
        appelButtonList = ['OFF'] * self.floor
        if self.status == 1:
            appelButtonList[self.signal] = 'ON'
        print(appelButtonList)
        return True

#pour le moment je le fais comme une class, mais va etre une fonction que je vais mettre a l'interieur d'une class
#class GoTo:



f = Column(10, 2, 2, 6, 1, 10)
f.createElevatorList()
f.createPanelList()
f.createButtonList()

u = Doors(status)
ux = u.doorsControl()


o = Elevator(num_floor, ux, status)
o.createDoorsList()

k = Button(num_floor,floorWhereRequestWasMade,status)
k.RequestElevator()


"""
 JE VAIS FAIRE DES TESTS
 SCENARIO A
 
 ELEVATOR A EST INACTIF AU 2E ETAGE
 ELEVATOR B EST INACTIF AU 6E ETAGE
 Someone is on floor 3 and wants to go to the 7th floor. 
 Elevator A is expected to be sent.
"""
print('SCENARIO A COMMENCE')

scenarioA = Column(10,2,2,6,1,10)

print('UP(u)')
print('DOWN(d)')

actionButton = input()
if actionButton == 'u':
    floorWhereRequestWasMade = 3
    status = 1

f = Column(10, 2, 2, 6, 1, 10)
f.createElevatorList()
f.createPanelList()
f.createButtonList()

u = Doors(status)
ux = u.doorsControl()


o = Elevator(num_floor, ux, status)
o.createDoorsList()

k = Button(num_floor,floorWhereRequestWasMade,status)
k.RequestElevator()


